using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    [Route("api/v{version:apiVersion}/client")]
    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        [SwaggerOperation(Summary = "인증을 수행한다.")]
        public ApiResponse<Client> Authentication(Client data)
        {
            ApiResponse<Client> result = new ApiResponse<Client>();
            System.Console.WriteLine("aaa");

            return result;
        }

        [HttpGet]
        [Route("{clientId}")]
        [MapToApiVersion("2")]
        [SwaggerOperation(Summary = "클라이언트 정보를 반환한다.")]
        public ApiResponse<Client> ClientInfo(string clientId)
        {
            ApiResponse<Client> apiResponse = new ApiResponse<Client>();

            apiResponse.result = new Client
            {
                clientId = clientId
            };

            return apiResponse;
        }
    }
}
