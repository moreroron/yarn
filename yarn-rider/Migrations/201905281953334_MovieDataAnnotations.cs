namespace yarn_rider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieDataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "MovieName", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "Date", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "PosterURL", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "MovieURL", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "MovieURL", c => c.String());
            AlterColumn("dbo.Movies", "PosterURL", c => c.String());
            AlterColumn("dbo.Movies", "Date", c => c.String());
            AlterColumn("dbo.Movies", "MovieName", c => c.String());
        }
    }
}
