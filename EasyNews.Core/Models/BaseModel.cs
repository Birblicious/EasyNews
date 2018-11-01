using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EasyNews.Core.Models
{
    public class BaseModel
    {
        
        public string localID { get; set; }

        [Key]
        public string shortUrl { get; set; }

        public BaseModel() {
            localID = Guid.NewGuid().ToString();
        }
    }
}
