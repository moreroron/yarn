namespace yarn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserIDname : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "Id", newName: "UserID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Users", name: "UserID", newName: "Id");
        }
    }
}
