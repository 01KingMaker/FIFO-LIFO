namespace LifoFifo.Models.orm;
using Npgsql;

public class SequenceValueGetter
{
    private string connectionString;

    public SequenceValueGetter()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        this.connectionString = configuration.GetConnectionString("DefaultConnection");
       // this.connectionString = connectionString;
    }

    public long GetNextSequenceValue(string sequenceName)
    {
        long nextValue = 0;

        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand($"SELECT nextval('{sequenceName}')", connection))
            {
                nextValue = (long)command.ExecuteScalar();
            }
        }

        return nextValue;
    }
}
