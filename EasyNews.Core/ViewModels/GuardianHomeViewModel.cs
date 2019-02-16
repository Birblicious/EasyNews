using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNews.Core.Models;

namespace EasyNews.Core.ViewModels
{
    public class GuardianHomeViewModel
    {
        public GuardianJSON Guardian { get; set; }
        public GuardianFilterModel GuardianFilter { get; set; }
    }
}
