namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pledges", "Guaridans_User_ID", "dbo.Accounts");
            DropForeignKey("dbo.Pledges", "PledgeStatusID", "dbo.PledgeStatus");
            DropForeignKey("dbo.Pledges", "Students_User_ID1", "dbo.Accounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Pledges",
                c => new
                    {
                        PledgeID = c.Int(nullable: false, identity: true),
                        Students_User_ID = c.Int(nullable: false),
                        Guardians_User_ID = c.Int(nullable: false),
                        Target = c.Int(nullable: false),
                        Deadline = c.DateTime(),
                        Title = c.String(),
                        Description = c.String(),
                        PledgeStatusID = c.Int(nullable: false),
                        Guaridans_User_ID = c.Int(),
                        Students_User_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.PledgeID);
            
            CreateIndex("dbo.Pledges", "Students_User_ID1");
            CreateIndex("dbo.Pledges", "Guaridans_User_ID");
            CreateIndex("dbo.Pledges", "PledgeStatusID");
            AddForeignKey("dbo.Pledges", "Students_User_ID1", "dbo.Accounts", "User_ID");
            AddForeignKey("dbo.Pledges", "PledgeStatusID", "dbo.PledgeStatus", "PledgeStatusID", cascadeDelete: true);
            AddForeignKey("dbo.Pledges", "Guaridans_User_ID", "dbo.Accounts", "User_ID");
        }
    }
}
