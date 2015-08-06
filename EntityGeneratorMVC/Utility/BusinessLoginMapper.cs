using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGeneratorMVC.Utility
{
    internal class BusinessLoginMapper
    {
        
      
        public static string InsertMethod(string ObjectName)
        {
            StringBuilder sbBusinessLogic = new StringBuilder();
            sbBusinessLogic.Append("\t\tpublic void Insert(" + ObjectName + " model)\r\n");
            sbBusinessLogic.Append("\t\t{\r\n");
            sbBusinessLogic.Append("\t\t\tDbContext." + ObjectName + ".Add(model);\r\n");
            sbBusinessLogic.Append("\t\t\tDbContext.SaveChanges();\r\n");
            sbBusinessLogic.Append("\t\t}\r\n");
            return sbBusinessLogic.ToString();
        }
        public static string UpdateMethod(string ObjectName)
        {
            StringBuilder sbBusinessLogic = new StringBuilder();
            sbBusinessLogic.Append("\t\tpublic void Update(" + ObjectName + " model)\r\n");
            sbBusinessLogic.Append("\t\t{\r\n");
            sbBusinessLogic.Append("\t\t\tDbContext." + ObjectName + ".Attach(model);\r\n");
            sbBusinessLogic.Append("\t\t\tDbContext.Entry(model).State = EntityState.Modified;\r\n");
            sbBusinessLogic.Append("\t\t\tDbContext.SaveChanges();\r\n");
            sbBusinessLogic.Append("\t\t}\r\n");
            return sbBusinessLogic.ToString();
        }
        public static string GetAllMethod(string ObjectName)
        {
            StringBuilder sbBusinessLogic = new StringBuilder();
            sbBusinessLogic.Append("\t\tpublic List<" + ObjectName + "> GetAll()\r\n");
            sbBusinessLogic.Append("\t\t{\r\n");
            sbBusinessLogic.Append("\t\t\t List<" + ObjectName + "> listObject = DbContext." + ObjectName + ".ToList();\r\n");
            sbBusinessLogic.Append("\t\t\t return listObject;\r\n");
            sbBusinessLogic.Append("\t\t}\r\n");
            return sbBusinessLogic.ToString();
        }
        public static string GetByPrimaryKey(string ObjectName,string PrimaryKey,string PrimaryKeyType)
        {
            StringBuilder sbBusinessLogic = new StringBuilder();
            sbBusinessLogic.Append("\t\tpublic " + ObjectName + " GetByPrimaryKey(" + PrimaryKeyType + " " + PrimaryKey + ")\r\n");
            sbBusinessLogic.Append("\t\t{\r\n");
            sbBusinessLogic.Append("\t\t\t " + ObjectName + " singleObject = DbContext." + ObjectName + ".FirstOrDefault(f=>f." + PrimaryKey + "==" + PrimaryKey + ");\r\n");
            sbBusinessLogic.Append("\t\t\t return singleObject;\r\n");
            sbBusinessLogic.Append("\t\t}\r\n");
            return sbBusinessLogic.ToString();
        }

        public static string GetPagination(string ObjectName, string PrimaryKey)
        {
            StringBuilder sbBusinessLogic = new StringBuilder();
            sbBusinessLogic.Append("\t\tpublic PagerList<" + ObjectName + "> GetPagination(int pageNumber = 1,int pageRows = 10,string orderBy = \"" + PrimaryKey + " descending\")\r\n");
            sbBusinessLogic.Append("\t\t{\r\n");
            sbBusinessLogic.Append("\t\t\t PagerList<" + ObjectName + "> paginationObject = DbContext." + ObjectName + ".ToPagerListOrderBy(pageNumber,pageRows,orderBy);\r\n");
            sbBusinessLogic.Append("\t\t\t return paginationObject;\r\n");
            sbBusinessLogic.Append("\t\t}\r\n");
            return sbBusinessLogic.ToString();
        }
    }
}
