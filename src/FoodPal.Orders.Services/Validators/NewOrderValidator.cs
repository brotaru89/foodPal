using FluentValidation;
using FoodPal.Orders.Dtos;

namespace FoodPal.Orders.Services.Validators
{
	public class NewOrderValidator : InternalValidator<NewOrderDto>
	{
		public NewOrderValidator()
		{
			RuleFor(x => x.CustomerId).NotEmpty();
			RuleFor(x => x.CustomerName).NotEmpty();
			RuleFor(x => x.CustomerEmail).EmailAddress();
			RuleFor(x => x.Items).NotEmpty();

			RuleFor(x => x.DeliveryDetails).SetValidator(new DeliveryDetailsValidator());
			RuleFor(x => x.OrderStatus).NotEmpty();
			RuleFor(x => x.OrderStatus.StatusId).NotEmpty();
		}
	}
}
