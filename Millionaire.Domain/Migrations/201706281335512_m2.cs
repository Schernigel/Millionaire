namespace Millionaire.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TotalUserResult", "Name", c => c.String(nullable: false));
            DropColumn("dbo.TotalUserResult", "PrizePerGame");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TotalUserResult", "PrizePerGame", c => c.Int(nullable: false));
            AlterColumn("dbo.TotalUserResult", "Name", c => c.Int(nullable: false));
        }
    }
}
