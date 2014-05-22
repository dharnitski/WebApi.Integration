using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TripFiles.Mobile.HttpHandlers
{
    public class ApiKeyMessageHandler : DelegatingHandler
    {
        //private readonly IPublicWebApiSettings _settings;

        //public ApiKeyMessageHandler(IPublicWebApiSettings settings)
        //{
        //    if (settings == null) throw new ArgumentNullException("settings");
        //    _settings = settings;
        //}


        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiKeyHeader = request.Headers.FirstOrDefault(h => h.Key == "X-API");
            if (apiKeyHeader.Value == null)
            {
                //todo: check if it is logged somewhere
                var badResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("X-API is missing!")
                };
                return badResponse;
            }

            //var settings = (IPublicWebApiSettings)request.GetDependencyScope().GetService(typeof(IPublicWebApiSettings));

            if (apiKeyHeader.Value.Count() != 1 || apiKeyHeader.Value.First() != "1234")
            {
                //todo: check if it is logged somewhere
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Wrong Api Key!")
                };
            }
            // Call the inner handler.
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}