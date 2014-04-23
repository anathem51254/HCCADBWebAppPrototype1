namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConsumerAreaOfInterestModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID", "dbo.ConsumerRepAreaOfInterestModel");
            DropIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", new[] { "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID" });
            AddColumn("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID1", c => c.Int());
            AlterColumn("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID", c => c.Int(nullable: false));
            CreateIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID1");
            AddForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID1", "dbo.ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModelID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID1", "dbo.ConsumerRepAreaOfInterestModel");
            DropIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", new[] { "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID1" });
            AlterColumn("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID", c => c.Int());
            DropColumn("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID1");
            CreateIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID");
            AddForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID", "dbo.ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModelID");
        }
    }
}
