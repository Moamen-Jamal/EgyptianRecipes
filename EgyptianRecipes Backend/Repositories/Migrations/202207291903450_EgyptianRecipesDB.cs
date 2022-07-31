namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EgyptianRecipesDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerBranch",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        BranchID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branch", t => t.BranchID, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.BranchID);
            
            DropColumn("dbo.Branch", "CustomerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Branch", "CustomerID", c => c.Int());
            DropForeignKey("dbo.CustomerBranch", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.CustomerBranch", "BranchID", "dbo.Branch");
            DropIndex("dbo.CustomerBranch", new[] { "BranchID" });
            DropIndex("dbo.CustomerBranch", new[] { "CustomerID" });
            DropTable("dbo.CustomerBranch");
        }
    }
}
