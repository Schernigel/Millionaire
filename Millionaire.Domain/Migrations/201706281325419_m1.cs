namespace Millionaire.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TotalUserResult",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.Int(nullable: false),
                        TotalPrize = c.Int(nullable: false),
                        GameCount = c.Int(nullable: false),
                        PrizePerGame = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TotalUserResult");
        }
    }
}
