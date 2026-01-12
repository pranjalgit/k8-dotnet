using DbUp;

namespace Media_MS.DbUp
{
    public class DbMigrator
    {
        public static void Run(string connectionString)
        {
            EnsureDatabase.For.MySqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .MySqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(
                        typeof(DbMigrator).Assembly)
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new Exception("Database migration failed", result.Error);
            }
        }
    }
}
