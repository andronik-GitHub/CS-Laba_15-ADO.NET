using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

// Виведення всіх [ПІБ] з таблиці
internal class PrintAllName : IQuery
{
    public SqlConnection connection { get; }
    public string table { get; }

    public PrintAllName(SqlConnection connection, string table)
    {
        this.connection = connection;
        this.table = table;
    }


    public async Task Query()
    {
        var command = new SqlCommand
        {
            Connection = connection, // підключення передається в команду
            CommandText = $"SELECT [ПІБ] FROM [{table}]" // текст команди
        };
        var reader = await command.ExecuteReaderAsync(); // результат запиту записується в SQLDataReader

        if (reader.HasRows) // якщо було записано хоча б один рядок
        {
            Console.WriteLine($"{reader.GetName(0)}"); // вивід імен стовпців

            while (await reader.ReadAsync())
                Console.WriteLine($"{reader.GetValue(0)}"); // вивід значень стовпців
        }

        await reader.CloseAsync();
    }
}