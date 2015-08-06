using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Objects.DataClasses;

using Utility.Extension.Parser.Interpreter.Tokens;
using Utility.Extension.Parser.Interpreter.MethodCallInterpreters;
using Utility.Extension.Parser.Interpreter.MemberExpressionInterpreters;
using Utility.Extension.Parser.Interpreter.NewInterpreters;
using Utility.Extension.Parser.Interpreter.ConstantInterpreters;

namespace Utility.Extension.Parser.Interpreter
{
    /// <summary>
    /// Interpreter Factory
    /// </summary>
    static class InterpreterFactory
    {
        #region Members 

        static IInterpreter memberInterpreter = new BaseMemberExpressionInterpreter();
        static IInterpreter newInterpreter = new BaseNewExpressionInterpreter();
        static IInterpreter constantInterpreter = new BaseConstantExpressionInterpreter();
        static IInterpreter methodCallInterpreter = new BaseMethodCallExpressionInterpreter();

        static TokenFactory tokenFactory = new SqlClientTokenFactory();

        #endregion

        #region Public Methods

        /// <summary>
        /// Get specific interpreter for expression type
        /// </summary>
        /// <param name="expressionType">Type of expression</param>
        /// <returns>Specific interpreter for expression</returns>
        public static IInterpreter GetInterpreter(Type expressionType)
        {
            switch (expressionType.Name)
            {
                case "MemberExpression":
                    {
                        return memberInterpreter;
                    }
                case "NewExpression":
                    {
                        return newInterpreter;
                    }
                case "ConstantExpression":
                    {
                        return constantInterpreter;
                    }
                case "MethodCallExpression":
                    {
                        return methodCallInterpreter;
                    }
                default: throw new InvalidOperationException("Cannot find Interpreter for expression");

            }
        }
        /// <summary>
        /// Get Token Factory
        /// </summary>
        /// <returns>Token Factory</returns>
        public static TokenFactory GetTokenFactory()
        {
            return tokenFactory;
        }

        #endregion
    }
}
