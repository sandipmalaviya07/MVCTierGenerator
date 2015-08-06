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
    /// Base Interpreter for method call expressions
    /// </summary>
    sealed class BaseMethodCallExpressionInterpreter
        :IInterpreter,IInterpreterChaining
    {
        #region Constructor

        /// <summary>
        /// Default constructor, create a chaining
        /// </summary>
        public BaseMethodCallExpressionInterpreter()
        {
            //Create Chaining
            EntityMemberTerminalMethodCallExpressionInterpreter entityMemberTerminalMethodCallInterpreter = new EntityMemberTerminalMethodCallExpressionInterpreter();
            EntityMemberMethodCallExpressionInterpreter entityMemberMethodCallInterpreter = new EntityMemberMethodCallExpressionInterpreter();
            DefaultMethodCallExpressionInterpreter defaultMethodCallInterpreter = new DefaultMethodCallExpressionInterpreter();
            
            //Asign Chaining
            entityMemberTerminalMethodCallInterpreter.NextInterpreter = entityMemberMethodCallInterpreter;
            entityMemberMethodCallInterpreter.NextInterpreter = defaultMethodCallInterpreter;
            this.mNextInterpreter = entityMemberTerminalMethodCallInterpreter;
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
            where T:EntityObject,new()
        {
            if (mNextInterpreter != null)
                return mNextInterpreter.InterpreteExpression<T>(expression);
            else
                throw new InvalidOperationException("Invalid Interpreter");
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
    }
}
