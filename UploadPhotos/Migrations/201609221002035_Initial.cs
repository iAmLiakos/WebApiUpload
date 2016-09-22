namespace UploadPhotos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Emotion", "LocationId", "dbo.Location");
            //DropPrimaryKey("dbo.Location");
            //AlterColumn("dbo.Location", "Id", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("dbo.Location", "Id");
            //AddForeignKey("dbo.Emotion", "LocationId", "dbo.Location", "Id");
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Emotion", "LocationId", "dbo.Location");
            //DropPrimaryKey("dbo.Location");
            //AlterColumn("dbo.Location", "Id", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.Location", "Id");
            //AddForeignKey("dbo.Emotion", "LocationId", "dbo.Location", "Id");
        }
    }
}
