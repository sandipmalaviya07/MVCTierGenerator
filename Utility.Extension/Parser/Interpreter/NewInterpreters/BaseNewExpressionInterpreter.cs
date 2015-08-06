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
    /// Base interpreter for new expressions
    /// </summary>
    sealed class BaseNewExpressionInterpreter
        :IInterpreter,IInterpreterChaining
    {
        #region Constructor

        /// <summary>
        /// Default constructor. Create a interpreter chaining
        /// </summary>
        public BaseNewExpressionInterpreter()
        {
            //Create Chaining
            DateTimeNewInterpreter interpreter = new DateTimeNewInterpreter();
            this.mNextInterpreter = interpreter;
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
            return NextInterpreter.InterpreteExpression<T>(expression);

        }
        /// <summary>
        /// <see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/>
        /// </summary>
        /// <param name="expression"><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></param>
        /// <returns><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></returns>
        public bool IsValidInterpreter(System.Linq.Expressions.Expression expression)
        {
            return expression is NewExpression;
        }

        #endregion
    }
}
