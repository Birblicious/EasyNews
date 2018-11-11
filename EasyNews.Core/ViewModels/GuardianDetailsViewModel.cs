using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNews.Core.Models;
namespace EasyNews.Core.ViewModels
{
    public class GuardianDetailsViewModel
    {
        public GuardianResults GuardianResults { get;set;}
        public GuardianFilterModel guardianFilter { get; set; }
    }
}
