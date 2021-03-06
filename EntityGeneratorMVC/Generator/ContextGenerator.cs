﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGeneratorMVC.Generator
{
    internal static class ContextGenerator
    {
        public static string EntityContextGenerator<T>(this IList<T> EntityMapper, string NameSpace = "Sandip.Malaviya")
        {
            StringBuilder ContextString = new StringBuilder();
            var list = EntityMapper as IList<string>;
            ContextString.Append("// <auto-generated>");
            ContextString.Append("// Copy Right Reserved.");
            ContextString.Append("// </auto-generated>");
            ContextString.Append("using System;" + "\r\n");
            ContextString.Append("using System.Collections.Generic;" + "\r\n");
            ContextString.Append("using System.Data.Common;" + "\r\n");
            ContextString.Append("using System.Data.Entity;" + "\r\n");
            ContextString.Append("using System.Data.Entity.Infrastructure;" + "\r\n");
            ContextString.Append("namespace " + NameSpace + "" + "\r\n");
            ContextString.Append("{\r\n");
            ContextString.Append("\tpublic class ContextClass : DbContext" + "\r\n");
            ContextString.Append("\t{\r\n");
            foreach (var item in list)
            {
                ContextString.Append("\t\tpublic DbSet<"+item+"> "+item+" { get; set; }\r\n");
            }
            ContextString.Append("\t}\r\n");
            ContextString.Append("}\r\n");
            return ContextString.ToString();  
        }
    }
}
