namespace EasyNews.SQLRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.GuardianFields");
            AlterColumn("dbo.GuardianFields", "id", c => c.String());
            AlterColumn("dbo.GuardianFields", "localID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.GuardianFields", "localID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.GuardianFields");
            AlterColumn("dbo.GuardianFields", "localID", c => c.String());
            AlterColumn("dbo.GuardianFields", "id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.GuardianFields", "id");
        }
    }
}
