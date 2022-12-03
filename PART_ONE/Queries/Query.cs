using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

internal class Query
{
    public static async Task ResultQueryAsync(IQuery query)
    {
        await query.Query();
    }
}