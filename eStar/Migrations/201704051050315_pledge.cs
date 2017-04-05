namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pledge : DbMigration
    {
        public override void Up()
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
                    })
                .PrimaryKey(t => t.PledgeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pledges");
        }
    }
}
