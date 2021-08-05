namespace MVC_Activity02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Null_UpdatedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "UpdatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "UpdatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
