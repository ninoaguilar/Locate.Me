using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using CompanyService.Models;
using LocateMeApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace CompanyService.Test
{
    public class SimpleIntegrationTests
    {
        private readonly TestServer testServer;
        private readonly HttpClient testClient;

        private readonly Company fooCompany;
        private readonly Employee barEmployee;

        public SimpleIntegrationTests()
        {
            testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            testClient = testServer.CreateClient();

            fooCompany = new Company()
            {
                Id = Guid.NewGuid(),
                Name = "Foo"
            };

            barEmployee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bar",
                LastName = "John",
            };
        }

        [Fact]
        public async void Test_company_post_and_get()
        {
            StringContent stringContent = new StringContent(
                JsonConvert.SerializeObject(fooCompany),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage postResponse =
                await testClient.PostAsync(
                    "api/companies",
                    stringContent);
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await testClient.GetAsync("api/companies");
            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();
            List<Company> companies =
                JsonConvert.DeserializeObject<List<Company>>(raw);
            Assert.Equal(3, companies.Count());
            Assert.Equal("Foo", companies[2].Name);
            Assert.Equal(fooCompany.Id, companies[2].Id);
        }

        [Fact]
        public async void Test_employee_post_and_get()
        {
            StringContent companyStringContent = new StringContent(
                JsonConvert.SerializeObject(fooCompany),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage companyPostResponse =
                await testClient.PostAsync(
                    "api/companies",
                    companyStringContent);
            companyPostResponse.EnsureSuccessStatusCode();

            StringContent stringContent = new StringContent(
                JsonConvert.SerializeObject(barEmployee),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage postResponse =
                await testClient.PostAsync(
                    "api/companies/" + fooCompany.Id + "/employees",
                    stringContent);
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await testClient.GetAsync("api/companies/" + fooCompany.Id + "/employees/");
            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();
            List<Employee> employees =
                JsonConvert.DeserializeObject<List<Employee>>(raw);
            Assert.Single(employees);
            Assert.Equal("Bar", employees[0].FirstName);
            Assert.Equal("John", employees[0].LastName);
            Assert.Equal(barEmployee.Id, employees[0].Id);
        }
    }
}
