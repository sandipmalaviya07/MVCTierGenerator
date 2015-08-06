using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Utility.Extension.Parser.Interpreter.Tokens;
using Utility.Extension.Metadata;
using System.Data.Objects.DataClasses;

namespace Utility.Extension.Parser.Interpreter.MethodCallInterpreters
{
    /// <summary>
    /// Interpreter of entity members call methods
    /// </summary>
    sealed class EntityMemberMethodCallExpressionInterpreter
        : IInterpreter, IInterpreterChaining
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
                string columnName = string.Empty;
                string formated = string.Empty;

                MethodCallExpression methodCallExpression = expression as MethodCallExpression;
                columnName = methodCallExpression.ToString().Split('.')[1];


                switch (methodCallExpression.Method.Name)
                {
                    case "Substring":
                        {
                            formated = InterpreterFactory.GetTokenFactory().GetSubstringFunction(
                                                    MetadataAccessor.GetColumnNameByEdmProperty<T>(columnName),
                                                    methodCallExpression.Arguments[0].ToString(),
                                                    methodCallExpression.Arguments[1].ToString());
                                                        
                        } break;
                    case "ToUpper":
                        {
                            formated = InterpreterFactory.GetTokenFactory().GetUpperFuction(
                                                    MetadataAccessor.GetColumnNameByEdmProperty<T>(columnName));
                        } break;
                    case "ToLower":
                        {
                            formated = InterpreterFactory.GetTokenFactory().GetLowerFunction(
                                                    MetadataAccessor.GetColumnNameByEdmProperty<T>(columnName));
                        } break;
                    default: break;
                }

                return string.Format("{0}",formated);

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
            //If is terminal expression over a entity object member
            if ((expression is MethodCallExpression))
            {
                MethodCallExpression methodCallExpression = expression as MethodCallExpression;
                if (methodCallExpression.Method.ReturnType != typeof(Boolean) && methodCallExpression.Object != null )
                {
                    if (methodCallExpression.Object is MemberExpression)
                    {
                        MemberExpression memberExpression = methodCallExpression.Object as MemberExpression;
                        if (typeof(EntityObject).IsAssignableFrom(memberExpression.Expression.Type))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        #endregion
    }
}
