namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comeon : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pledges", "Guardians_User_ID", "dbo.Accounts");
            DropForeignKey("dbo.Pledges", "PledgeStatusID", "dbo.PledgeStatus");
            DropForeignKey("dbo.Pledges", "Students_User_ID", "dbo.Accounts");
            DropIndex("dbo.Pledges", new[] { "PledgeStatusID" });
            DropIndex("dbo.Pledges", new[] { "Guardians_User_ID" });
            DropIndex("dbo.Pledges", new[] { "Students_User_ID" });
            DropTable("dbo.Pledges");
        }
        
        public override void Down()
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
                        Guardians_User_ID = c.Int(),
                        Students_User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.PledgeID);
            
            CreateIndex("dbo.Pledges", "Students_User_ID");
            CreateIndex("dbo.Pledges", "Guardians_User_ID");
            CreateIndex("dbo.Pledges", "PledgeStatusID");
            AddForeignKey("dbo.Pledges", "Students_User_ID", "dbo.Accounts", "User_ID");
            AddForeignKey("dbo.Pledges", "PledgeStatusID", "dbo.PledgeStatus", "PledgeStatusID", cascadeDelete: true);
            AddForeignKey("dbo.Pledges", "Guardians_User_ID", "dbo.Accounts", "User_ID");
        }
    }
}
