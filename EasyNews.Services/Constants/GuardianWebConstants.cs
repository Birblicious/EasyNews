using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNews.Services.Constants
{
    public class GuardianWebConstants
    {
        private const string webUrl = "https://content.guardianapis.com/search?show-fields=all";
        private const string apiKey = "&api-key=d1ad8b82-7bce-42a5-bcce-e404259076a5";

        public string getApiKey() {
            return apiKey;
        }

        public string getWebUrl()
        {
            return webUrl;
        }
    }
}
