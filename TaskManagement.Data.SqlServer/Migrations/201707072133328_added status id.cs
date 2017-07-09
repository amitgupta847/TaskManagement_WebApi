namespace WebApi2Book.Data.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstatusid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Status_StatusId", "dbo.Status");
            DropIndex("dbo.Tasks", new[] { "Status_StatusId" });
            RenameColumn(table: "dbo.Tasks", name: "Status_StatusId", newName: "StatusId");
            AlterColumn("dbo.Tasks", "StatusId", c => c.Long(nullable: false));
            CreateIndex("dbo.Tasks", "StatusId");
            AddForeignKey("dbo.Tasks", "StatusId", "dbo.Status", "StatusId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "StatusId", "dbo.Status");
            DropIndex("dbo.Tasks", new[] { "StatusId" });
            AlterColumn("dbo.Tasks", "StatusId", c => c.Long());
            RenameColumn(table: "dbo.Tasks", name: "StatusId", newName: "Status_StatusId");
            CreateIndex("dbo.Tasks", "Status_StatusId");
            AddForeignKey("dbo.Tasks", "Status_StatusId", "dbo.Status", "StatusId");
        }
    }
}
