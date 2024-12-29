using AutoMapper;
using ExamSystem.API.Hubs;
using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ExamSystem.Application.Services
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub, IHub> _hubContext;
        public ExamService(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<NotificationHub, IHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ExamResultdto>> AllExamResults(int page, int pageSize)
        {
            var examResults = await _unitOfWork.ExamResultRepository.GetAllAsync(page, pageSize);

            if (examResults == null || !examResults.Any())
                return null;

            var subjectId = examResults.FirstOrDefault().SubjectId;
            var subjectName = await getSubjectName(subjectId);


            var resultDtoList = new List<ExamResultdto>();

            foreach (var result in examResults)
            {
                var studentName = await getUserName(result.StudentId);

                resultDtoList.Add(new ExamResultdto
                {
                    ExamId = result.ExamId,
                    StudentId = result.StudentId,
                    SubjectId = result.SubjectId,
                    StudentName = studentName,
                    SubjectName = subjectName,
                    DateTime = result.DateTime,
                    Score = result.Score,
                    Status = result.Status
                });
            }

            return resultDtoList;
        }
        
        public async Task<IEnumerable<ExamFromdb>> AllExams(int page, int pageSize)
        {
            var examFromDB = await _unitOfWork.ExamRepository.GetAllAsync(page, pageSize);

            var exams = _mapper.Map<IEnumerable<ExamFromdb>>(examFromDB);

            foreach (var exam in exams)
            {
                exam.SubjectName = await getSubjectName(exam.SubjectId);
            }

            return exams;
        }
        
        public async Task<ExamQuestiondto> GetRandomExam(string subjectId)
        {
            var examQuestions = await _unitOfWork.ExamRepository.GetRandomExamAsync(subjectId);

            if (examQuestions == null || !examQuestions.Any())
                return null;

            var examQuestionDto = _mapper.Map<ExamQuestiondto>(examQuestions.First());

            examQuestionDto.Questions = _mapper.Map<List<QuestionExam>>(examQuestions);

            return examQuestionDto;
        }
        public async Task<IEnumerable<ExamResultdto>> GetExamHistoryForStudent(string studentId, int page, int pageSize)
        {
            var examResults = await _unitOfWork.ExamRepository.GetExamHistoryAsync(studentId, page, pageSize);

            if (examResults == null || !examResults.Any())
                return null;

            var studentName = await getUserName(studentId);

            return examResults.Select(result => new ExamResultdto
            {
                ExamId = result.ExamId,
                StudentId = result.StudentId,
                SubjectId = result.SubjectId,
                StudentName = studentName,
                SubjectName = result.Exam?.Subject?.Name,
                DateTime = result.DateTime,
                Score = result.Score,
                Status = result.Status
            }).ToList();
        }


        public async Task<ExamQuestiondto> ExamById(string examId)
        {
            var exam = await _unitOfWork.ExamRepository.GetByIdAsync(examId);
            if (exam == null)
                return null;

            var examFromDb = await _unitOfWork.ExamRepository.GetExamById(examId);
            if (examFromDb == null || !examFromDb.Any())
                return null;

            var questions = _mapper.Map<List<QuestionExam>>(examFromDb);

            return new ExamQuestiondto
            {
                ExamId = examFromDb.First().ExamId,
                SubjectId = exam.SubjectId,
                SubjectName = exam.Subject.Name,
                Questions = questions
            };
        }

        public async Task<ExamResultdto> SubmitExam(SubmitExamdto examdto)
        {
            var exam = await _unitOfWork.ExamRepository.GetByIdAsync(examdto.ExamId);
            if (exam == null) return null;

            int score = await calculateScore(examdto.questions);

            bool passed = score > 49;

            var subjectName = await getSubjectName(examdto.SubjectId);

            var examResult = new ExamResult
            {
                ExamResultId = Guid.NewGuid().ToString(),
                ExamId = examdto.ExamId,
                StudentId = examdto.StudentId,
                SubjectId = examdto.SubjectId,
                DateTime = DateTime.Now,
                Score = score,
                Status = passed
            };

            await _unitOfWork.ExamResultRepository.AddAsync(examResult);
            if (await _unitOfWork.CompleteAsync() <= 0) return null;


            await _hubContext.Clients.All.ReceiveExamNotification($"{await getUserName(examdto.StudentId)}" , "has submit exam");

            return new ExamResultdto
            {
                SubjectId = examResult.SubjectId,
                ExamResultId = examResult.ExamResultId,
                DateTime = examResult.DateTime,
                ExamId = examResult.ExamId,
                Score = examResult.Score,
                SubjectName = subjectName,
                StudentId = examResult.StudentId,
                Status = examResult.Status
            };
        }

        public async Task<bool> AddExam(Examdto examdto)
        {
            List<Question> questions = new List<Question>();
            foreach (var question in examdto.Questions)
            {
                Question q = await _unitOfWork.QuestionRepository.GetByIdAsync(question);
                questions.Add(q);
            }

            string examId = Guid.NewGuid().ToString();

            Exam exam = new Exam
            {
                ExamId = examId,
                SubjectId = examdto.SubjectId,
                ExamQuestions = questions.Select(q => new ExamQuestion
                {
                    QuestionId = q.QuestionId,
                    ExamId = examId
                }).ToList()

            };
            await _unitOfWork.ExamRepository.AddAsync(exam);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> UpdateExam(Examdto examdto)
        {
            return false;
            //return await _examGRepository.UpdateAsync(exam);
        }

        public async Task<bool> DeleteExam(string examId)
        {
            var exam = await _unitOfWork.ExamRepository.GetByIdAsync(examId);
            if (exam == null)
                return false;

            await _unitOfWork.ExamRepository.DeleteAsync(exam);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteExamResult(string examResultId)
        {
            var examResult = await _unitOfWork.ExamResultRepository.GetByIdAsync(examResultId);
            if (examResult == null)
                return false;

            await _unitOfWork.ExamResultRepository.DeleteAsync(examResult);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        private async Task<int> calculateScore(List<question> questions)
        {
            int score = 0;
            foreach (var question in questions)
            {
                var correctAnswerId = await _unitOfWork.QuestionRepository.GetCorrectAnswerId(question.QuestionId);
                if (correctAnswerId == question.AnswerId)
                {
                    score++;
                }
            }
            return (int)((score * 100.0) / questions.Count);
        }

        private async Task<string> getUserName(string userId)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(userId);
            return student.userName;
        }

        private async Task<string> getSubjectName(string subjectId)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
            return subject.Name;
        }

    }

}
