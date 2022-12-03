using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

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
                    "CREATE DATABASE [Фірма канцтоварів] ";
                await command.ExecuteNonQueryAsync(); // асинхронне виконування команди

                Console.WriteLine("Базу даних створено успішно");
            }
            catch (SqlException e) // якщо створення БД не получилось
            {
                Console.WriteLine(e.Message);

                Console.Write("\nНатисніть для продовження...");
                Console.ReadKey(); Console.Clear();
            }

            // строка підключення змінюється на новостворену базу данних
            connectionString = "Server=(localdb)\\mssqllocaldb;Database=Фірма канцтоварів;Trusted_Connection=True;";
        }

        try // якщо БД [Фірма канцтоварів] вдалось створити
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(); // асинхронно відкривається нове підключення

                var command = new SqlCommand() { Connection = connection };
                try
                {
                    command.CommandText = // створення таблиці [Менеджери]
                        "CREATE TABLE [Менеджери] " +
                        "( " +
                        "   [ID] INT NOT NULL IDENTITY(1000,1), " +
                        "   [Ім'я менеджера] NVARCHAR(50) NOT NULL, " +
                        "   [Заробітня плата] MONEY NOT NULL, " +
                        " " +
                        "   PRIMARY KEY ([ID]) " +
                        ")";
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Таблицю [Менеджери] створено успішно");


                    command.CommandText = // створення таблиці [Канцтовари]
                        "CREATE TABLE [Канцтовари] " +
                        "( " +
                        "   [Назва канцтовара] NVARCHAR(50) NOT NULL, " +
                        "   [Тип канцтовару] NVARCHAR(30) NOT NULL, " +
                        "   [Кількість] INT NOT NULL, " +
                        "   [ID менеджера] INT NOT NULL, " +
                        "   [Собівартість] MONEY NOT NULL, " +
                        " " +
                        "   PRIMARY KEY ([Назва канцтовара],[Тип канцтовару]), " +
                        "   CONSTRAINT FK_Канцтовари_To_Менеджери FOREIGN KEY([ID менеджера]) REFERENCES Менеджери ([ID]) ON DELETE CASCADE " +
                        ")";
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Таблицю [Канцтовари] створено успішно");


                    command.CommandText = // створення таблиці [Замовлення]
                        "CREATE TABLE [Замовлення] " +
                        "( " +
                        "   [ID замовлення] INT NOT NULL IDENTITY(1000,1), " +
                        "   [Назва фірми покупця] NVARCHAR(50) NOT NULL, " +
                        "   [Назва канцтовара] NVARCHAR(50) NOT NULL, " +
                        "   [Тип канцтовару] NVARCHAR(30) NOT NULL, " +
                        "   [Кількість проданих канцтоварів] INT NOT NULL, " +
                        "   [ID менеджера] INT NOT NULL, " +
                        "   [Ціна замовлення] MONEY NOT NULL, " +
                        "   [Дата продажу] DATE NOT NULL DEFAULT GETDATE(), " +
                        " " +
                        "   PRIMARY KEY ([ID замовлення]), " +
                        "   CONSTRAINT FK_Замовлення_To_Менеджери FOREIGN KEY([ID менеджера]) REFERENCES Менеджери ([ID]) ON DELETE CASCADE, " +
                        "   CONSTRAINT FK_Замовлення_To_Канцтовари FOREIGN KEY([Назва канцтовара],[Тип канцтовару])" +
                        "               REFERENCES Канцтовари ([Назва канцтовара],[Тип канцтовару]) ON DELETE NO ACTION " +
                        ")";
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Таблицю [Замовлення] створено успішно");
                }
                catch (SqlException e) // якщо створення таблиць не получилось
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

                    Console.Write("\nНатисніть для продовження...");
                    Console.ReadKey(); Console.Clear();
                }
            }
        }
        catch (Exception e) // якщо БД [Фірма канцтоварів] не вдалось створити
        {
            Console.WriteLine(e.Message);

            Console.Write("\nНатисніть для продовження...");
            Console.ReadKey(); Console.Clear();
        }
    }
}