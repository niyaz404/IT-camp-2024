using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для создания таблицы STRUCTURALELEMENT
    /// </summary>
    [Migration(4, $"Создание таблицы {PgTables.StructuralElement}")]
    public class M4 : Migration
    {
        private static readonly string _tableName = PgTables.StructuralElement;
        private static readonly string _elementTypeTableName = PgTables.StructuralElementType;
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (!Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Create.Table(_tableName).InSchema(PgTables.Schema)
                    .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                    .WithColumnDescription("Идентификатор элемента")

                    .WithColumn("elementtypeid").AsInt32().NotNullable()
                    .ForeignKey("elementtypeid", PgTables.Schema, _elementTypeTableName, "id")
                    .WithColumnDescription("Идентификатор типа структурного элемента")

                    .WithColumn("startx").AsInt32().NotNullable()
                    .WithColumnDescription("Координата X начала области")

                    .WithColumn("endx").AsInt32().NotNullable()
                    .WithColumnDescription("Координата X конца области");
            }
        }

        public override void Down()
        {
            // Проверка на существование таблицы перед удалением
            if (Schema.Schema(PgTables.Schema).Table(_tableName).Exists())
            {
                Delete.Table(_tableName).InSchema(PgTables.Schema);
            }
        }
    }
}
