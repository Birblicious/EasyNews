namespace EasyNews.SQLRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuardianFields",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 1024),
                        headline = c.String(),
                        standfirst = c.String(),
                        trailText = c.String(),
                        byline = c.String(),
                        main = c.String(),
                        body = c.String(),
                        newspaperPageNumber = c.String(),
                        wordcount = c.String(),
                        firstPublicationDate = c.DateTime(),
                        isInappropriateForSponsorship = c.String(),
                        isPremoderated = c.String(),
                        lastModified = c.DateTime(),
                        newspaperEditionDate = c.DateTime(),
                        productionOffice = c.String(),
                        publication = c.String(),
                        shortUrl = c.String(),
                        shouldHideAdverts = c.String(),
                        showInRelatedContent = c.String(),
                        thumbnail = c.String(),
                        legallySensitive = c.String(),
                        lang = c.String(),
                        bodyText = c.String(),
                        charCount = c.String(),
                        shouldHideReaderRevenue = c.String(),
                        showAffiliateLinks = c.String(),
                        commentable = c.String(),
                        sensitive = c.String(),
                        displayHint = c.String(),
                        commentCloseDate = c.DateTime(),
                        screenStory = c.String(),
                        sectionId = c.String(),
                        isSaved = c.Boolean(nullable: false),
                        localID = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GuardianFields");
        }
    }
}
