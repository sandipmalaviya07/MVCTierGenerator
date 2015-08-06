using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Extension.Parser.Interpreter.MemberInterpreters;
using System.Linq.Expressions;
using System.Data.Objects.DataClasses;

namespace Utility.Extension.Parser.Interpreter.MemberExpressionInterpreters
{
    /// <summary>
    /// Base interpreter for expression members
    /// </summary>
    sealed class BaseMemberExpressionInterpreter
        :IInterpreter,IInterpreterChaining
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseMemberExpressionInterpreter()
        {
            //Create chaining
            

            //Assign Chaining
           

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
            System.Linq.Expressions.MemberExpression memberExpression = expression as System.Linq.Expressions.MemberExpression;

            if (memberExpression.Type.IsAssignableFrom(typeof(DateTime)))
            {
                DateTimeMemberInterpreter dateTimeMemberIntercepter = new DateTimeMemberInterpreter();
                FieldInfoMemberInterpreter valueTypeMemberIntercepter = new FieldInfoMemberInterpreter();
                dateTimeMemberIntercepter.NextInterpreter = valueTypeMemberIntercepter;
                mNextInterpreter = dateTimeMemberIntercepter;
            }
            else if (memberExpression.Expression != null && memberExpression.Expression.Type.IsAssignableFrom(typeof(T)))
                mNextInterpreter = new ColumnNameMemberInterpreter();
            else
                mNextInterpreter = new FieldInfoMemberInterpreter();

            return NextInterpreter.InterpreteExpression<T>(expression);
        }
        /// <summary>
        /// <see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/>
        /// </summary>
        /// <param name="expression"><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></param>
        /// <returns><see cref="Utility.Extension.Parser.Interpreter.IInterpreter"/></returns>
        public bool IsValidInterpreter(System.Linq.Expressions.Expression expression)
        {
            return expression is MemberExpression;
        }
        #endregion

        #region IInterpreterChainging Members

        private IInterpreter mNextInterpreter = null;
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
