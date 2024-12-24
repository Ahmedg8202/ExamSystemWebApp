using AutoMapper;
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
        private readonly IMapper _mapper;

        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            var question = _mapper.Map<Question>(questiondto);
            question.Answers = options;

            //Question question = new Question
            //{
            //    QuestionId = Guid.NewGuid().ToString(),
            //    SubjectId = questiondto.SubjectId,
            //    Text = questiondto.Text,
            //    Answers = options
            //};

            await _unitOfWork.QuestionRepository.AddAsync(question);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> UpdateQuestion(Questiondto questiondto)
        {
            return true;
            //return await _unitOfWork.QuestionRepository.UpdateAsync(questiondto);
        }

        public async Task<bool> DeleteQuestion(string questionId)
        {
            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(questionId);
            if (question == null)
                return false;

            await _unitOfWork.QuestionRepository.DeleteAsync(question);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Question>> GetAllQuestions(string subjectId)
        {
            return await _unitOfWork.QuestionRepository.GetAll(subjectId);
        }

        public async Task<Question> GetQuestionById(string questionId)
        {
            return await _unitOfWork.QuestionRepository.GetQuestionById(questionId);
        }
    }
}
