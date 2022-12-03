﻿using Microsoft.Data.SqlClient;
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
                        "   [Назва канцтовара] NVARCHAR(100) NOT NULL, " +
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
                        "   [Назва фірми покупця] NVARCHAR(100) NOT NULL, " +
                        "   [Назва канцтовара] NVARCHAR(100) NOT NULL, " +
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
                       "INSERT INTO [Канцтовари] " +
                       "   VALUES  (N'Фломастери OL-917-6к Фломіки 6цв',                                        N'Фломастери',                  240,    1015,   13.0)," +
                       "           (N'Фломастери OL-916-6 Фломиіи 6цв',                                         N'Фломастери',                  381,    1019,   14.0)," +
                       "           (N'Фломастери Ol-917-6T Turbo 6цв',                                          N'Фломастери',                  298,    1008,   14.0)," +
                       "           (N'Блокнот на пружині зверху SHOTLANDKA, А6, 48 арк., клітинка, синій',      N'Блокноти',                    146,    1009,   16.99)," +
                       "           (N'Зошит А5 32 аркушів клітинка Interdruk HS Kraft 93800',                   N'Блокноти',                    198,    1006,   34.99)," +
                       "           (N'Ручка Паркер Sonnet 17 Matte Black Lacquer позол. кул. 84 832',           N'Ручка',                       141,    1020,   6499.0)," +
                       "           (N'Лінер 0.4мм GRAPH PEPS MP.74911 MAPED чорний',                            N'Ручка',                       673,    1017,   54.99)," +
                       "           (N'Ручка кулькова Buromax 8117 синій 01',                                    N'Ручка',                       591,    1010,   4.99)," +
                       "           (N'ГУМКА ВМ.1111 для стирання',                                              N'Гумки',                       763,    1016,   8.99)," +
                       "           (N'ГУМКА ВМ.1112 для стирання',                                              N'Гумки',                       703,    1003,   9.99)," +
                       "           (N'Точилка ТВАРИНКИ ZB.5594 з контейнером, 2 отв., асорті для олівців',      N'Точилки',                     358,    1009,   29.99)," +
                       "           (N'Точилка з контейнером Buromax Alfa 2 отвори асорті BM.4778 для олівців',  N'Точилки',                     298,    1007,   29.99)," +
                       "           (N'Кошик для сміття пластиковий чорний 82061 Арніка',                        N'Кошики для паперу',           193,    1006,   84.99)," +
                       "           (N'Кошик для сміття металевий сітка 3126 ZiBi синій',                        N'Кошики для паперу',           120,    1007,   399.90)," +
                       "           (N'Швидкозшивач картонний Аспект білий',                                     N'Офісна паперова продукція',   569,    1018,   5.99)," +
                       "           (N'Обкладинка картонна Справа Аспект',                                       N'Офісна паперова продукція',   791,    1012,   4.99)," +
                       "           (N'Конверт DL СКЛ білий 2052,2040',                                          N'Офісна паперова продукція',   673,    1004,   1.99)," +
                       "           (N'СКОБИ 24/6 BM. 4402 нікель',                                              N'Канцелярське приладдя',       1207,   1013,   13.99)," +
                       "           (N'Ножиці 16см чорні BM.4507-01 Buromax',                                    N'Канцелярське приладдя',       273,    1001,   29.99);";
                    /* 
                    command.CommandText =
                        "INSERT INTO [Канцтовари] VALUES (N'',N'',0,1000,0.0)";
                     */
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Таблицю [Канцтовари] заповненo успішно");


                    command.CommandText = // заповнення таблиці [Замовлення]
                       "INSERT INTO [Замовлення] " +
                       "   VALUES  (N'ДоброБуд',        N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'ВашБудинок',      N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Комфорт Таун',    N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Аудит Прем'єр',   N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Предикат',        N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'БухБюро',         N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Аудит 911',       N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Фенікс Аккаунт',  N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Бест Клінік',     N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Парацельс',       N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Пан Диван',       N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Есентуки',        N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Факстрот',        N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Альянс Холдинг',  N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Ерідон',          N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Фармак',          N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Росава',          N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Оболонь',         N'',N'',0,1000,0.0,2022/05/18)," +
                       "           (N'Рітейл Груп',     N'',N'',0,1000,0.0,2022/05/18);";
                    /*і
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