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
                    command.CommandText = // заповнення таблиці [Менеджери]
                       "INSERT INTO [Менеджери] " +
                       "   VALUES  (N'Цвігун Ігор Петрович',            7200)," +
                       "           (N'Грицюк Олександра Олегівна',      6800)," +
                       "           (N'Яремак Влад Леонтович',           8500)," +
                       "           (N'Крупчак Анастасія Володимирівна', 12300)," +
                       "           (N'Минзат Валентин Володимирович',   10250)," +
                       "           (N'Колчанова Тетяна Олександрівна',  9000)," +
                       "           (N'Зварко Вікторія Станіславівна',   6900)," +
                       "           (N'Веркаш Віталій Юрійович',         7425)," +
                       "           (N'Мачик Назарій Анатолійович',      8120)," +
                       "           (N'Макогон Роман Павлович',          9850)," +
                       "           (N'Кульчицький Андрій Сергійович',   11380)," +
                       "           (N'Андрищак Олександр Орландович',   10800)," +
                       "           (N'Мельничук Анна Олександрівна',    8490)," +
                       "           (N'Сиримак Марина Віталіївна',       8940)," +
                       "           (N'Арнаутов Кирил Георгійович',      7910)," +
                       "           (N'Ткач Євгеній Анатолійович',       9740)," +
                       "           (N'Задорожна Марія Олександрівна',   7640)," +
                       "           (N'Сташко Іванна Іванівна',          8130)," +
                       "           (N'Іванов Влад Володимирович',       9980);";
                    /*
                    command.CommandText =
                        "INSERT INTO [Менеджери] VALUES (N'',0.0)";
                     */
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Таблицю [Менеджери] заповненo успішно");


                    command.CommandText = // заповнення таблиці [Канцтовари]
                       "INSERT INTO [Менеджери] " +
                       "   VALUES  (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0)," +
                       "           (N'',N'',0,1000,0.0);";
                    /*
                    command.CommandText =
                        "INSERT INTO [Канцтовари] VALUES (N'',N'',0,1000,0.0)";
                     */
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Таблицю [Канцтовари] заповненo успішно");


                    command.CommandText = // заповнення таблиці [Замовлення]
                       "INSERT INTO [Замовлення] " +
                       "   VALUES  (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'',N'',N'',0,1000,0.0,2022/05/18);";
                    /*
                    command.CommandText =
                        "INSERT INTO [Замовлення] VALUES (N'',N'',N'',0,1000,0.0,2022/05/18)";
                     */
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Таблицю [Замовлення] заповненo успішно");


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