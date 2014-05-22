using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace WebApi.Tests
{
    [TestFixture]
    public class Tests
    {
        private TestServer _server;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            _server = TestServer.Create<TestsStartup>();
        }

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            _server.Dispose();
        }

        [Test]
        public async Task PingTest()
        {
            //Arrange
            var client = new HttpClient(_server.Handler)
            {
                BaseAddress = new Uri("https://api.tripfiles.com"),
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/m/test/ping");
            requestMessage.Headers.Add("X-API", "1234");

            //Act
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            var content = response.Content.ReadAsStringAsync().Result;
            
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(@"""pong""", content);
        }

        [Test]
        public async Task SecureTest()
        {
            //Arrange
            var client = new HttpClient(_server.Handler)
            {
                BaseAddress = new Uri("https://api.tripfiles.com"),
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/m/test/secure");
            requestMessage.Headers.Add("X-API", "1234");
            var token = GetAccessToken();
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Result);

            //Act
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            var content = response.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(@"""secret""", content);
        }

        [Test]
        public async Task AuthenticationNegativeTest()
        {
            //Arrange
            var client = new HttpClient(_server.Handler)
            {
                BaseAddress = new Uri("https://api.tripfiles.com"),
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/m/test/secure");
            requestMessage.Headers.Add("X-API", "1234");

            //Act
            HttpResponseMessage response = await client.SendAsync(requestMessage);

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task RoutingNegativeTest()
        {
            //Arrange
            var client = new HttpClient(_server.Handler)
            {
                BaseAddress = new Uri("https://api.tripfiles.com"),
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/m/wrong");
            requestMessage.Headers.Add("X-API", "1234");

            //Act
            HttpResponseMessage response = await client.SendAsync(requestMessage);


            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task XAPI_Required_Test()
        {
            //Arrange
            var client = new HttpClient(_server.Handler)
            {
                BaseAddress = new Uri("https://api.tripfiles.com"),
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/");

            //Act
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            
            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task LoginTest()
        {
            //Arrange
            var client = new HttpClient(_server.Handler)
            {
                BaseAddress = new Uri("https://api.tripfiles.com"),
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/m/login");
            requestMessage.Headers.Add("X-API", "1234");
            requestMessage.Content = new StringContent("client_id=1234&client_secret=11223344&grant_type=password&username=michael@brainjocks.com&password=brain");

            //Act
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            var content = response.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var data = JObject.Parse(content);
            var accessToken = (string)data["access_token"];

            Assert.IsNotEmpty(accessToken);
        }
        
        private async Task<string> GetAccessToken()
        {
            //Arrange
            var client = new HttpClient(_server.Handler)
            {
                BaseAddress = new Uri("https://api.tripfiles.com"),
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/m/login");
            requestMessage.Headers.Add("X-API", "1234");
            requestMessage.Content = new StringContent("client_id=1234&client_secret=11223344&grant_type=password&username=michael@brainjocks.com&password=brain");

            HttpResponseMessage response = await client.SendAsync(requestMessage);
            var content = response.Content.ReadAsStringAsync().Result;

            var data = JObject.Parse(content);
            return (string)data["access_token"];
        }
    }
}
