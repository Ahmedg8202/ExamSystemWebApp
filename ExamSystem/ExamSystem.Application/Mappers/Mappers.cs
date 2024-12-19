using AutoMapper;
using ExamSystem.Application.DTOs;
using ExamSystem.Core.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Mappers
{
    public class Mappers: Profile
    {
        public Mappers()
        {
            CreateMap<IdentityUser, Userdto>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => "Success"));

            CreateMap<Subjectdto, Subject>()
                .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<Questiondto, Question>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<Answer, AnswerExam>();

            CreateMap<Question, QuestionExam>()
                .ForMember(dest => dest.questionId, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

            CreateMap<ExamQuestion, QuestionExam>()
                .IncludeMembers(e => e.Question);

            CreateMap<ExamQuestion, ExamQuestiondto>()
                .ForMember(dest => dest.ExamId, opt => opt.MapFrom(src => src.ExamId))
                .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.Exam.SubjectId))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Exam.Subject.Name))
                .ForMember(dest => dest.Questions, opt => opt.Ignore());


            CreateMap<Exam, ExamFromdb>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));


        }
    }
}
