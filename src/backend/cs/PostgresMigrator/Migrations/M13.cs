using Consts;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    /// <summary>
    /// Миграция для bзменения типа данных CreatedAt в таблицах COMMIT и MAGNETOGRAM
    /// </summary>
    [Migration(13, $"Изменения типа данных CreatedAt в таблицах {PgTables.Commit} и {PgTables.Magnetogram}")]
    public class M13 : Migration
    {
        private static readonly string _commitTableName = PgTables.Commit;
        private static readonly string _magnetogramTableName = PgTables.Magnetogram;
        
        public override void Up()
        {
            // Проверка на существование таблицы перед созданием
            if (Schema.Schema(PgTables.Schema).Table(_commitTableName).Exists())
            {
                Execute.Sql($@"ALTER TABLE itcamp.{_commitTableName}
                            ALTER COLUMN createdat TYPE timestamptz
                            USING createdat::timestamptz;");
            }
            
            // Проверка на существование таблицы перед созданием
            if (Schema.Schema(PgTables.Schema).Table(_magnetogramTableName).Exists())
            {
                Execute.Sql($@"ALTER TABLE itcamp.{_magnetogramTableName}
                            ALTER COLUMN createdat TYPE timestamptz
                            USING createdat::timestamptz;");
            }
        }

        public override void Down()
        {
            
        }
    }
}
