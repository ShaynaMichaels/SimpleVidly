namespace SimpleVidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateCustomers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Customers (Name, IsSubscribedToNewsletter, Birthdate, Membertype_Id) VALUES ('John Smith', 0, 10-09-1990, 2)");
            Sql("INSERT INTO Customers (Name, IsSubscribedToNewsletter, Birthdate, Membertype_Id) VALUES ('Yummi Smith', 1, 10-15-1985, 1)");

        }

        public override void Down()
        {
        }
    }
}
