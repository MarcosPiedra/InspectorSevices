using InspectorServices.WebAPI.DTOs;
using InspectorServices.WebAPI.Tests.Unit;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoorballServices.WebAPI.Tests.Unit
{
    public class InspectionTest : InspectorWebApi
    {
        private static string InspectionMethods => "api/v1/Inspection";
        private InspectionRequest GetToSend() => new InspectionRequest() { Address = "Address 1", Customer = "Customer 1", Date = "Date 1", Observations = "Observations 1", Status = 1 };
        private void ChangeToSend(InspectionRequest m) => m.Customer = "Customer 2";

        [Fact]
        public async Task Get_all_return_ok()
        {
            var server = await GetServer();
            var client = server.GetTestClient();
            var response = await client.GetAsync($"{InspectionMethods}");
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task Get_by_id_return_ok()
        {
            var toSend = GetToSend();
            var server = await GetServer();
            var client = server.GetTestClient();
            var content = new StringContent(JsonConvert.SerializeObject(toSend), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{InspectionMethods}", content);
            response.EnsureSuccessStatusCode();

            var id = 1;
            response = await client.GetAsync($"{InspectionMethods}/{id}");
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task Get_by_id_return_not_found()
        {
            var id = int.MaxValue;
            var server = await GetServer();
            var client = server.GetTestClient();
            var response = await client.GetAsync($"{InspectionMethods}/{id}");
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_return_ok()
        {
            var toSend = GetToSend();
            var server = await GetServer();
            var client = server.GetTestClient();
            var content = new StringContent(JsonConvert.SerializeObject(toSend), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{InspectionMethods}", content);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Put_manager_ok()
        {
            var toSend = GetToSend();
            var server = await GetServer();
            var client = server.GetTestClient();
            var content = new StringContent(JsonConvert.SerializeObject(toSend), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{InspectionMethods}", content);
            var idQuery = response.Headers.Location.Query.Replace("?id=", "");
            if (int.TryParse(idQuery, out int id))
            {
                ChangeToSend(toSend);
                content = new StringContent(JsonConvert.SerializeObject(toSend), Encoding.UTF8, "application/json");
                response = await server.GetTestClient().PutAsync($@"{InspectionMethods}\{id}", content);

                Assert.True(response.StatusCode == HttpStatusCode.OK);
            }
            response.EnsureSuccessStatusCode();
        }

    }
}

