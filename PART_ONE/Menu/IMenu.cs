using Microsoft.Data.SqlClient;
using Microsoft.Data;
using System;
using System.Threading.Tasks;

internal interface IMenu
{
    SqlConnection connection { get; }

    Task OutputMenu();
}