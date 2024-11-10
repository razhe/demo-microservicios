using Ocelot.Requester;

namespace ApiGateway.Handlers
{
    public class SampleHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            // Do stuff and optionally call the base handler...
            return await base.SendAsync(request, token);
        }
    }
}
