using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Objects.DataClasses;

namespace Utility.Extension.Parser.Interpreter
{
    /// <summary>
    /// Define a contract to all interpreters
    /// </summary>
    internal interface IInterpreter
    {
        /// <summary>
        /// Interprete a defined expression
        /// </summary>
        /// <param name="expression">Expression to interprete</param>
        /// <returns>Interpreted expression</returns>
        string InterpreteExpression<T>(Expression expression) where T : EntityObject, new();

        /// <summary>
        /// Verify if interpreter  is valid for a specific expression
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns>true if interpreter is valid</returns>
        bool IsValidInterpreter(Expression expression);
    }
}
