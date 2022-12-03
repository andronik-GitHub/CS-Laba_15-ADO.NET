using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

internal class PrintProductByMaxCount : IQuery
{
    public SqlConnection connection { get; }

    public PrintProductByMaxCount(SqlConnection connection) => this.connection = connection;


    public async Task Query() // приймається підключення і назва таблиці
    {
        var command = new SqlCommand
        {
            Connection = connection, // підключення передається в команду
            CommandText = "SELECT [Тип канцтовару],MAX([Кількість]) AS [Макс. кількість] FROM [Канцтовари] GROUP BY [Тип канцтовару]" // текст команди
        };
        var reader = await command.ExecuteReaderAsync(); // результат запиту записується в SQLDataReader

        if (reader.HasRows) // якщо було записано хоча б один рядок
        {
            // Витягуються імена стовпців
            string col1 = reader.GetName(0);
            string col2 = reader.GetName(1);

            // Константний займаючий простір(для коректного відображення в консолі в форматі таблиці)
            col1 = col1.PadRight(30, ' ');
            col2 = col2.PadRight(10, ' ');

            Console.WriteLine($"{col1}\t{col2}"); // вивід імен стовпців


            while (await reader.ReadAsync())
            {
                // Витягуються значення стовпців
                col1 = (string)reader.GetValue(0);
                col2 = Convert.ToString(reader.GetValue(1));

                // Константний займаючий простір(для коректного відображення в консолі в форматі таблиці)
                col1 = col1.PadRight(30, ' ');
                col2 = col2.PadRight(10, ' ');

                Console.WriteLine($"{col1}\t{col2}"); // вивід значень стовпців
            }
        }

        await reader.CloseAsync();
    }
}