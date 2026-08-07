// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net.Http.Json;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial class CoolifyApiBroker : ICoolifyApiBroker
    {
        private readonly HttpClient httpClient;

        public CoolifyApiBroker(IHttpClientFactory httpClientFactory) =>
            this.httpClient = httpClientFactory.CreateClient(nameof(CoolifyApiBroker));

        private async ValueTask<T> GetAsync<T>(string relativeUrl, CancellationToken cancellationToken)
            where T : class
        {
            HttpResponseMessage response = await this.httpClient.GetAsync(relativeUrl, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
        }

        private async ValueTask<T> PostAsync<T>(string relativeUrl, object content, CancellationToken cancellationToken)
            where T : class
        {
            HttpResponseMessage response =
                await this.httpClient.PostAsJsonAsync(relativeUrl, content, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
        }

        private async ValueTask PostAsync(string relativeUrl, object content, CancellationToken cancellationToken)
        {
            HttpResponseMessage response =
                await this.httpClient.PostAsJsonAsync(relativeUrl, content, cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        private async ValueTask PostAsync(string relativeUrl, CancellationToken cancellationToken)
        {
            HttpResponseMessage response =
                await this.httpClient.PostAsync(relativeUrl, content: null, cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        private async ValueTask<T> PatchAsync<T>(string relativeUrl, object content, CancellationToken cancellationToken)
            where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, relativeUrl)
            {
                Content = JsonContent.Create(content)
            };

            HttpResponseMessage response = await this.httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
        }

        private async ValueTask DeleteAsync(string relativeUrl, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await this.httpClient.DeleteAsync(relativeUrl, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
    }
}
