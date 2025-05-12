using FluentMigrator;

namespace Fiap.Health.Med.User.Manager.Infrastructure.Migrations
{
    [Migration(20250327220700)]
    public class CreatePatientsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Patients")
                .InSchema("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(200).NotNullable()
                .WithColumn("Document").AsString(200).NotNullable()
                .WithColumn("HashedPassword").AsString(500).NotNullable()
                .WithColumn("Email").AsString(200);

            Create.Index("IX_Patients_Document")
                .OnTable("Patients")
                .InSchema("Users")
                .OnColumn("Document")
                .Ascending();
        }

        public override void Down()
        {
            Delete.Table("Patients").InSchema("Users");
        }
    }
}
