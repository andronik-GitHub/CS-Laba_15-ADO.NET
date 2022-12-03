using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

// Показати кількість студентів, я уких мінімальна середня оціна з Вищої математики
internal class PrintCountStudentByMaxSubject : IQuery
{
    public SqlConnection connection { get; }
    public string table { get; }

    public PrintCountStudentByMaxSubject(SqlConnection connection, string table)
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
            CommandText = $"SELECT COUNT(*) FROM [{table}] WHERE [Предмет з макс. балом] = N'Вища математика'"
        };

        object? count = await command.ExecuteScalarAsync(); // результат агрегатної функції

        Console.WriteLine($"Кількість студентів, я уких максимальна середня оціна з Вищої математики: {count}");
    }
}