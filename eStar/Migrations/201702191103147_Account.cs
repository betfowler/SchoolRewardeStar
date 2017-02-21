namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Account : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        User_ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        Prefix = c.String(),
                        First_Name = c.String(),
                        Surname = c.String(),
                        User_Type = c.String(),
                        Job_Role = c.String(),
                        Weekly_Points = c.Int(),
                        Remaining_Points = c.Int(),
                        Year_Group = c.String(),
                        Tutor_Group = c.String(),
                        Total_Points = c.Int(),
                        Balance = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}
