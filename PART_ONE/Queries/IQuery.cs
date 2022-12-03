using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;


internal interface IQuery
{
    SqlConnection connection { get; } // підключення
    string table { get; } // назва таблиці для якої буде використовуватись запит

    Task Query();
}