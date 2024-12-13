using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExamQuestiondto> GetRandomExam()
        {
            var examQuestions = await _unitOfWork.ExamRepository.GetRandomExamAsync();

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


            return new ExamQuestiondto
            {
                ExamId = examQuestions.FirstOrDefault().ExamId,
                Questions = questions
            };
        }

        public async Task<List<ExamQuestion>> ExamById(string examId)
        {
            var exam = await _unitOfWork.ExamRepository.GetByIdAsync(examId);
            //if (exam == null)
            //    return null;
            var examFromdb = await _unitOfWork.ExamRepository.GetExamById(examId);
            if (examFromdb == null)
                return null;

            
            return examFromdb;
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
            return score;
        }
        public async Task<ExamResult> SubmitExam(SubmitExamdto examdto)
        {
            var exam = await _unitOfWork.ExamRepository.GetExamById(examdto.ExamId);
            if (exam == null)
                return null;

            int score = await calculateScore(examdto.questions);
                
            bool passed = (score * 1.0 / examdto.questions.Count()) * 100 > 49;

            var examResult = new ExamResult
            {
                ExamResultId = Guid.NewGuid().ToString(),
                ExamId = examdto.ExamId,
                StudentId = examdto.StudentId,
                Score = score,
                Status = passed
            };

            var result =  await _unitOfWork.ExamResultRepository.AddExamResult(examResult);
            if (!result)
                return null;

            return examResult;
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

        public async Task<IEnumerable<ExamResult>> GetExamHistoryForStudent(string studentId)
        {
            var examResults = await _unitOfWork.ExamRepository.GetExamHistoryAsync(studentId);

            if (examResults == null || !examResults.Any())
                return null;

            return examResults.Select(result => new ExamResult
            {
                ExamId = result.ExamId,
                StudentId = result.StudentId,
                Score = result.Score,
                Status = result.Status
            }).ToList();
        }

        public async Task<IEnumerable<ExamResult>> AllExamResults()
        {
            var examResults = await _unitOfWork.ExamResultRepository.GetAllAsync();

            if (examResults == null || !examResults.Any())
                return null;

            return examResults.Select(result => new ExamResult
            {
                ExamId = result.ExamId,
                StudentId = result.StudentId,
                Score = result.Score,
                Status = result.Status
            }).ToList();
        }
    }

}
