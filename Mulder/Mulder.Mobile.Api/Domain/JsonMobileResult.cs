using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mulder.Mobile.Api.Domain
{
    public class JsonMobileResult
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string ErrorMessage { get; set; }

        public JsonMobileResult(string errorMessage)
        {
            this.Success = false;
            this.Data = null;
            this.ErrorMessage = errorMessage;
        }

        public JsonMobileResult(object data)
        {
            this.Success = true;
            this.Data = data;
            this.ErrorMessage = null;
        }
    }
}
