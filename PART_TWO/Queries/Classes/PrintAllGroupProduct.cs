using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

// Відображення всіх типів канцтоварів
internal class PrintAllGroupProduct : IQuery
{
    public SqlConnection connection { get; }

    public PrintAllGroupProduct(SqlConnection connection) => this.connection = connection;


    public async Task Query() // приймається підключення і назва таблиці
    {
        var command = new SqlCommand
        {
            Connection = connection, // підключення передається в команду
            CommandText = "SELECT [Тип канцтовару] FROM [Канцтовари] GROUP BY [Тип канцтовару]" // текст команди
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