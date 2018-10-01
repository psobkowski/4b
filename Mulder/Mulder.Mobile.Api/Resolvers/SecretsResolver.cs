using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mulder.Mobile.Api.Resolvers
{
    public class SecretsResolver
    {
        public string SqlConnection { get; set; }
        public string RequestSource { get; set; }
        public string SecurityKey { get; set; }
    }
}
