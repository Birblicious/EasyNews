using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNews.Core.GuardianFilters
{
    public class GuardianSections
    {
        public string ClickedSectionList { get; set; }

        public GuardianSections(string value) {
            this.ClickedSectionList = value;
        }
    }
}