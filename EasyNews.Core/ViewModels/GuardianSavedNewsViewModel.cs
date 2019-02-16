using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNews.Core.Models;

namespace EasyNews.Core.ViewModels
{
    public class GuardianSavedNewsViewModel
    {
        public DropdownList Sections { get; set; }
        public enum DropdownList
        {
            All,
            World,
            Politics,
            [Description("uk-news")]
            UkNews,
            [Description("us-News")]
            UsNews,
            Cities,
            Science,
            [Description("global-development")]
            GlobalDevelopment,
            Sport,
            Football,
            Technology,
            Business,
            Environment,
            Film,
            Commentisfree,
            Obituaries

        }

        public List<GuardianFields> GuardianFields { get; set; }
    }
}
