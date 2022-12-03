using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

internal class MainMenu : IMenu
{
    public SqlConnection connection { get; }

    public MainMenu(SqlConnection connection) => this.connection = connection;


    public async Task OutputMenu()
    {
        while (true) // міні-менюшка
        {
            Console.WriteLine("1 - Відображення всієї інформації про канцтовари");
            Console.WriteLine("2 - Відображення всіх типів канцтоварів");
            Console.WriteLine("3 - Відображення всіх менеджерів з продажу");
            Console.WriteLine("4 - Показати канцтовари з максимальною кількістю одиниць");
            Console.WriteLine("5 - Показати канцтовари з мінімальною кількістю одиниць");
            Console.WriteLine("6 - Показати канцтовари з мінімальною собівартістю одиниці");
            Console.WriteLine("7 - Показати канцтовари з максимальною собівартістю одиниці");
            Console.WriteLine("Exit - Відключення від БД");

            switch (Console.ReadLine()?.ToLower().Trim())
            {
                case ("1"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintAllStationeryInformation(connection)); // реалізація інтерфейса IQuery
                    break;
                case ("2"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintAllGroupProduct(connection));
                    break;
                case ("3"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintAllManagers(connection));
                    break;
                case ("4"):
                    Console.WriteLine();
                    break;
                case ("5"):
                    Console.WriteLine();
                    break;
                case ("6"):
                    Console.WriteLine();
                    break;
                case ("7"):
                    Console.WriteLine();
                    break;
                case ("exit"):
                    Console.WriteLine();
                    return;
                default:
                    Console.Write("\nВведено некоректне значення...");
                    break;
            }

            Console.ReadKey(); // щоб можна було продивитись результат команди
            Console.Clear(); // очищення консолі
        }
    }
}