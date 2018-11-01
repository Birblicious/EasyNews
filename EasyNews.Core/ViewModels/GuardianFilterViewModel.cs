using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNews.Core.GuardianFilters;
using EasyNews.Core.Models;

namespace EasyNews.Core.ViewModels
{
    public class GuardianFilterViewModel : BaseModel
    {
        
        

        public string SearchContent { get; set; }

        [Range(10, 1000)]
        public int PageSize { get; set; }

        public GuardianSections Sections { get; set; }
        public List<GuardianSections> SectionList { get; set; }

        public List<GuardianOrderBy> OrderByList { get; set; }
        public GuardianOrderBy OrderBy { get; set; }

        public GuardianFilterViewModel() {

            PageSize = 10;

            Sections = new GuardianSections("Section");
            OrderBy = new GuardianOrderBy("Order By");

            SectionList = new List<GuardianSections>();
            SectionList.Add(new GuardianSections("world"));
            SectionList.Add(new GuardianSections("uk-news"));
            SectionList.Add(new GuardianSections("cities"));
            SectionList.Add(new GuardianSections("science"));
            SectionList.Add(new GuardianSections("global-development"));
            SectionList.Add(new GuardianSections("football"));
            SectionList.Add(new GuardianSections("technology"));
            SectionList.Add(new GuardianSections("business"));
            SectionList.Add(new GuardianSections("environment"));
            SectionList.Add(new GuardianSections("obituaries"));

            OrderByList = new List<GuardianOrderBy>();
            OrderByList.Add(new GuardianOrderBy("newest"));
            OrderByList.Add(new GuardianOrderBy("oldest"));
            OrderByList.Add(new GuardianOrderBy("relevance"));
        }
    }
}
