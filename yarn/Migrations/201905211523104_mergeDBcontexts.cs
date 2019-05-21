namespace yarn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mergeDBcontexts : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovieApplicationUsers", newName: "ApplicationUserMovies");
            DropPrimaryKey("dbo.ApplicationUserMovies");
            AddPrimaryKey("dbo.ApplicationUserMovies", new[] { "ApplicationUser_Id", "Movie_MovieID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ApplicationUserMovies");
            AddPrimaryKey("dbo.ApplicationUserMovies", new[] { "Movie_MovieID", "ApplicationUser_Id" });
            RenameTable(name: "dbo.ApplicationUserMovies", newName: "MovieApplicationUsers");
        }
    }
}
