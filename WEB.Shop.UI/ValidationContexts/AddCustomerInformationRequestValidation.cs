using FluentValidation;
using WEB.Shop.Application.Cart;

namespace WEB.Shop.UI.ValidationContexts
{
    public class AddCustomerInformationRequestValidation 
        : AbstractValidator<AddCustomerInformation.Request>
    {
        public AddCustomerInformationRequestValidation()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.PhoneNumber).NotNull().MinimumLength(7);
            RuleFor(x => x.Address1).NotNull();
            RuleFor(x => x.City).NotNull();
            RuleFor(x => x.PostCode).NotNull();
            RuleFor(x => x.FirstName).NotNull();
        }
    }
}
