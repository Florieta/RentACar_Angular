using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using RentACar.Domain.Entitites;
using RentACar.Domain.Entitites.Enum.Car;
using RentACar.WebApi.ViewModels.Car;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Test
{
    /// <summary>
    /// Integration tests for car controller
    /// </summary>

    public class CarIntegrationTests
    {
        private static WebApplicationFactory<Program> _factory;
        public CarIntegrationTests()
        {
            _factory = new CustomWebAplicationFactory<Program>();
        }

        [Fact]
        public async Task GetById_ShouldReturnResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Car/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task GetById_ShouldRetunr404IfNotFound()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Car/123");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task All_ShouldReturnResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Car");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task CreateCar_ShouldReturnResponse()
        {
            var newCar = new AddCarModel
            {
                Make = "Audi",
                Model = "R8",
                MakeYear = 2020,
                DailyRate = 30,
                RegNumber = "B1233AB",
                AirCondition = true,
                Seats = 5,
                Doors = 5,
                ImageUrl = "car1",
                NavigationSystem = false,
                Fuel = Fuel.Hybrid.ToString(),
                Transmission = Transmission.Automatic.ToString(),
                CategoryId = 1,
                DealerId = 1,
                
            };
            var client = _factory.CreateClient();
            var response = await client.PostAsync("api/Car", new StringContent(JsonConvert.SerializeObject(newCar), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task DeleteCar_ShouldReturnNoContent()
        {
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/car/2");
            Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        }
        [Fact]
        public async Task DeleteCar_ShouldReturn404IfNotFound()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/car/45");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task EditCar_ShouldReturnResponse()
        {

            var updateCar = new EditCarViewModel
            {
                Id = 1,
                Make = "Toyota",
                Model = "Corolla",
                MakeYear = 2022,
                DailyRate = 30,
                RegNumber = "B1234AB",
                AirCondition = true,
                Seats = 5,
                Doors = 5,
                NavigationSystem = false,
                Fuel = Fuel.Hybrid.ToString(),
                Transmission = Transmission.Automatic.ToString(),
                CategoryId = 1,
                DealerId = 1
            };
            var client = _factory.CreateClient();
            var response = await client.PutAsync("api/Car/1", new StringContent(JsonConvert.SerializeObject(updateCar), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        }

        [Fact]
        public async Task EditCar_ShouldReturn404IfIdNotFound()
        {

            var updateCar = new EditCarViewModel
            {
                Id = 101,
                Make = "Toyota",
                Model = "Corolla",
                MakeYear = 2022,
                DailyRate = 30,
                RegNumber = "B1234AB",
                AirCondition = true,
                Seats = 5,
                Doors = 5,
                NavigationSystem = false,
                Fuel = Fuel.Hybrid.ToString(),
                Transmission = Transmission.Automatic.ToString(),
                DealerId = 1
            };
            var client = _factory.CreateClient();
            var response = await client.PutAsync("api/Car/101", new StringContent(JsonConvert.SerializeObject(updateCar), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }


        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}
