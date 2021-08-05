namespace MVC_Activity02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_tblCustomers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "UpdatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "UpdatedDate");
        }
    }
}
