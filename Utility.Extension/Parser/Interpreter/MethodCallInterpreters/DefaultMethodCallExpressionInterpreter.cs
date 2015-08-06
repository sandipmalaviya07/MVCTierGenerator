using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Utility.Extension.Parser.Interpreter.Tokens;
using System.Data.Objects.DataClasses;

namespace Utility.Extension.Parser.Interpreter.MethodCallInterpreters
{
    /// <summary>
    /// Default Interpreter for method call expressions
    /// </summary>
    sealed class DefaultMethodCallExpressionInterpreter
        :IInterpreter,IInterpreterChaining
    {
        #region IInterpreter Members

        /// <summary>
        /// <see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/>
        /// </summary>
        /// <typeparam name="T"><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></typeparam>
        /// <param name="expression"><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></param>
        /// <returns><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></returns>
        public string InterpreteExpression<T>(System.Linq.Expressions.Expression expression)
            where T : EntityObject, new()
        {
            if (IsValidInterpreter(expression))
            {
                MethodCallExpression methodCallExpression = expression as MethodCallExpression;
                object result = Expression.Lambda(methodCallExpression).Compile().DynamicInvoke();

                return InterpreterFactory.GetTokenFactory().PrepareElement(result, methodCallExpression.Type);
            }
            else
            {
                if (NextInterpreter != null)
                    return NextInterpreter.InterpreteExpression<T>(expression);
                else
                    throw new InvalidOperationException("Invalid Interpreter");
            }
        }
        /// <summary>
        /// <see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/>
        /// </summary>
        /// <param name="expression"><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></param>
        /// <returns><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></returns>
        public bool IsValidInterpreter(System.Linq.Expressions.Expression expression)
        {
            return expression is MethodCallExpression;
        }

        #endregion

        #region IInterpreterChainging Members

        private IInterpreter mNextInterpreter = null;
        /// <summary>
        /// <see cref="Utility.Extension.Parser.Interpreter.IInterpreterChaining"/>
        /// </summary>
        public IInterpreter NextInterpreter
        {
            get
            {
                return mNextInterpreter;
            }
            set
            {
                mNextInterpreter = value;
            }
        }

        #endregion
    }
}
