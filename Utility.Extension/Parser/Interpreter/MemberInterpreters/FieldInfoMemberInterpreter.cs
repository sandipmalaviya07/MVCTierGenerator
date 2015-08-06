using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Utility.Extension.Parser.Interpreter.Tokens;
using System.Data.Objects.DataClasses;

namespace Utility.Extension.Parser.Interpreter.MemberInterpreters
{
    /// <summary>
    /// FieldInfo interpreter
    /// </summary>
    sealed class FieldInfoMemberInterpreter
        :IInterpreter,IInterpreterChaining
    {
        #region IInterpreterChainging Members

        IInterpreter mNextInterpreter = null;
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
                object value = Expression.Lambda(expression).Compile().DynamicInvoke();
                return InterpreterFactory.GetTokenFactory().PrepareElement(value, expression.Type);
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
            MemberExpression memberExpression = expression as MemberExpression;
            if (memberExpression.Member is FieldInfo || memberExpression.Member is PropertyInfo)
                return true;
            else
                return false;
        }

        #endregion
    }
}
