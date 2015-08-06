using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityGeneratorMVC.Generator;
using System.Data;
namespace EntityGeneratorMVC.DataBaseCommand
{
   public static class DataBaseOperation
    {
        public static DataTable GetDataBaseTable()
        {
            return SqlQuery.GetDataTable("select Name as 'Table Name' from sys.tables where type='U'");
        }
        public static DataTable GetDataTableSchema(string TableName)
        {
            return SqlQuery.GetDataTable(@"SELECT  COLUMN_NAME,IS_NULLABLE,DATA_TYPE , (SELECT top 1 (select top 1 CONSTRAINT_TYPE from INFORMATION_SCHEMA.TABLE_CONSTRAINTS tab 
where tab.CONSTRAINT_NAME = col.CONSTRAINT_NAME) as CONSTRIANT_TYPE from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col where 
col.COLUMN_NAME = schem.COLUMN_NAME and col.TABLE_NAME = '"+TableName+@"' ) as CONSTRIANT_TYPE,
(select top 1 object_name(referenced_object_id) as Foreignkey_TableName from sys.foreign_keys keys inner join  INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE  col
on keys.name = col.CONSTRAINT_NAME where col.COLUMN_NAME = schem.COLUMN_NAME and col.TABLE_NAME = '" + TableName + @"') as Foreignkey_TableName
 FROM INFORMATION_SCHEMA.COLUMNS as schem WHERE TABLE_NAME = '" + TableName + @"' order by ORDINAL_POSITION");
        }
    }
}
