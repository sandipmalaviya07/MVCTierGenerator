using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Utility.Extension.Parser.Interpreter.Tokens;
using System.Data.Objects.DataClasses;

namespace Utility.Extension.Parser.Interpreter.ConstantInterpreters
{
    /// <summary>
    /// <see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/>
    /// </summary>
    sealed class BaseConstantExpressionInterpreter
        :IInterpreter
    {
        #region IInterpreter Members

        /// <summary>
        /// <see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/>
        /// </summary>
        /// <param name="expression"></param>
        /// <returns><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></returns>
        public string InterpreteExpression<T>(System.Linq.Expressions.Expression expression)
            where T : EntityObject, new()
        {
            if (!(expression is ConstantExpression))
                throw new InvalidOperationException("Invalid expression interpreter");

            ConstantExpression constantExpression = expression as ConstantExpression;
            //return InterpreterFactory.GetTokenFactory().ReplaceQuoteTokens(expression.ToString());
            return InterpreterFactory.GetTokenFactory().PrepareElement(constantExpression.Value, constantExpression.Type);
        }
        /// <summary>
        /// <see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/>
        /// </summary>
        /// <param name="expression"><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></param>
        /// <returns></returns>
        public bool IsValidInterpreter(Expression expression)
        {
            return expression is ConstantExpression;
        }
        #endregion   
    }
}
