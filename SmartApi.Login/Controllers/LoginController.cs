using RestSharp;
using SmartApi.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace SmartApi.Login.Controllers
{
    public class Login: ApiController
    {
        
        [HttpGet]
        public string Teste()
        {
            string a = "teste";
            return a;
        }

        [HttpPost]
        public IHttpResponse GetCep()
        {
            User user = new User();

            user.CEP = "60841280";
            var client = new RestClient("viacep.com.br/ws/" + user.CEP + "/json/");
            var RSrequest = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };

            return (IHttpResponse)client.Execute(RSrequest);

        }
    }
}
