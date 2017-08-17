using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotAuth.Models
{
    [Serializable]
    public class AuthenticationOptions
    {
        public string ClientType { get; set; }
        public string Authority { get; set; }
        public string ResourceId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string[] Scopes { get; set; }
        public string RedirectUrl { get; set; }
        public string Policy { get; set; }
    }
}
