namespace HCCADBWebAppPrototype1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommitteeModel_CommitteeAreaOfHealthModel",
                c => new
                    {
                        CommitteeModel_CommitteeAreaOfHealthModelID = c.Int(nullable: false, identity: true),
                        CommitteeModel_CommitteeModelID = c.Int(nullable: false),
                        CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID = c.Int(),
                        CommitteeModel_CommitteeModelID1 = c.Int(),
                    })
                .PrimaryKey(t => t.CommitteeModel_CommitteeAreaOfHealthModelID)
                .ForeignKey("dbo.CommitteeAreaOfHealthModel", t => t.CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID)
                .ForeignKey("dbo.CommitteeModel", t => t.CommitteeModel_CommitteeModelID1)
                .Index(t => t.CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID)
                .Index(t => t.CommitteeModel_CommitteeModelID1);
            
            CreateTable(
                "dbo.CommitteeAreaOfHealthModel",
                c => new
                    {
                        CommitteeAreaOfHealthModelID = c.Int(nullable: false, identity: true),
                        AreaOfHealthName = c.String(),
                    })
                .PrimaryKey(t => t.CommitteeAreaOfHealthModelID);
            
            CreateTable(
                "dbo.CommitteeModel",
                c => new
                    {
                        CommitteeModelID = c.Int(nullable: false, identity: true),
                        CommitteeName = c.String(),
                        CurrentStatus = c.Int(),
                    })
                .PrimaryKey(t => t.CommitteeModelID);
            
            CreateTable(
                "dbo.ConsumerRepCommitteeHistoryModel",
                c => new
                    {
                        ConsumerRepCommitteeHistoryModelID = c.Int(nullable: false, identity: true),
                        CommitteeModelID = c.Int(nullable: false),
                        ConsumerRepModelID = c.Int(nullable: false),
                        PrepTime = c.Int(nullable: false),
                        Meetingtime = c.Int(nullable: false),
                        EndorsementStatus = c.Int(),
                        EndorsementDate = c.DateTime(nullable: false),
                        EndorsementType = c.Int(),
                    })
                .PrimaryKey(t => t.ConsumerRepCommitteeHistoryModelID)
                .ForeignKey("dbo.CommitteeModel", t => t.CommitteeModelID, cascadeDelete: true)
                .ForeignKey("dbo.ConsumerRepModel", t => t.ConsumerRepModelID, cascadeDelete: true)
                .Index(t => t.CommitteeModelID)
                .Index(t => t.ConsumerRepModelID);
            
            CreateTable(
                "dbo.ConsumerRepModel",
                c => new
                    {
                        ConsumerRepModelID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(),
                        MemberStatus = c.Int(),
                        DateTrained = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConsumerRepModelID);
            
            CreateTable(
                "dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel",
                c => new
                    {
                        ConsumerRepModel_ConsumerRepAreaOfInterestModelID = c.Int(nullable: false, identity: true),
                        ConsumerRepModelID = c.Int(nullable: false),
                        ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID = c.Int(),
                    })
                .PrimaryKey(t => t.ConsumerRepModel_ConsumerRepAreaOfInterestModelID)
                .ForeignKey("dbo.ConsumerRepAreaOfInterestModel", t => t.ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID)
                .ForeignKey("dbo.ConsumerRepModel", t => t.ConsumerRepModelID, cascadeDelete: true)
                .Index(t => t.ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID)
                .Index(t => t.ConsumerRepModelID);
            
            CreateTable(
                "dbo.ConsumerRepAreaOfInterestModel",
                c => new
                    {
                        ConsumerRepAreaOfInterestModelID = c.Int(nullable: false, identity: true),
                        AreaOfInterestName = c.String(),
                    })
                .PrimaryKey(t => t.ConsumerRepAreaOfInterestModelID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConsumerRepCommitteeHistoryModel", "ConsumerRepModelID", "dbo.ConsumerRepModel");
            DropForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepModelID", "dbo.ConsumerRepModel");
            DropForeignKey("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID", "dbo.ConsumerRepAreaOfInterestModel");
            DropForeignKey("dbo.ConsumerRepCommitteeHistoryModel", "CommitteeModelID", "dbo.CommitteeModel");
            DropForeignKey("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeModel_CommitteeModelID1", "dbo.CommitteeModel");
            DropForeignKey("dbo.CommitteeModel_CommitteeAreaOfHealthModel", "CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID", "dbo.CommitteeAreaOfHealthModel");
            DropIndex("dbo.ConsumerRepCommitteeHistoryModel", new[] { "ConsumerRepModelID" });
            DropIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", new[] { "ConsumerRepModelID" });
            DropIndex("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel", new[] { "ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID" });
            DropIndex("dbo.ConsumerRepCommitteeHistoryModel", new[] { "CommitteeModelID" });
            DropIndex("dbo.CommitteeModel_CommitteeAreaOfHealthModel", new[] { "CommitteeModel_CommitteeModelID1" });
            DropIndex("dbo.CommitteeModel_CommitteeAreaOfHealthModel", new[] { "CommitteeAreaOfHealthModel_CommitteeAreaOfHealthModelID" });
            DropTable("dbo.ConsumerRepAreaOfInterestModel");
            DropTable("dbo.ConsumerRepModel_ConsumerRepAreaOfInterestModel");
            DropTable("dbo.ConsumerRepModel");
            DropTable("dbo.ConsumerRepCommitteeHistoryModel");
            DropTable("dbo.CommitteeModel");
            DropTable("dbo.CommitteeAreaOfHealthModel");
            DropTable("dbo.CommitteeModel_CommitteeAreaOfHealthModel");
        }
    }
}
