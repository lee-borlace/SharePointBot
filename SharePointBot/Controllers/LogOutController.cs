using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SharePointBot.Controllers
{

    public class LogOutController : ApiController
    {
        /// <summary>
        /// Respond to log out message. 
        /// </summary>
        /// <param name="conversationRef"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("LogOut")]
        public async Task<HttpResponseMessage> LogOut([FromUri] string conversationRef)
        {
            // Get the conversation reference from the URL (this was specified when triggering logout in the first place). Send a message
            // to the user confirming logout is complete.
            var conversationRefDecoded = UrlToken.Decode<ConversationReference>(conversationRef);
            var message = conversationRefDecoded.GetPostToBotMessage();
            var client = new ConnectorClient(new Uri(message.ServiceUrl));

            var replyMessage = message.CreateReply("You are now logged out.");
            await client.Conversations.SendToConversationAsync((Activity)replyMessage);

            // Show a message in the browser indicating logout is complete.
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent($"<html><body>You are now logged out of SharePoint.</body></html>", System.Text.Encoding.UTF8, @"text/html");
            return resp;
        }
    }
}
