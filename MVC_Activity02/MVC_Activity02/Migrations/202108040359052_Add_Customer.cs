namespace MVC_Activity02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Customer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(maxLength: 64),
                        Lastname = c.String(maxLength: 64),
                        Address = c.String(maxLength: 100),
                        Status = c.String(maxLength: 10),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
