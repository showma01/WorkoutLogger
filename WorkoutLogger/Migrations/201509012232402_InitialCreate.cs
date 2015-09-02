namespace WorkoutLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        UserEmail = c.String(nullable: false, maxLength: 128),
                        WorkoutType = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                        Intensity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserEmail);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Workouts");
        }
    }
}
