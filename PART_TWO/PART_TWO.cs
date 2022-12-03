using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

class PART_TWO
{
    static async Task Main()
    {
        // Для відображення кирилиці
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        await CreateDB.Create();


        Console.ReadKey();
    }
}