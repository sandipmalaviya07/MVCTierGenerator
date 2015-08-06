using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGeneratorMVC.Utility
{
    public class AutheticationMapper
    {
        public AutheticationMapper(string AuthType, int id)
        {
            this.AuthType = AuthType; this.ID = id;
        }
        public string AuthType { get; set; }
        public int ID { get; set; }
    }
}
