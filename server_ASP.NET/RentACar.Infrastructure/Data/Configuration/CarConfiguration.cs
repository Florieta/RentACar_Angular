using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Domain.Entitites;
using RentACar.Domain.Entitites.Enum.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Data.Configuration
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {

        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(CreateCars());
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Category)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.CategoryId);
            builder.HasOne(d => d.Dealer)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.DealerId);
            builder.Property(r => r.RegNumber)
                   .IsRequired()
                   .HasMaxLength(8);
            builder.Property(m => m.Make)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(m => m.Model)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(m => m.MakeYear)
                   .IsRequired();
            builder.Property(i => i.ImageUrl)
                   .IsRequired();
            builder.Property(a => a.IsDeleted)
                .IsRequired();
            builder.Property(d => d.DailyRate)
                .IsRequired();
            builder.Property(a => a.IsAvailable)
                .IsRequired();
        }

        private List<Car> CreateCars()
        {
            var cars = new List<Car>()
            {
                new Car()
                 {
                      Id = 1,
                      RegNumber = "B1234AB",
                      Make = "Toyota",
                      Model = "Corolla",
                      MakeYear = 2022,
                      AirCondition = true,
                      Seats = 5,
                      Doors = 5,
                      NavigationSystem = false,
                      Fuel = Fuel.Petrol,
                      ImageUrl = "https://images.dealer.com/autodata/us/640/color/2022/USD20TOC041A0/209.jpg?_returnflight_id=091119126",
                      Transmission = Transmission.Manual,
                      DailyRate = 40,
                      IsAvailable = true,
                      IsDeleted = false,
                      CategoryId = 3,
                      DealerId = 1
                 },

                new Car()
                {
                    Id = 2,
                    RegNumber = "B1444CB",
                    Make = "Hundai",
                    Model = "i20",
                    MakeYear = 2022,
                    AirCondition = true,
                    Seats = 5,
                    Doors = 5,
                    NavigationSystem = false,
                    Fuel = Fuel.Diesel,
                    ImageUrl = "https://s7g10.scene7.com/is/image/hyundaiautoever/BC3_5DR_TopTrim_DG01-01_EXT_front_rgb_v01_w3a-1:4x3?wid=960&hei=720&fmt=png-alpha&fit=wrap,1",
                    Transmission = Transmission.Automatic,
                    DailyRate = 33,
                    IsAvailable = true,
                    IsDeleted = false,
                    CategoryId = 1,
                    DealerId = 1
                },

                new Car()
                {
                    Id = 3,
                    RegNumber = "B1223AB",
                    Make = "Citroen",
                    Model = "C4",
                    MakeYear = 2022,
                    AirCondition = true,
                    Seats = 5,
                    Doors = 5,
                    NavigationSystem = false,
                    Fuel = Fuel.Hybrid,
                    ImageUrl = "https://www.citroen-eg.com/wp-content/uploads/2021/11/Polar-White-front1.jpg",
                    Transmission = Transmission.Automatic,
                    DailyRate = 37,
                    IsAvailable = true,
                    IsDeleted= false,
                    CategoryId = 2,
                    DealerId = 1
                }
            };

            return cars;
        }
    }
}
