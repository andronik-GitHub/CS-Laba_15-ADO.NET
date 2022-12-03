using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

internal class NewConnection
{
    public static async Task Connection()
    {
        try // якщо БД [Оцінки студентів] створенно
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Оцінки студентів;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(); // асинхронно відкривається нове підключення

                // Повідомлення про успішне підключення
                Console.WriteLine("Пiдключення до бази [Оцінки студентів] завершено успішно");

                await Menu.ResultQueryAsync(new MainMenu(connection)); // меню з викликом запитів
            }

            // Повідомлення про успішне відключення
            Console.WriteLine("Відключення від бази [Оцінки студентів] завершено успішно");
        }
        catch (Exception e) // якщо БД [Оцінки студентів] не вдалось створити
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("\nНе вдалося підключитися до бази даних [Оцінки студентів]");
        }
    }

    
}