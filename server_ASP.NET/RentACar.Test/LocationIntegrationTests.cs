using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RentACar.WebApi.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Test
{
    public class LocationIntegrationTests
    {
        private static WebApplicationFactory<Program> _factory;
        public LocationIntegrationTests()
        {
            _factory = new CustomWebAplicationFactory<Program>();
        }

        [Fact]
        public async Task GetById_ShouldReturnResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Location/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task GetById_ShouldRetunr404IfNotFound()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Location/45");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task All_ShouldReturnResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Location");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task CreateLocation_ShouldReturnResponse()
        {
            var newCategory = new AddLocationModel
            {
                LocationName = "Sofia Mall Paradise",
                Address = "Sofia, 1000, Vitosha"

            };
            var client = _factory.CreateClient();
            var response = await client.PostAsync("api/Location", new StringContent(JsonConvert.SerializeObject(newCategory), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

      
        [Fact]
        public async Task DeleteLocation_ShouldReturn404IfNotFound()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Location/45");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}
