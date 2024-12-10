using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddQuestion(Questiondto questiondto)
        {
            List<Answer> options = new List<Answer>();
            foreach (string o in questiondto.Options)
            {
                options.Add(new Answer
                {
                    AnswerId = Guid.NewGuid().ToString(),
                    Text = o,
                    IsCorrect = (o == questiondto.CorrecAnswer),
                });
            }

            Question question = new Question
            {
                QuestionId = Guid.NewGuid().ToString(),
                Text = questiondto.Text,
                Answers = options
            };

            return await _unitOfWork.QuestionRepository.AddAsync(question);
        }

        public async Task<bool> UpdateQuestion(Questiondto questiondto)
        {
            return true;
            //return await _unitOfWork.QuestionRepository.UpdateAsync(questiondto);
        }

        public async Task<bool> DeleteQuestion(string questionId)
        {
            return true;
            //return await _unitOfWork.QuestionRepository.DeleteAsync(questionId);
        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await _unitOfWork.QuestionRepository.GetAllAsync();
        }

        public async Task<Question> GetQuestionById(string questionId)
        {
            return await _unitOfWork.QuestionRepository.GetQuestionById(questionId);
        }
    }
}
