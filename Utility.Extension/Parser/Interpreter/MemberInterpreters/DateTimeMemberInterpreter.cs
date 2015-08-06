﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Utility.Extension.Parser.Interpreter.Tokens;
using System.Data.Objects.DataClasses;

namespace Utility.Extension.Parser.Interpreter.MemberInterpreters
{
    /// <summary>
    /// DateTime member interpreter
    /// </summary>
    sealed class DateTimeMemberInterpreter
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
            if ( IsValidInterpreter(expression) )
            { 
                MemberExpression memberExpression = expression as MemberExpression;

                if (memberExpression.GetType().Name == "PropertyExpression")
                    return InterpreterFactory.GetTokenFactory().GetDateFunction(memberExpression.Member.Name);
                else
                {
                    object value = Expression.Lambda(expression).Compile().DynamicInvoke();
                    return InterpreterFactory.GetTokenFactory().PrepareElement(value, expression.Type);
                }
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
        public bool IsValidInterpreter(Expression expression)
        {
            MemberExpression memberExpression = expression as MemberExpression;
            if (memberExpression != null)
            {
                if (memberExpression.Type != null && memberExpression.Type.Name == "DateTime")
                    return true;
                else
                    return false;

            }
            else
                return false;
        }

        #endregion

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
    }
}
