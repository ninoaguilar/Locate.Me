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

        private readonly Company sampleCompany;
        private readonly Employee sampleEmployee;

        public SimpleIntegrationTests()
        {
            testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            testClient = testServer.CreateClient();

            sampleCompany = new Company()
            {
                Id = Guid.NewGuid(),
                Name = "Foo"
            };

            sampleEmployee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bar",
                LastName = "John",
            };
        }

        [Fact]
        public async void Test_company_post_and_get()
        {
            StringContent bodyContent = CreateBody(sampleCompany);

            HttpResponseMessage postResponse =
                await testClient.PostAsync(
                    "api/companies",
                    bodyContent);
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await testClient.GetAsync("api/companies");
            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();
            List<Company> companies =
                JsonConvert.DeserializeObject<List<Company>>(raw);

            Assert.Equal(3, companies.Count());
            Assert.Equal("Foo", companies[2].Name);
            Assert.Equal(sampleCompany.Id, companies[2].Id);
        }

        [Fact]
        public async void Test_employee_delete()
        {
            StringContent bodyContent = CreateBody(sampleCompany);

            HttpResponseMessage companyPostResponse =
                await testClient.PostAsync(
                    "api/companies",
                    bodyContent);
            companyPostResponse.EnsureSuccessStatusCode();

            StringContent stringContent = new StringContent(
                JsonConvert.SerializeObject(sampleEmployee),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage postResponse =
                await testClient.PostAsync(
                    "api/companies/" + sampleCompany.Id + "/employees",
                    stringContent);
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await testClient.GetAsync("api/companies/" + sampleCompany.Id + "/employees/");
            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();
            List<Employee> employees =
                JsonConvert.DeserializeObject<List<Employee>>(raw);
            Assert.Single(employees);
            Assert.Equal("Bar", employees[0].FirstName);
            Assert.Equal("John", employees[0].LastName);
            Assert.Equal(sampleEmployee.Id, employees[0].Id);

            string deleteString = "api/companies/" + sampleCompany.Id + "/employees/" + sampleEmployee.Id;

            HttpResponseMessage deleteResponse =
                await testClient.DeleteAsync(
                    deleteString);
            getResponse.EnsureSuccessStatusCode();
            raw = await deleteResponse.Content.ReadAsStringAsync();
            Employee deletedEmployee = JsonConvert.DeserializeObject<Employee>(raw);
            Assert.Equal(sampleEmployee.Id, deletedEmployee.Id);
        }

        public StringContent CreateBody(object obj)
        {
            return new StringContent(
                JsonConvert.SerializeObject(obj),
                Encoding.UTF8,
                "application/json");
        }
    }
}
