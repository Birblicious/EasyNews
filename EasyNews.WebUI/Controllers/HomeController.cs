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
        IRepository<GuardianResults> repository;
        IRepository<GuardianFields> SQLRepository;
        IRepository<GuardianFilterModel> filterRepository;

        public HomeController(
            IRepository<GuardianResults> repository,
            IRepository<GuardianFields> SQLRepository,
            IRepository<GuardianFilterModel> filterRepository)
        {
            this.repository = repository;
            this.SQLRepository = SQLRepository;
            this.filterRepository = filterRepository;
        }

        public ActionResult Index()
        {
            GuardianWebServices guardianServices = new GuardianWebServices();
            GuardianHomeViewModel guardianHomeView = new GuardianHomeViewModel();
            using (WebClient web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;
                string url = guardianServices.getJSONUrl();
                var json = web.DownloadString(url);
                GuardianJSON guardianJSON = JsonConvert.DeserializeObject<GuardianJSON>(json);
                guardianHomeView.Guardian = guardianServices.FillGuardianJSONModel(guardianJSON, SQLRepository, repository);
                guardianHomeView.GuardianFilter = new GuardianFilterModel();
                if (filterRepository.Collection().Count() == 0)
                {
                    filterRepository.Insert(guardianHomeView.GuardianFilter);
                    filterRepository.Commit();
                }
                else
                {
                    guardianHomeView.GuardianFilter = filterRepository.Collection().First();
                    guardianHomeView.GuardianFilter.OrderBy.ClickedOrderByItem = null;
                    guardianHomeView.GuardianFilter.Sections.ClickedSectionList = null;
                    guardianHomeView.GuardianFilter.SearchContent = null;
                    guardianHomeView.GuardianFilter.PageSize = 10;
                    filterRepository.Update(guardianHomeView.GuardianFilter);
                    filterRepository.Commit();
                }
            }
            return View(guardianHomeView);
        }

        [HttpPost]
        public ActionResult Index(GuardianFilterModel guardianFilter)
        {
            GuardianWebServices guardianServices = new GuardianWebServices();
            GuardianHomeViewModel guardianHomeView = new GuardianHomeViewModel();
            using (WebClient web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;
                string url = guardianServices.getJSONUrl(
                    guardianFilter.SearchContent,
                    guardianFilter.Sections.ClickedSectionList,
                    guardianFilter.OrderBy.ClickedOrderByItem,
                    guardianFilter.PageSize.ToString());
                var json = web.DownloadString(url);
                GuardianJSON guardianJSON = JsonConvert.DeserializeObject<GuardianJSON>(json);
                guardianHomeView.Guardian = guardianServices.FillGuardianJSONModel(guardianJSON, SQLRepository, repository);
                guardianHomeView.GuardianFilter = filterRepository.Collection().First();
                guardianHomeView.GuardianFilter.OrderBy.ClickedOrderByItem = guardianFilter.OrderBy.ClickedOrderByItem;
                guardianHomeView.GuardianFilter.Sections.ClickedSectionList = guardianFilter.Sections.ClickedSectionList;
                guardianHomeView.GuardianFilter.SearchContent = guardianFilter.SearchContent;
                guardianHomeView.GuardianFilter.PageSize = guardianFilter.PageSize;
                filterRepository.Update(guardianHomeView.GuardianFilter);
                filterRepository.Commit();
            }
            return View(guardianHomeView);
        }

        public ActionResult Details(string shortUrl)
        {
            GuardianDetailsViewModel guardianDetailsView = new GuardianDetailsViewModel();
            guardianDetailsView.GuardianResults = repository.Find(shortUrl);
            guardianDetailsView.guardianFilter = filterRepository.Collection().First();
            return View(guardianDetailsView);
        }

        public ActionResult Save(string localID, string shortUrl)
        {
            GuardianResults result = repository.Find(shortUrl);
            if (result != null)
            {
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

        public ActionResult SavedNews()
        {
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