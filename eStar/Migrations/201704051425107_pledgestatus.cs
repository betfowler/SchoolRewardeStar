namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pledgestatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PledgeStatus",
                c => new
                    {
                        PledgeStatusID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.PledgeStatusID);
            
            AddColumn("dbo.Pledges", "PledgeStatusID", c => c.Int(nullable: false));
            AddColumn("dbo.Pledges", "Guaridans_User_ID", c => c.Int());
            AddColumn("dbo.Pledges", "Students_User_ID1", c => c.Int());
            CreateIndex("dbo.Pledges", "PledgeStatusID");
            CreateIndex("dbo.Pledges", "Guaridans_User_ID");
            CreateIndex("dbo.Pledges", "Students_User_ID1");
            AddForeignKey("dbo.Pledges", "Guaridans_User_ID", "dbo.Accounts", "User_ID");
            AddForeignKey("dbo.Pledges", "Students_User_ID1", "dbo.Accounts", "User_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pledges", "Students_User_ID1", "dbo.Accounts");
            DropForeignKey("dbo.Pledges", "PledgeStatusID", "dbo.PledgeStatus");
            DropForeignKey("dbo.Pledges", "Guaridans_User_ID", "dbo.Accounts");
            DropIndex("dbo.Pledges", new[] { "Students_User_ID1" });
            DropIndex("dbo.Pledges", new[] { "Guaridans_User_ID" });
            DropIndex("dbo.Pledges", new[] { "PledgeStatusID" });
            DropColumn("dbo.Pledges", "Students_User_ID1");
            DropColumn("dbo.Pledges", "Guaridans_User_ID");
            DropColumn("dbo.Pledges", "PledgeStatusID");
            DropTable("dbo.PledgeStatus");
        }
    }
}
