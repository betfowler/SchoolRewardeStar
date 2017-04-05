namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class again : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pledges", "Guardians_User_ID1", "dbo.Accounts");
            DropForeignKey("dbo.Pledges", "PledgeStatusID", "dbo.PledgeStatus");
            DropForeignKey("dbo.Pledges", "Students_User_ID1", "dbo.Accounts");
            DropIndex("dbo.Pledges", new[] { "PledgeStatusID" });
            DropIndex("dbo.Pledges", new[] { "Guardians_User_ID1" });
            DropIndex("dbo.Pledges", new[] { "Students_User_ID1" });
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
                        Students_User_ID = c.Int(nullable: false),
                        Guardians_User_ID = c.Int(nullable: false),
                        Guardians_User_ID1 = c.Int(),
                        Students_User_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.PledgeID);
            
            CreateIndex("dbo.Pledges", "Students_User_ID1");
            CreateIndex("dbo.Pledges", "Guardians_User_ID1");
            CreateIndex("dbo.Pledges", "PledgeStatusID");
            AddForeignKey("dbo.Pledges", "Students_User_ID1", "dbo.Accounts", "User_ID");
            AddForeignKey("dbo.Pledges", "PledgeStatusID", "dbo.PledgeStatus", "PledgeStatusID", cascadeDelete: true);
            AddForeignKey("dbo.Pledges", "Guardians_User_ID1", "dbo.Accounts", "User_ID");
        }
    }
}
