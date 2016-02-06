using FluentValidation;
using Kong.OnlineStoreAPI.Model;

namespace Kong.OnlineStoreAPI.Logic.Validator
{
    public class UserRegistrtionValidator : AbstractValidator<User>
    {
        public UserRegistrtionValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage(Utility.REQUIRED_FIELD).
                Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage(Utility.INVALID_FIELD);

            RuleFor(u => u.Password).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);

            RuleFor(u => u.ConfirmPassword).NotEmpty().WithMessage(Utility.REQUIRED_FIELD).
                Equal(u => u.Password).WithMessage("doesn't match password;");
        }
    }

    public class UserLoginValidator : AbstractValidator<User>
    {
        public UserLoginValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Required").WithErrorCode("09");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Required").WithErrorCode("09");
        }
    }
}
