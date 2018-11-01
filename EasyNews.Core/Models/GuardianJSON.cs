using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyNews.Core.Models
{
    public class GuardianJSON
    {
        public GuardianResponse response { get; set; }
    }

    public class GuardianResponse
    {
        public string status { get; set; }
        public string userTier { get; set; }
        public int total { get; set; }
        public int startIndex { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int pages { get; set; }
        public string orderBy { get; set; }
        public IEnumerable<GuardianResults> results { get; set; }
    }

    public class GuardianResults : BaseModel
    {
        public string id { get; set; }
        public string type { get; set; }
        public string sectionId { get; set; }
        public string sectionName { get; set; }
        public string webPublicationDate { get; set; }
        public string webTitle { get; set; }
        public string webUrl { get; set; }
        public string apiUrl { get; set; }
        public bool isHosted { get; set; }
        public string pillarId { get; set; }
        public string pillarName { get; set; }
        public GuardianFields fields {get;set;}
        public string responseID { get; set; }
    }

    public class GuardianFields : BaseModel
    {
        public string headline { get; set; }
        public string standfirst { get; set; }
        public string trailText { get; set; }
        public string byline { get; set; }
        public string main { get; set; }
        public string body { get; set; }
        public string newspaperPageNumber { get; set; }
        public string wordcount { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? firstPublicationDate { get; set; }
        public string isInappropriateForSponsorship { get; set; }
        public string isPremoderated { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? lastModified { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? newspaperEditionDate { get; set; }
        public string productionOffice { get; set; }
        public string publication { get; set; }
        //public string shortUrl { get; set; }
        public string shouldHideAdverts { get; set; }
        public string showInRelatedContent { get; set; }
        public string thumbnail { get; set; }
        public string legallySensitive { get; set; }
        public string lang { get; set; }
        public string bodyText { get; set; }
        public string charCount { get; set; }
        public string shouldHideReaderRevenue { get; set; }
        public string showAffiliateLinks { get; set; }
        public string commentable { get; set; }
        public string sensitive { get; set; }
        public string displayHint { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? commentCloseDate { get; set; }
        public string screenStory { get; set; }
        public string sectionId { get; set; }
        public bool isSaved { get; set; }
    }
}
