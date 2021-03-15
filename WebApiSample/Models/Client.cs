using System;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiSample.Models
{
    [SwaggerSchema]
    public class Client
    {
        [SwaggerSchema("클라이언트 ID")]
        public string clientId { get; set; }

        [SwaggerSchema("클라이언트 명칭")]
        public string clientName { get; set; }

        [SwaggerSchema("비밀번호")]
        public string password { get; set; }

        [SwaggerSchema("API 인증키")]
        public string apiKey { get; set; }
    }
}
