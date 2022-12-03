using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

// Виведення всіх предметів за мінімальним рейтингом
internal class PrintAllSubjectByRating : IQuery
{
    public SqlConnection connection { get; }
    public string table { get; }

    public PrintAllSubjectByRating(SqlConnection connection, string table)
    {
        this.connection = connection;
        this.table = table;
    }


    public async Task Query()
    {

        var command = new SqlCommand
        {
            Connection = connection, // підключення передається в команду
            CommandText = $"SELECT [Предмет з мін. балом],AVG([Мінімальний бал]) AS [Середній бал] FROM [{table}] GROUP BY [Предмет з мін. балом]" // текст команди
        };
        var reader = await command.ExecuteReaderAsync(); // результат запиту записується в SQLDataReader

        if (reader.HasRows) // якщо було записано хоча б один рядок
        {
            // Витягуються імена стовпців
            string col1 = reader.GetName(0);
            string col2 = reader.GetName(1);

            // Константний займаючий простір(для коректного відображення в консолі в форматі таблиці)
            col1 = col1.PadRight(30, ' ');
            col2 = col2.PadRight(5, ' ');

            Console.WriteLine($"{col1}\t{col2}"); // вивід імен стовпців


            while (await reader.ReadAsync())
            {
                col1 = (string)reader.GetValue(0); // витягується значення стовпця
                col2 = Convert.ToString(reader.GetValue(1));

                // Константний займаючий простір(для коректного відображення в консолі в форматі таблиці)
                col1 = col1.PadRight(30, ' ');

                Console.WriteLine($"{col1}\t{col2}"); // вивід значень стовпців
            }
        }

        await reader.CloseAsync();
    }
}