using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Domain.Entitites;
using RentACar.Domain.Entitites.Enum.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Data.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(CreateOrders());
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Car)
                .WithMany(b => b.Orders)
                .HasForeignKey(c => c.CarId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Renter)
                .WithMany(b => b.Orders)
               .HasForeignKey(c => c.RenterId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.PickUpLocation)
                 .WithMany(c => c.PickUpLocations)
                 .HasForeignKey(t => t.PickUpLocationId)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.DropOffLocation)
                 .WithMany(c => c.DropOffLocations)
                 .HasForeignKey(t => t.DropOffLocationId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Property(pd => pd.PickUpDateAndTime)
                .IsRequired();
            builder.Property(dd => dd.DropOffDateAndTime)
               .IsRequired();
            builder.Property(d => d.Duration)
               .IsRequired();
            builder.Property(a => a.TotalAmount)
               .IsRequired();
            builder.Property(a => a.PaymentType)
              .IsRequired();
        }

        private List<Order> CreateOrders()
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                     Id = 1,
                     PickUpDateAndTime = new DateTime(2022, 11, 17, 5, 0, 0),
                     DropOffDateAndTime = new DateTime(2022, 11, 23, 6, 0, 0),
                     Duration = 6,
                     PaymentType = PaymentType.Card,
                     CarId = 3,
                     PickUpLocationId = 1,
                     DropOffLocationId = 1,
                     TotalAmount = 292,
                     IsActive = true,
                     IsPaid = false,
                     IsDeleted = false,
                     RenterId = 1
                },
                new Order()
                {
                     Id = 2,
                     PickUpDateAndTime = new DateTime(2022, 11, 17, 3, 0, 0),
                     DropOffDateAndTime = new DateTime(2022, 11, 20, 5, 0, 0),
                     Duration = 3,
                     PaymentType = PaymentType.BankTransfer,
                     CarId = 2,
                     PickUpLocationId = 1,
                     DropOffLocationId = 2,
                     TotalAmount = 114,
                     IsActive = true,
                     IsPaid = false,
                     IsDeleted = false,
                     RenterId = 1
                }
            };

            return orders;
        }
    }
}
