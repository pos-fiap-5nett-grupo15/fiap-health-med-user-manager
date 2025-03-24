using FluentMigrator;

namespace Fiap.Health.Med.User.Manager.Infrastructure.Migrations
{
    [Migration(20250319212000)]
    public class AddTable : Migration
    {
        public override void Up()
        {
            Create.Table("TestTable")
                .InSchema("SandboxSchema")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).NotNullable();

        }

        public override void Down()
        {
            Delete.Table("TestTable").InSchema("SandboxSchema");
        }
    }
}
