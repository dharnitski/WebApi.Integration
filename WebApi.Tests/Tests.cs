using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
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
            Assert.AreEqual(@"""pong""", content);
        }
    }
}
