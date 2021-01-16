using FoodPal.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodPal.Orders.Data.Configurations
{
	public class DeliveryDetailsEntityTypeConfiguration : IEntityTypeConfiguration<DeliveryDetails>
	{
		public void Configure(EntityTypeBuilder<DeliveryDetails> builder)
		{
			builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
			builder.Property(x => x.City).IsRequired().HasMaxLength(100);
			builder.Property(x => x.County).IsRequired().HasMaxLength(100);
			builder.Property(x => x.ZipCode).IsRequired();
			builder.Property(x => x.Country).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Phone).IsRequired().HasMaxLength(10);
		}
	}
}
