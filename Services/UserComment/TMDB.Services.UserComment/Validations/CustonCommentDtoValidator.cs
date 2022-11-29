using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDB.Services.UserComment.Dtos;

namespace TMDB.Services.UserComment.Validations
{
    public class CustonCommentDtoValidator : AbstractValidator<CustonCommentDto>
    {
        public CustonCommentDtoValidator()
        {
            RuleFor(x => x.Point).InclusiveBetween(0, 10).WithMessage("Kulanıcı puanı ({PropertyName}) 0 ile 10 arasında olmalıdır.");
        }
    }
}
