namespace WorkoutLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkoutId : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Workouts", newName: "UserWorkouts");
            DropPrimaryKey("dbo.UserWorkouts");
            AddColumn("dbo.UserWorkouts", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserWorkouts", "UserEmail", c => c.String());
            AddPrimaryKey("dbo.UserWorkouts", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserWorkouts");
            AlterColumn("dbo.UserWorkouts", "UserEmail", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.UserWorkouts", "Id");
            AddPrimaryKey("dbo.UserWorkouts", "UserEmail");
            RenameTable(name: "dbo.UserWorkouts", newName: "Workouts");
        }
    }
}
