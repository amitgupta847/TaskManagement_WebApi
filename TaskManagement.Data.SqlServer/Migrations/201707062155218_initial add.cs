namespace WebApi2Book.Data.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialadd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Ordinal = c.Int(nullable: false),
                        Version = c.Binary(),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Long(nullable: false, identity: true),
                        Subject = c.String(),
                        StartDate = c.DateTime(),
                        DueDate = c.DateTime(),
                        CompletedDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        Version = c.Binary(),
                        CreatedBy_UserId = c.Long(),
                        Status_StatusId = c.Long(),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Users", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.Status_StatusId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Username = c.String(),
                        Version = c.Binary(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.TaskUser",
                c => new
                    {
                        TaskId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.TaskId, t.UserId })
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TaskId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskUser", "UserId", "dbo.Users");
            DropForeignKey("dbo.TaskUser", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "Status_StatusId", "dbo.Status");
            DropForeignKey("dbo.Tasks", "CreatedBy_UserId", "dbo.Users");
            DropIndex("dbo.TaskUser", new[] { "UserId" });
            DropIndex("dbo.TaskUser", new[] { "TaskId" });
            DropIndex("dbo.Tasks", new[] { "Status_StatusId" });
            DropIndex("dbo.Tasks", new[] { "CreatedBy_UserId" });
            DropTable("dbo.TaskUser");
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
            DropTable("dbo.Status");
        }
    }
}
