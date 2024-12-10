using ExamSystem.Application.DTOs;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Validators
{
    public class SubjectValidator: AbstractValidator<Subjectdto>
    {
        public SubjectValidator(IAdminRepository _adminRepository) {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Subject's name is required")
                .Must(name => ! _adminRepository.SubjectExists(name))
                .WithMessage("Subject name must be unique");
            RuleFor(s => s.QuestionsNumber).NotEmpty().WithMessage("Subject's questions number is required");
            RuleFor(s => s.total).NotEmpty().WithMessage("Subject's total is required");
            RuleFor(s => s.Duration).NotEmpty().WithMessage("Subject's duration is required");
            RuleFor(s => s.QuestionsNumber).NotEmpty().WithMessage("Subject's name is required");
            
            //unique subject in databse validate
        }
    }
}
