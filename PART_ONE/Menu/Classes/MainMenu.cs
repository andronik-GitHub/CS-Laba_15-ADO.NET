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
            Console.WriteLine("1 - Відображення всієї інформації з таблиці зі студентами та оцінками");
            Console.WriteLine("2 - Відображення ПІБ усіх студентів");
            Console.WriteLine("3 - Відображення всіх середніх оцінок");
            Console.WriteLine("4 - Показати ПІБ всіх студентів з мінімальною оцінкою, більше, ніж зазначена");
            Console.WriteLine("5 - Показати назву всіх предметів з мінімальними середніми оцінками");
            Console.WriteLine("6 - Показати мінімальну середню оцінку");
            Console.WriteLine("7 - Показати максимальну середню оцінку");
            Console.WriteLine("8 - Показати кількість студентів, у яких мінімальна середня оцінка з математики");
            Console.WriteLine("9 - Показати кількість студентів, у яких максимальна середня оцінка з математики");
            Console.WriteLine("10 - Показати кількість студентів у кожній групі");
            Console.WriteLine("Exit - Відключення від БД");

            switch (Console.ReadLine()?.ToLower().Trim())
            {
                case ("1"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintAllInformation(connection, "Студенти")); // реалізація інтерфейса IQuery
                    break;
                case ("2"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintAllName(connection, "Студенти"));
                    break;
                case ("3"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintAllGrades(connection, "Студенти"));
                    break;
                case ("4"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintAllNamesByRating(connection, "Студенти"));
                    break;
                case ("5"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintAllSubjectByRating(connection, "Студенти"));
                    break;
                case ("6"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintMinAVGRating(connection, "Студенти"));
                    break;
                case ("7"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintMaxAVGRating(connection, "Студенти"));
                    break;
                case ("8"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintCountStudentByMinSubject(connection, "Студенти"));
                    break;
                case ("9"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintCountStudentByMaxSubject(connection, "Студенти"));
                    break;
                case ("10"):
                    Console.WriteLine();
                    await Query.ResultQueryAsync(new PrintCountStudentByGroup(connection, "Студенти"));
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