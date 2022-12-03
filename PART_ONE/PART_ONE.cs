using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

internal class PART_ONE
{
    static async Task Main()
    {
        // Для відображення кирилиці
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        await CreateDB.Create(); // створення БД, таблиці і заповнення її
        await NewConnection.Connection(); // підключення до новоствореної БД і виклик запитів до неї


        Console.ReadKey();
    }
}