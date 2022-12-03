using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

// Показати кількість студентів, я уких мінімальна середня оціна з Вищої математики
internal class PrintCountStudentByGroup : IQuery
{
    public SqlConnection connection { get; }
    public string table { get; }

    public PrintCountStudentByGroup(SqlConnection connection, string table)
    {
        this.connection = connection;
        this.table = table;
    }


    public async Task Query()
    {
        var command = new SqlCommand
        {
            Connection = connection, // підключення передається в команду

            // Tекст команди
            CommandText = $"SELECT [Група], COUNT([Група]) AS [Кількість студентів] FROM [{table}] GROUP BY [Група]"
        };
        var reader = await command.ExecuteReaderAsync(); // результат запиту записується в SQLDataReader

        if (reader.HasRows) // якщо було записано хоча б один рядок
        {
            // Витягуються імена стовпців
            string col1 = reader.GetName(0);
            string col2 = reader.GetName(1);

            // Константний займаючий простір(для коректного відображення в консолі в форматі таблиці)
            col1 = col1.PadRight(10, ' ');

            Console.WriteLine($"{col1}\t{col2}"); // вивід імен стовпців


            while (await reader.ReadAsync())
            {
                col1 = "" + reader.GetValue(0); // витягується значення стовпця
                col2 = "" + reader.GetValue(1);

                // Константний займаючий простір(для коректного відображення в консолі в форматі таблиці)
                col1 = col1.PadRight(10, ' ');

                Console.WriteLine($"{col1}\t{col2}"); // вивід значень стовпців
            }
        }

        await reader.CloseAsync();
    }
}