using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNews.Core.Models;
using EasyNews.Services.Constants;
using EasyNews.SQLRepository;
using EasyNews.InMemoryRepository;
using EasyNews.Core.Contracts;

namespace EasyNews.Services.GuardianWebServices
{
    public class GuardianWebServices
    {
        public string getJSONUrl(string searchArea = null, string sectionArea = null, string orderByArea = null, string pageSize = "10") {
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

        public GuardianJSON FillGuardianJSONModel(GuardianJSON jSON, IRepository<GuardianFields> SQLRepository, IRepository<GuardianResults> repository) {
            repository.Clear();
            foreach (var content in jSON.response.results)
            {

                if (content.fields.bodyText.Length > 1000) { content.fields.screenStory = content.fields.bodyText.Substring(0, 1000) + "..."; }
                else if (content.fields.bodyText.Length > 750) { content.fields.screenStory = content.fields.bodyText.Substring(0, 750) + "..."; }
                else if (content.fields.bodyText.Length > 500) { content.fields.screenStory = content.fields.bodyText.Substring(0, 500) + "..."; }
                else if (content.fields.bodyText.Length > 250) { content.fields.screenStory = content.fields.bodyText.Substring(0, 250) + "..."; }
                else { content.fields.screenStory = "..."; } //content.fields.bodyText.Substring(0, 10) +

                if (SQLRepository.Find(content.fields.shortUrl) != null)
                {
                    content.fields.isSaved = true;
                }
                else
                {
                    content.fields.isSaved = false;
                }
                content.shortUrl = content.fields.shortUrl;
                content.fields.sectionId = content.sectionId;
                if (content.fields.thumbnail == null)
                {
                    content.fields.thumbnail = "https://images.pexels.com/photos/1095602/pexels-photo-1095602.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260";
                }

                if (content.fields.trailText == null)
                {
                    content.fields.trailText = "Pretty old stuff fam!";
                }

                repository.Insert(content);
            }

            repository.Commit();
            return jSON;
        }
    }
}
