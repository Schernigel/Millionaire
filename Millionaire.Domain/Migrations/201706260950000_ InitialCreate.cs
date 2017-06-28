namespace Millionaire.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        First = c.String(nullable: false, maxLength: 400),
                        Second = c.String(nullable: false, maxLength: 400),
                        Third = c.String(nullable: false, maxLength: 400),
                        Fourth = c.String(nullable: false, maxLength: 400),
                        Сorrect = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameQuestion", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.GameQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(nullable: false, maxLength: 400),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultsId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        QuestionNumber = c.Int(nullable: false),
                        Prize = c.Int(nullable: false),
                        FiftyFifty = c.Int(nullable: false),
                        Call = c.Int(nullable: false),
                        ViewersHelp = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResultsId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.UserStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        AnswerNumber = c.Int(nullable: false),
                        Prompt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "Id", "dbo.GameQuestion");
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.Results", new[] { "UserId" });
            DropIndex("dbo.Answer", new[] { "Id" });
            DropTable("dbo.UserStatistics");
            DropTable("dbo.User");
            DropTable("dbo.Results");
            DropTable("dbo.GameQuestion");
            DropTable("dbo.Answer");
        }
    }
}
