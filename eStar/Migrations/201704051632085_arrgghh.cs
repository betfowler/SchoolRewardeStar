namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class arrgghh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pledges",
                c => new
                    {
                        PledgeID = c.Int(nullable: false, identity: true),
                        Target = c.Int(nullable: false),
                        Deadline = c.DateTime(),
                        Title = c.String(),
                        Description = c.String(),
                        PledgeStatusID = c.Int(nullable: false),
                        Students_User_ID = c.Int(nullable: false),
                        Guardians_User_ID = c.Int(nullable: false),
                        Guardians_User_ID1 = c.Int(),
                        Students_User_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.PledgeID)
                .ForeignKey("dbo.Accounts", t => t.Guardians_User_ID1)
                .ForeignKey("dbo.PledgeStatus", t => t.PledgeStatusID, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Students_User_ID1)
                .Index(t => t.PledgeStatusID)
                .Index(t => t.Guardians_User_ID1)
                .Index(t => t.Students_User_ID1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pledges", "Students_User_ID1", "dbo.Accounts");
            DropForeignKey("dbo.Pledges", "PledgeStatusID", "dbo.PledgeStatus");
            DropForeignKey("dbo.Pledges", "Guardians_User_ID1", "dbo.Accounts");
            DropIndex("dbo.Pledges", new[] { "Students_User_ID1" });
            DropIndex("dbo.Pledges", new[] { "Guardians_User_ID1" });
            DropIndex("dbo.Pledges", new[] { "PledgeStatusID" });
            DropTable("dbo.Pledges");
        }
    }
}
