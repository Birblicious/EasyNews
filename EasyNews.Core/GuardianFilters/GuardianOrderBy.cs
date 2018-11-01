using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNews.Core.GuardianFilters
{
    public class GuardianOrderBy
    {
        public string ClickedOrderByItem { get; set; }

        public GuardianOrderBy(string value)
        {
            ClickedOrderByItem = value;
        }
    }
}
