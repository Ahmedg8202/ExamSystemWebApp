using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;

namespace ExamSystem.Application.Services
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExamQuestiondto> GetRandomExam(string subjectId)
        {
            var examQuestions = await _unitOfWork.ExamRepository.GetRandomExamAsync(subjectId);

            var questions = examQuestions.Select(examQuestion => new QuestionExam
            {
                questionId = examQuestion.Question.QuestionId,
                text = examQuestion.Question.Text,
                Answers = examQuestion.Question.Answers.Select(answer => new AnswerExam
                {
                    answerId = answer.AnswerId,
                    text = answer.Text,
                    isCorrect = answer.IsCorrect
                }).ToList()
            }).ToList();

            var subjectName = examQuestions.FirstOrDefault().Exam.Subject.Name;
            return new ExamQuestiondto
            {
                ExamId = examQuestions.FirstOrDefault().ExamId,
                SubjectId = subjectId,
                SubjectName = subjectName,
                Questions = questions
            };
        }

        public async Task<ExamQuestiondto> ExamById(string examId)
        {
            var exam = await _unitOfWork.ExamRepository.GetByIdAsync(examId);
            if (exam == null)
                return null;
            var examFromdb = await _unitOfWork.ExamRepository.GetExamById(examId);
            if (!examFromdb.Any())
                return null;

            var questions = examFromdb.Select(examQuestion => new QuestionExam
            {
                questionId = examQuestion.Question.QuestionId,
                text = examQuestion.Question.Text,
                Answers = examQuestion.Question.Answers.Select(answer => new AnswerExam
                {
                    answerId = answer.AnswerId,
                    text = answer.Text,
                    isCorrect = answer.IsCorrect
                }).ToList()
            }).ToList();


            return new ExamQuestiondto
            {
                ExamId = examFromdb.FirstOrDefault().ExamId,
                SubjectId = exam.SubjectId,
                SubjectName = examFromdb.FirstOrDefault().Exam.Subject.Name,
                Questions = questions
            };
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

        public async Task<ExamResultdto> SubmitExam(SubmitExamdto examdto)
        {
            var exam = await _unitOfWork.ExamRepository.GetExamById(examdto.ExamId);
            if (exam == null)
                return null;

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

            var result =  await _unitOfWork.ExamResultRepository.AddExamResult(examResult);
            if (!result)
                return null;

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
            return await _unitOfWork.ExamRepository.AddAsync(exam);
        }

        public async Task<bool> UpdateExam(Examdto examdto)
        {
            return false;
            //return await _examGRepository.UpdateAsync(exam);
        }

        public async Task<bool> DeleteExam(string examId)
        {
            return false; 
            //return await _examGRepository.DeleteAsync(examId);
        }

        public async Task<IEnumerable<ExamResultdto>> GetExamHistoryForStudent(string studentId)
        {
            var examResults = await _unitOfWork.ExamRepository.GetExamHistoryAsync(studentId);

            if (examResults == null || !examResults.Any())
                return null;
            var subjectId = examResults.FirstOrDefault().SubjectId;
            var subjectName = (await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId)).Name;

            var studentName = (await _unitOfWork.StudentRepository.GetByIdAsync(studentId)).userName;

            return examResults.Select(result => new ExamResultdto
            {
                ExamId = result.ExamId,
                StudentId = result.StudentId,
                SubjectId = result.SubjectId,
                StudentName = studentName,
                SubjectName = subjectName,
                DateTime = result.DateTime,
                Score = result.Score,
                Status = result.Status
            }).ToList();
        }

        private async Task<string> getUserName(string userId)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(userId);
            return (student).userName;
        }
        private async Task<string> getSubjectName(string subjectId)
        {
            return (await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId)).Name;
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

        public async Task<IEnumerable<ExamFromdb>> AllExams()
        {
            var examFromdDB =  await _unitOfWork.ExamRepository.GetAllAsync();
            var exams = new List<ExamFromdb> { };
            foreach(var exam in examFromdDB)
            {
                var name = await getSubjectName(exam.SubjectId);
                exams.Add(new ExamFromdb
                {
                    ExamId  = exam.ExamId,
                    SubjectId = exam.SubjectId,
                    SubjectName = name,
                    ExamQuestions = exam.ExamQuestions
                });
            }
            return exams;
        }
    }

}
