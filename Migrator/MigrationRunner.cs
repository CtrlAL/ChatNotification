using MongoDB.Driver;

namespace Migrator
{
    public static class MigrationRunner
    {
        public static async Task RunMigrationsAsync(
            string connectionString,
            string databaseName,
            CancellationToken cancellationToken = default)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            var runner = new MigrationRunner(database, new[] { typeof(MigrationRunner).Assembly });

            var progress = new Progress<string>(msg => Console.WriteLine($"[MIGRATION] {msg}"));
            await runner.RunAsync(progress, cancellationToken);
        }
    }
}
