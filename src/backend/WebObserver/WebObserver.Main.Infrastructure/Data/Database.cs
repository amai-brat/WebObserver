using System.Globalization;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace WebObserver.Main.Infrastructure.Data;

public static class Database
{
    public static async Task CreateHangfireDatabaseAsync(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Hangfire") 
                               ?? throw new NullReferenceException("Hangfire connection string is null");
        var dbName = GetDbName(connectionString);
        
        const string query = "SELECT 1 FROM pg_database WHERE datistemplate = false and datname = @name";
        await using (var connection = CreateMasterConnection(configuration))
        {
            var records = connection.Query(query, new { name = dbName });
            if (!records.Any())
                await connection.ExecuteAsync($"CREATE DATABASE \"{dbName}\"");
        }
    }

    private static NpgsqlConnection CreateMasterConnection(IConfiguration configuration) => 
        new(configuration.GetConnectionString("Master"));

    private static string GetDbName(string connectionString)
    {
        var kvp = connectionString.Split(";")
            .First(x => x.StartsWith("Database=", true, CultureInfo.InvariantCulture))
            .Split("=");
        
        return kvp[1];
    }
}