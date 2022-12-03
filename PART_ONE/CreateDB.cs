using Microsoft.Data.SqlClient;
using System;

internal class CreateDB
{
    public static async Task Create()
    {
        // Строка підключення
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;";

        // using для автоматичного закриття підключення
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync(); // асинхронно відкривається підключення

            var command = new SqlCommand() { Connection = connection }; // для виконування команд

            try
            {
                command.CommandText = // створення БД
                    "CREATE DATABASE [Оцінки студентів] ";
                await command.ExecuteNonQueryAsync(); // асинхронне виконування команди

                Console.WriteLine("Базу даних створено успішно");
            }
            catch (SqlException e) // якщо створення БД не получилось
            {
                Console.WriteLine(e.Message);

                Console.Write("\nEnter for continue...");
                Console.ReadKey(); Console.Clear();
            }

            // строка підключення змінюється на новостворену базу данних
            connectionString = "Server=(localdb)\\mssqllocaldb;Database=Оцінки студентів;Trusted_Connection=True;";
        }

        try // якщо БД [Оцінки студентів] вдалось створити
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(); // асинхронно відкривається нове підключення

                var command = new SqlCommand() { Connection = connection };
                try
                {
                    command.CommandText = // створення таблиці
                        "CREATE TABLE [Студенти] " +
                        "( " +
                            "[ПІБ] NVARCHAR(50) NOT NULL, " +
                            "[Група] NVARCHAR(10) NOT NULL, " +
                            "[Середній бал] DECIMAL(5,2) NOT NULL, " +
                            "[Мінімальний бал] DECIMAL(5,2) NOT NULL, " +
                            "[Максимальний бал] DECIMAL(5,2) NOT NULL, " +
                            "[Предмет з мін. балом] NVARCHAR(50) NOT NULL, " +
                            "[Предмет з макс. балом] NVARCHAR(50) NOT NULL, " +
                            " " +
                            "PRIMARY KEY ([ПІБ]) " +
                        ")";
                    await command.ExecuteNonQueryAsync();

                    Console.WriteLine("Таблицю створено успішно");
                }
                catch (SqlException e) // якщо створення БД не получилось
                {
                    Console.WriteLine(e.Message);

                    Console.Write("\nEnter for continue...");
                    Console.ReadKey(); Console.Clear();
                }

                try
                {
                     command.CommandText = // заповнення таблиці
                        "INSERT INTO [Студенти] " +
                        "   VALUES  (N'Цвігун Ігор Петрович',            N'242-Б',    54.5,     15.5,   75.8,   N'Математичний аналіз',         N'Теорія алгоритмів')," +
                        "           (N'Грицюк Олександра Олегівна',      N'244-А',    72.5,     71.9,   83.1,   N'Операційні системи',          N'ООП')," +
                        "           (N'Яремак Влад Леонтович',           N'241-A',    58.2,     48.2,   69.4,   N'Мережі',                      N'Java Script')," +
                        "           (N'Крупчак Анастасія Володимирівна', N'243-А',    58.5,     51.6,   78.7,   N'Теорія алгоритмів',           N'ООП')," +
                        "           (N'Минзат Валентин Володимирович',   N'244-А',    30.1,     8.1,    39.4,   N'Теорія ймовірності',          N'Теорія алгоритмів')," +
                        "           (N'Колчанова Тетяна Олександрівна',  N'243-Б',    64.2,     63.2,   72.2,   N'Java',                        N'БД')," +
                        "           (N'Зварко Вікторія Станіславівна',   N'244-А',    54.9,     57.7,   64.5,   N'Java Script',                 N'ООП')," +
                        "           (N'Веркаш Віталій Юрійович',         N'241-А',    75.7,     68.9,   92.2,   N'БД',                          N'Математичний аналіз')," +
                        "           (N'Мачик Назарій Анатолійович',      N'244-А',    51.8,     45.4,   61.0,   N'Теорія алгоритмів',           N'ООП')," +
                        "           (N'Макогон Роман Павлович',          N'241-А',    55.5,     56.6,   78.4,   N'Теорія алгоритмів',           N'Теорія ймовірності')," +
                        "           (N'Кульчицький Андрій Сергійович',   N'244-А',    59.7,     63.3,   83.6,   N'Операційні системи',          N'Java Script')," +
                        "           (N'Андрищак Олександр Орландович',   N'242-Б',    85.3,     81.2,   97.0,   N'С',                           N'Вища математика')," +
                        "           (N'Мельничук Анна Олександрівна',    N'244-Б',    71.5,     59.9,   88.8,   N'ООП',                         N'БД')," +
                        "           (N'Сиримак Марина Віталіївна',       N'241-Б',    53.1,     35.2,   70.2,   N'Вища математика',             N'С++')," +
                        "           (N'Арнаутов Кирил Георгійович',      N'243-А',    62.9,     53.6,   92.4,   N'Статистичний аналіз даних',   N'С++')," +
                        "           (N'Ткач Євгеній Анатолійович',       N'244-А',    85.0,     65.5,   97.3,   N'БД',                          N'ООП')," +
                        "           (N'Задорожна Марія Олександрівна',   N'242-Б',    64.8,     72.2,   93.8,   N'Вища математика',             N'Філософія')," +
                        "           (N'Сташко Іванна Іванівна',          N'244-Б',    89.4,     81.8,   95.3,   N'Теорія алгоритмів',           N'ООП')," +
                        "           (N'Іванов Влад Володимирович',       N'241-A',    63.0,     27.2,   63.8,   N'Теорія ймовірності',          N'Основи керуванння БД');";

                    /*
                    command.CommandText =
                        "INSERT INTO [Студенти] VALUES (N'',N'',0.0,0.0,0.0,N'',N'')";
                     */

                    await command.ExecuteNonQueryAsync();

                    Console.WriteLine("Таблицю заповненo успішно");
                    Console.ReadKey(); Console.Clear();
                }
                catch (SqlException e) // якщо створення БД не получилось
                {
                    Console.WriteLine(e.Message);

                    Console.Write("\nEnter for continue...");
                    Console.ReadKey(); Console.Clear();
                }
            }
        }
        catch (Exception e) // якщо БД [Оцінки студентів] не вдалось створити
        {
            Console.WriteLine(e.Message);

            Console.Write("\nEnter for continue...");
            Console.ReadKey(); Console.Clear();
        }
    }
}