using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNews.Core.ViewModels;
using EasyNews.Services.Constants;

namespace EasyNews.Services.GuardianWebServices
{
    public class GuardianWebServices
    {
        public string getJSONUrl(string searchArea, string sectionArea, string orderByArea, string pageSize) {
            GuardianWebConstants constants = new GuardianWebConstants();
            string urlToReturn = string.Format(constants.getWebUrl());
            string apiKey = string.Format(constants.getApiKey());

            searchArea = (searchArea != null && searchArea != "Seach Here") ? "&q=" + searchArea : string.Empty;
            sectionArea = (sectionArea != null && sectionArea != "Section") ? "&section=" + sectionArea : string.Empty;
            orderByArea = (orderByArea != null && orderByArea != "Order By") ? "&order-by=" + orderByArea : string.Empty;
            pageSize = (pageSize != null && pageSize != string.Empty) ? "&page-size=" + pageSize : string.Empty; 

            urlToReturn = urlToReturn + pageSize + searchArea + sectionArea + orderByArea + apiKey;

            return urlToReturn;
        }
    }
}
