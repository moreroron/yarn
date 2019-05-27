namespace yarn_rider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGenre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Genere", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Genere");
        }
    }
}
