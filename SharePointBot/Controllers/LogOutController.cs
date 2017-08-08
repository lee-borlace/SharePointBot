using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SharePointBot.Controllers
{
    public class LogOutController : ApiController
    {
        [HttpGet]
        [Route("LogOut")]
        public async Task<HttpResponseMessage> LogOut()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent($"<html><body>You are now logged out of SharePoint Bot.</body></html>", System.Text.Encoding.UTF8, @"text/html");
            return resp;
        }
    }
}
