using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

internal class Menu
{
    public static async Task ResultQueryAsync(IMenu menu)
    {
        await menu.OutputMenu();
    }
}