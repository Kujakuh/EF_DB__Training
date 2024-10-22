using Microsoft.EntityFrameworkCore;

namespace EF_DB
{
    public enum DbType
    {
        POSTGRE,
        SQLSERVER
    }

    internal class DBManagementUtils
    {
        public static void DropRecords<T>(DbContext db, DbType serverType) where T : class
        {
            var table = db.Set<T>();
            if (!table.Any()) return;

            table.RemoveRange(table);
            db.SaveChanges();

            ResetIdentity<T>(db, serverType);
        }

        public static void AddRecordsIgnoreKey<T>(DbContext db, T[] records) where T : class
        {
            var entityType = db.Model.FindEntityType(typeof(T));
            var key = entityType.FindPrimaryKey().Properties.FirstOrDefault();

            if (key == null) return;

            foreach (var item in records)
            {
                var entry = db.Entry(item);

                // Key previous value is disposable
                // Let db generate a new value for the field
                entry.Property(key.Name).IsTemporary = true;
                // Gen insert command
                entry.State = EntityState.Added;
            }
            db.SaveChanges();

        }

        public static void ResetIdentity<T>(DbContext db, DbType serverType) where T : class
        {
            var entityType = db.Model.FindEntityType(typeof(T));
            var tableName = entityType.GetTableName();
            var key = entityType.FindPrimaryKey().Properties.FirstOrDefault();

            switch (serverType)
            {
                case DbType.POSTGRE:
                    var pgSchema = entityType.GetSchema() ?? "public";
                    db.Database.ExecuteSqlRaw($"ALTER TABLE \"{pgSchema}\".\"{tableName}\" ALTER COLUMN \"{key.Name}\" RESTART SET START 1");
                    break;

                case DbType.SQLSERVER:
                    var sqlsSchema = entityType.GetSchema() ?? "dbo";
                    db.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('{sqlsSchema}.{tableName}', RESEED, 0);");
                    break;
            }
        }
    }
}
