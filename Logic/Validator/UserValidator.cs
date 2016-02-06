using FluentValidation;
using Kong.OnlineStoreAPI.Model;

namespace Kong.OnlineStoreAPI.Logic.Validator
{
    public class UserRegistrtionValidator : AbstractValidator<User>
    {
        public UserRegistrtionValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
            RuleFor(u => u.Email).EmailAddress().When(u => !string.IsNullOrEmpty(u.Email)).WithMessage(Utility.INVALID_FIELD);

            RuleFor(u => u.Password).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
            RuleFor(u => u.Password).Matches(@"(?=^.{8,16}$)(?=.*\d)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$").When(u => !string.IsNullOrEmpty(u.Password)).WithMessage(Utility.INVALID_PASSWORD);

            RuleFor(u => u.ConfirmPassword).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
            RuleFor(u => u.ConfirmPassword).Equal(u => u.Password).When(u => !string.IsNullOrEmpty(u.ConfirmPassword)).WithMessage(" doesn't match password;");
        }
    }

    public class UserLoginValidator : AbstractValidator<User>
    {
        public UserLoginValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
            RuleFor(u => u.Password).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
        }
    }
}
