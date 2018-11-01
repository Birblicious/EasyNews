using System.Net;
using System.Text;
using System.Web.Mvc;
using EasyNews.Core.Models;
using EasyNews.Core.Contracts;
using EasyNews.Core.ViewModels;
using Newtonsoft.Json;
using EasyNews.InMemoryRepository;
using EasyNews.Services.GuardianWebServices;
using System.Collections.Generic;
using System.Linq;

namespace EasyNews.WebUI.Controllers
{
    public class HomeController : Controller
    {
        GuardianJSON guardianJSON;
        
        IRepository<GuardianResults> repository;
        IRepository<GuardianFields> SQLRepository;
        IRepository<GuardianFilterViewModel> filterRepository;

        public HomeController(
            IRepository<GuardianResults> repository, 
            IRepository<GuardianFields> SQLRepository,
            IRepository<GuardianFilterViewModel> filterRepository) {
            this.repository = repository;
            this.SQLRepository = SQLRepository;
            this.filterRepository = filterRepository;
        }

        public PartialViewResult _HeaderNavbar()
        {
            return PartialView(filterRepository.Collection());
        }

        
        public ActionResult Index()
        {
            GuardianWebServices guardianServices = new GuardianWebServices();

            using (WebClient web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                string url = guardianServices.getJSONUrl(
                   null,
                   null,
                   null,
                   "10");

                var json = web.DownloadString(url);

                GuardianJSON guardianJSON = JsonConvert.DeserializeObject<GuardianJSON>(json);

                foreach (var content in guardianJSON.response.results)
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
                    if (content.fields.thumbnail == null) {
                        content.fields.thumbnail = "https://images.pexels.com/photos/1095602/pexels-photo-1095602.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260";
                    }

                    if (content.fields.trailText == null)
                    {
                        content.fields.trailText = "Pretty old stuff fam!";
                    }
                    repository.Insert(content);
                }
                repository.Commit();
                this.guardianJSON = guardianJSON;
            }
            return View(guardianJSON);
        }

        [HttpPost]
        public ActionResult Index(GuardianFilterViewModel models)
        {
            GuardianWebServices guardianServices = new GuardianWebServices();

            using (WebClient web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                string url = guardianServices.getJSONUrl(
                    models.SearchContent,
                    models.Sections.ClickedSectionList,
                    models.OrderBy.ClickedOrderByItem,
                    models.PageSize.ToString());

                var json = web.DownloadString(url);

                GuardianJSON guardianJSON = JsonConvert.DeserializeObject<GuardianJSON>(json);
                
                foreach (var content in guardianJSON.response.results) {

                    if (content.fields.bodyText.Length > 1000){ content.fields.screenStory = content.fields.bodyText.Substring(0, 1000) + "..."; }
                    else if (content.fields.bodyText.Length > 750) { content.fields.screenStory = content.fields.bodyText.Substring(0, 750) + "..."; }
                    else if (content.fields.bodyText.Length > 500) { content.fields.screenStory = content.fields.bodyText.Substring(0, 500) + "..."; }
                    else if (content.fields.bodyText.Length > 250) { content.fields.screenStory = content.fields.bodyText.Substring(0, 250) + "..."; }
                    else { content.fields.screenStory =  "..."; } //content.fields.bodyText.Substring(0, 10) +



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
                this.guardianJSON = guardianJSON;
            }
            return View(guardianJSON);
        }

            public ActionResult Details(string shortUrl)
        {
            GuardianResults result = repository.Find(shortUrl);
            return View(result.fields);
        }

        public ActionResult Save(string localID, string shortUrl)
        {
            GuardianResults result = repository.Find(shortUrl);
            if (result != null) {
                //result.fields.shortUrl = result.id;
                result.fields.isSaved = true;
                SQLRepository.Insert(result.fields);
                SQLRepository.Commit();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string localID, string shortUrl, string actionName)
        {
            GuardianFields result = SQLRepository.Find(shortUrl);
            if (result != null)
            {
                //result.fields.shortUrl = result.id;
                result.isSaved = true;
                SQLRepository.Delete(result.shortUrl);
                SQLRepository.Commit();
            }

            return RedirectToAction(actionName);
        }

        public ActionResult SavedNews() {
            List<GuardianFields> allContent = SQLRepository.Collection().ToList();
            return View(allContent);
        }

        public ActionResult SavedNewsDetails(string shortUrl)
        {
            GuardianFields result = SQLRepository.Find(shortUrl);
            return View(result);
        }
    }
}