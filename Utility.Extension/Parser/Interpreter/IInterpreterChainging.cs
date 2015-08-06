using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Extension.Parser.Interpreter
{
    /// <summary>
    /// Interpreter chaining interface
    /// </summary>
    interface  IInterpreterChaining
    {
        /// <summary>
        /// Get next interpreter
        /// </summary>
         IInterpreter NextInterpreter { get; set; }
    }
}
