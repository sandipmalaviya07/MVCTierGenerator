using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Utility.Extension.Parser.Interpreter.Tokens;
using System.Data.Objects.DataClasses;

namespace Utility.Extension.Parser.Interpreter.NewInterpreters
{
    /// <summary>
    /// DateTime interpreter
    /// </summary>
    sealed class DateTimeNewInterpreter
        :IInterpreter,IInterpreterChaining
    {
        #region IInterpreterChainging Members

        IInterpreter mNextInterpreter = null;
        /// <summary>
        /// <see cref="Utility.Extension.Parser.Interpreter.IInterpreterChaining"/>
        /// </summary>
        public IInterpreter NextInterpreter
        {
            get { return mNextInterpreter; }
            set { mNextInterpreter = value; }
        }

        #endregion

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
                NewExpression newExpression = expression as NewExpression;
                ConstructorInfo constructorInfo = newExpression.Constructor;

                object[] arguments = (from a in newExpression.Arguments.OfType<ConstantExpression>()
                                      select a.Value).ToArray();

                DateTime dateTime = (DateTime)constructorInfo.Invoke(arguments);
                return string.Format("{0}{1}{0}", InterpreterFactory.GetTokenFactory().QuoteToken, dateTime.ToString());
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
            if (expression is NewExpression)
            {
                NewExpression newExpression = expression as NewExpression;
                if (newExpression.Type != null && newExpression.Type.Name == "DateTime")
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        #endregion
    }
}
