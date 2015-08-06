using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGeneratorMVC.Utility
{
    public class EntityMapper
    {
        public string COLUMN_NAME { get; set; }
        public string IS_NULLABLE{get; set;}
        public string DATA_TYPE { get; set; }
        public string CONSTRIANT_TYPE { get; set; }
        public string Foreignkey_TableName { get; set; }
    }
}
