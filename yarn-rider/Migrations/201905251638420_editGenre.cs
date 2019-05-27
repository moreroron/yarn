namespace yarn_rider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editGenre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Genre", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "Genere");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Genere", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "Genre");
        }
    }
}
