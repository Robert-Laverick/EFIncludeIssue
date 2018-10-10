namespace EFIncludeIssue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MetaInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MetaInfoes", "DocumentId", "dbo.Documents");
            DropIndex("dbo.MetaInfoes", new[] { "DocumentId" });
            DropTable("dbo.MetaInfoes");
            DropTable("dbo.Documents");
        }
    }
}
