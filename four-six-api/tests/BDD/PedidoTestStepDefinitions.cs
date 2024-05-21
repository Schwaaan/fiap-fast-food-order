using BDD.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace BDD
{
    [Binding]
    public class PedidoTestStepDefinitions
    {
        private const string BaseAddress = "http://localhost:5293/";
        public WebApplicationFactory<Program> Factory { get; }
        public HttpClient Client { get; set; } = null!;
        private HttpResponseMessage Response { get; set; } = null!;
        public JsonFilesRepository JsonFilesRepo { get; }

        public PedidoTestStepDefinitions(WebApplicationFactory<Program> factory)
        {
            Factory = factory;
        }

        [Given(@"a url do endpoint '([^']*)'")]
        public void GivenAUrlDoEndpoint(string p0)
        {
            Client = Factory.CreateDefaultClient(new Uri(p0));
        }

        [When(@"chamar um GET para '([^']*)'")]
        public async Task WhenChamarUmGETPara(string endpoint)
        {
            Response = await Client.GetAsync(endpoint);
        }

        [Then(@"o statuscode code is '([^']*)'")]
        public void ThenOStatuscodeCodeIs(int statusCode)
        {
            var expected = (HttpStatusCode)statusCode;
            Assert.Equal(expected, Response.StatusCode);
        }

    }
}
