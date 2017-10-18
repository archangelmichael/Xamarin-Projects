using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace XAzureAuth.Authentication
{
    public class OAuthCodeHttpHandler : HttpClientHandler
    {
        private readonly string username;
        private readonly string samltoken;

        public OAuthCodeHttpHandler(string username, string samltoken)
		{
			this.username = username;
			this.samltoken = samltoken;
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			string credentials = username + ":" + samltoken;

			request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials)));

			return base.SendAsync(request, cancellationToken);
		}
    }
}
