using AutoMapper;
using ExamSystem.Application.DTOs;
using ExamSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Mappers
{
    public class ExamMappers: Profile
    {
        public ExamMappers()
        {
            CreateMap<Exam, Examdto>()
                .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.SubjectId))
                .ForMember(dest => dest.SubjectName, opt => opt.Ignore()) // AutoMapper will skip this property
                ;
        }
    }
}
