namespace EasyNews.SQLRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.GuardianFields");
            AlterColumn("dbo.GuardianFields", "localID", c => c.String());
            AlterColumn("dbo.GuardianFields", "shortUrl", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.GuardianFields", "shortUrl");
            DropColumn("dbo.GuardianFields", "id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GuardianFields", "id", c => c.String());
            DropPrimaryKey("dbo.GuardianFields");
            AlterColumn("dbo.GuardianFields", "shortUrl", c => c.String());
            AlterColumn("dbo.GuardianFields", "localID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.GuardianFields", "localID");
        }
    }
}
