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
            RuleFor(u => u.Password).Matches(Utility.VALID_PASSWORD).When(u => !string.IsNullOrEmpty(u.Password)).WithMessage(Utility.INVALID_PASSWORD);

            RuleFor(u => u.ConfirmPassword).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
            RuleFor(u => u.ConfirmPassword).Equal(u => u.Password).When(u => !string.IsNullOrEmpty(u.ConfirmPassword)).WithMessage(" doesn't match password;");
        }
    }

    public class UserLoginValidator : AbstractValidator<User>
    {
        public UserLoginValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
            RuleFor(u => u.Email).EmailAddress().When(u => !string.IsNullOrEmpty(u.Email)).WithMessage(Utility.INVALID_FIELD);

            RuleFor(u => u.Password).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
            RuleFor(u => u.Password).Matches(Utility.VALID_PASSWORD).When(u => !string.IsNullOrEmpty(u.Password)).WithMessage(Utility.INVALID_FIELD);
        }
    }

    public class UserActivationValidator : AbstractValidator<User>
    {
        public UserActivationValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
            RuleFor(u => u.Email).EmailAddress().When(u => !string.IsNullOrEmpty(u.Email)).WithMessage(Utility.INVALID_FIELD);

            RuleFor(u => u.Token).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
        }
    }

    public class UserRecoverPasswordValidator : AbstractValidator<User>
    {
        public UserRecoverPasswordValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage(Utility.REQUIRED_FIELD);
            RuleFor(u => u.Email).EmailAddress().When(u => !string.IsNullOrEmpty(u.Email)).WithMessage(Utility.INVALID_FIELD);
        }
    }

    public class UserChangePasswordValidator : AbstractValidator<User>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email" + Utility.REQUIRED_FIELD);
            RuleFor(u => u.Email).EmailAddress().When(u => !string.IsNullOrEmpty(u.Email)).WithMessage("Email" + Utility.INVALID_FIELD);

            RuleFor(u => u.TempPassword).NotEmpty().WithMessage("Old/Temp Password" + Utility.REQUIRED_FIELD);
            RuleFor(u => u.TempPassword).Matches(Utility.VALID_PASSWORD).When(u => !string.IsNullOrEmpty(u.Password)).WithMessage("Old/Temp Password" + Utility.INVALID_PASSWORD);


            RuleFor(u => u.Password).NotEmpty().WithMessage("New Password" + Utility.REQUIRED_FIELD);
            RuleFor(u => u.Password).Matches(Utility.VALID_PASSWORD).When(u => !string.IsNullOrEmpty(u.Password)).WithMessage("New Password" + Utility.INVALID_PASSWORD);

            RuleFor(u => u.ConfirmPassword).NotEmpty().WithMessage("Confirm New Password" + Utility.REQUIRED_FIELD);
            RuleFor(u => u.ConfirmPassword).Equal(u => u.Password).When(u => !string.IsNullOrEmpty(u.ConfirmPassword)).WithMessage("Confirm New Password doesn't match new password;");
        }
    }
}
