using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

// Виведення всієї інформації з таблиці
internal class PrintAllInformation : IQuery
{
    public SqlConnection connection { get; }
    public string table { get; }

    public PrintAllInformation(SqlConnection connection, string table)
    {
        this.connection = connection;
        this.table = table;
    }


    public async Task Query()
    {
        var command = new SqlCommand
        {
            Connection = connection, // підключення передається в команду
            CommandText = $"SELECT * FROM [{table}]" // текст команди
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
            string col6 = reader.GetName(5);
            string col7 = reader.GetName(6);

            // Константний займаючий простір(для коректного відображення в консолі в форматі таблиці)
            col1 = col1.PadRight(30, ' ');
            col2 = col2.PadRight(5, ' ');
            col3 = col3.PadRight(15, ' ');
            col4 = col4.PadRight(15, ' ');
            col5 = col5.PadRight(15, ' ');
            col6 = col6.PadRight(30, ' ');
            col7 = col7.PadRight(30, ' ');

            Console.WriteLine($"{col1}\t{col2}\t{col3}\t{col4}\t{col5}\t{col6}\t{col7}"); // вивід імен стовпців


            while(await reader.ReadAsync())
            {
                // Витягуються значення стовпців
                col1 = (string)reader.GetValue(0);
                col2 = (string)reader.GetValue(1);
                col3 = Convert.ToString(reader.GetValue(2));
                col4 = Convert.ToString(reader.GetValue(3));
                col5 = Convert.ToString(reader.GetValue(4));
                col6 = (string)reader.GetValue(5);
                col7 = (string)reader.GetValue(6);

                // Константний займаючий простір(для коректного відображення в консолі в форматі таблиці)
                col1 = col1.PadRight(30, ' ');
                col2 = col2.PadRight(5, ' ');
                col3 = col3.PadRight(15, ' ');
                col4 = col4.PadRight(15, ' ');
                col5 = col5.PadRight(15, ' ');
                col6 = col6.PadRight(30, ' ');
                col7 = col7.PadRight(30, ' ');

                Console.WriteLine($"{col1}\t{col2}\t{col3}\t{col4}\t{col5}\t\t{col6}\t{col7}"); // вивід значень стовпців
            }
        }

        await reader.CloseAsync(); // закривається об'єкт SQLDataReader
    }
}