using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

internal class NewConnection
{
    public static async Task Connection()
    {
        try // якщо БД [Фірма канцтоварів] створенно
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Фірма канцтоварів;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(); // асинхронно відкривається нове підключення

                // Повідомлення про успішне підключення
                Console.WriteLine("Пiдключення до бази [Фірма канцтоварів] завершено успішно");

                await Menu.ResultQueryAsync(new MainMenu(connection)); // меню з викликом запитів
            }

            // Повідомлення про успішне відключення
            Console.WriteLine("Відключення від бази [Фірма канцтоварів] завершено успішно");
        }
        catch (Exception e) // якщо БД [Фірма канцтоварів] не вдалось створити
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("\nНе вдалося підключитися до бази даних [Фірма канцтоварів]");
        }
    }


}