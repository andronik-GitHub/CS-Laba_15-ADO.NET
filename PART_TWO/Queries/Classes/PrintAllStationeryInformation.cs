using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

// Відображення всієї інформації про канцтовари
internal class PrintAllStationeryInformation : IQuery
{
    public SqlConnection connection { get; }

    public PrintAllStationeryInformation(SqlConnection connection) => this.connection = connection;


    public async Task Query() // приймається підключення і назва таблиці
    {
        var command = new SqlCommand
        {
            Connection = connection, // підключення передається в команду
            CommandText = "SELECT * FROM [Канцтовари]" // текст команди
        };
        var reader = await command.ExecuteReaderAsync(); // результат запиту записується в SQLDataReader

        if (reader.HasRows) // якщо було записано хоча б один рядок
        {
            // Витягуються імена стовпців
            string col1 = reader.GetName(0);
            string col2 = reader.GetName(1);
            string col3 = reader.GetName(2);
            string col4 = reader.GetName(3);
            string col5 = reader.GetName(4);

            // Константний займаючий простір(для коректного відображення в консолі в форматі таблиці)
            col1 = col1.PadRight(80, ' ');
            col2 = col2.PadRight(30, ' ');
            col3 = col3.PadRight(15, ' ');
            col4 = col4.PadRight(15, ' ');
            col5 = col5.PadRight(15, ' ');

            Console.WriteLine($"{col1}\t{col2}\t{col3}\t{col4}\t{col5}"); // вивід імен стовпців


            while (await reader.ReadAsync())
            {
                // Витягуються значення стовпців
                col1 = (string)reader.GetValue(0);
                col2 = (string)reader.GetValue(1);
                col3 = Convert.ToString(reader.GetValue(2));
                col4 = Convert.ToString(reader.GetValue(3));
                col5 = Convert.ToString(reader.GetValue(4));

                // Константний займаючий простір(для коректного відображення в консолі в форматі таблиці)
                col1 = col1.PadRight(80, ' ');
                col2 = col2.PadRight(30, ' ');
                col3 = col3.PadRight(15, ' ');
                col4 = col4.PadRight(15, ' ');
                col5 = col5.PadRight(15, ' ');

                Console.WriteLine($"{col1}\t{col2}\t{col3}\t{col4}\t{col5}"); // вивід значень стовпців
            }
        }

        await reader.CloseAsync();
    }
}