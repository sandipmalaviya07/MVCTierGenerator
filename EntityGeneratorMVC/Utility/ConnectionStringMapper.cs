using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGeneratorMVC.Utility
{
    internal static class ConnectionStringMapper
    {
        public static string ConnectionString(string Type,string ServerName,string CatalogName,string UserName,string PassWord)
        {
            if (Type == "1")
                return @"Data Source=" + ServerName + ";Initial Catalog=" + CatalogName + ";Integrated Security=True";
            else
                return @"Data Source=" + ServerName + ";Initial Catalog=" + CatalogName + ";Integrated Security=False;User ID=" + UserName + ";Password=" + PassWord + "";
        }
    }
}
