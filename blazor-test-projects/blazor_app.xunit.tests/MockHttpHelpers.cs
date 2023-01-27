using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace blazor_app.xunit.tests
{
    public static class MockHttpHelpers
    {
        public static MockHttpMessageHandler AddMockHttpClient(this TestServiceProvider services)
        {
            var mockHttpHandler = new MockHttpMessageHandler();
            var httpClient = mockHttpHandler.ToHttpClient();
            httpClient.BaseAddress = new Uri("http://localhost");
            services.AddSingleton<HttpClient>(httpClient);
            return mockHttpHandler;
        }

        public static MockedRequest RespondJson<T>(this MockedRequest request, T content)
        {
            request.Respond(req =>
            {
                HttpResponseMessage response = new()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(content))
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            });
            return request;
        }

        public static MockedRequest RespondJson<T>(this MockedRequest request, Func<T> contentProvider)
        {
            request.Respond(req =>
            {
                HttpResponseMessage response = new()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(contentProvider()))
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            });
            return request;
        }
    }
}
