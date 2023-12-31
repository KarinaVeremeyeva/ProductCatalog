﻿using System.Net.Http.Headers;

namespace ProductCatalog.Web.Services
{
    public class ApiTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string TokenKey = "Authorization";

        public ApiTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext!.Request.Cookies[TokenKey];

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
