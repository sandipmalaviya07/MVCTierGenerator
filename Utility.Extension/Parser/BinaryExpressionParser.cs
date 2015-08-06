using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Utility.Extension.Metadata;
using System.Data.Objects.DataClasses;
using System.Data;
using Utility.Extension.Parser.Interpreter;
using Utility.Extension.Parser.Interpreter.Tokens;

namespace Utility.Extension.Parser
{

    /// <summary>
    /// Parser of Binary expressions
    /// </summary>
    /// <typeparam name="T">type of entity</typeparam>
    internal sealed class BinaryExpressionParser<T> :IExpressionParser
        where T:EntityObject,new()
    {

        #region Members

        string principalCondition = default(String);
        string principalRightCondition = default(string);
        string principalLeftCondition = default(string);

        #endregion

        #region Private Methods

        /// <summary>
        /// Parse operator name to SQL syntax
        /// </summary>
        /// <param name="nodeTypeName">Name of Node Type</param>
        /// <returns>Sql syntax operator</returns>
        private string GetOperatorString(ExpressionType nodeTypeName)
        {
            switch (nodeTypeName)
            {
                case ExpressionType.OrElse:
                    {
                        return InterpreterFactory.GetTokenFactory().OrElseOperator;
                    }
                case ExpressionType.AndAlso:
                    {
                        return InterpreterFactory.GetTokenFactory().AndAlsoOperator;
                    }
                case ExpressionType.Equal:
                    {
                        return InterpreterFactory.GetTokenFactory().EqualOperator;
                    }
                case ExpressionType.GreaterThan:
                    {
                        return InterpreterFactory.GetTokenFactory().GreaterThanOperator;
                    }
                case ExpressionType.GreaterThanOrEqual:
                    {
                        return InterpreterFactory.GetTokenFactory().GreaterThanOrEqualOperator;
                    }
                case ExpressionType.LessThan:
                    {
                        return InterpreterFactory.GetTokenFactory().LessThanOperator;
                    }
                case ExpressionType.LessThanOrEqual:
                    {
                        return InterpreterFactory.GetTokenFactory().LessThanOrEqualOperator;
                    }
                case ExpressionType.NotEqual:
                    {
                        return InterpreterFactory.GetTokenFactory().NotEqualOperator;
                    }

                default: throw new NotImplementedException("BinaryExpressionType not recognized");
            }
        }
        /// <summary>
        /// Validate if expression is simple comparer expression or not
        /// </summary>
        /// <param name="nodeType">Name of Node Type</param>
        /// <returns>Tru if is simple comparer expression ( =  ...)</returns>
        private bool IsSimpleExpression(ExpressionType nodeType)
        {
            return (nodeType == ExpressionType.AndAlso || nodeType == ExpressionType.OrElse)
                ? false
                : true;
        }
        /// <summary>
        /// Obtain string expression 
        /// </summary>
        /// <param name="expression">BinaryExpression to extract string sql syntax expression</param>
        /// <returns>Sql Syntax expression</returns>
        private string GetStringExpression(Expression expression)
        {
            if (expression != null)
            {
                if (expression is BinaryExpression)
                {
                    BinaryExpression binaryExpression = expression as BinaryExpression;

                    if (IsSimpleExpression(expression.NodeType))
                    {
                        string leftEquation = string.Empty;
                        string rightEquation = string.Empty;

                        if (binaryExpression.Left is ConstantExpression)
                        {
                            leftEquation = InterpreterFactory.GetInterpreter(typeof(ConstantExpression)).InterpreteExpression<T>(binaryExpression.Left);
                        }
                        else if (binaryExpression.Left is MethodCallExpression)
                        {
                            leftEquation  = InterpreterFactory.GetInterpreter(typeof(MethodCallExpression)).InterpreteExpression<T>(binaryExpression.Left);
                        }
                        else if (binaryExpression.Left is MemberExpression)
                        {
                            if (binaryExpression.Left.NodeType == ExpressionType.MemberAccess)
                                leftEquation = MetadataAccessor.GetColumnNameByEdmProperty<T>(binaryExpression.Left.ToString().Split('.')[1]);
                            else
                                throw new InvalidOperationException("Expression Tree is not supported");
                        }
                        else
                            throw new InvalidOperationException("Expression Tree is not supported");
                            

                        

                        if (binaryExpression.Right is ConstantExpression)
                            rightEquation = InterpreterFactory.GetInterpreter(typeof(ConstantExpression)).InterpreteExpression<T>(binaryExpression.Right);
                        else if (binaryExpression.Right is UnaryExpression)
                        {
                            UnaryExpression unaryExpression = binaryExpression.Right as UnaryExpression;
                            rightEquation =  InterpreterFactory.GetInterpreter(unaryExpression.Operand.GetType()).InterpreteExpression<T>(unaryExpression.Operand);
                        }
                        else if (binaryExpression.Right is MemberExpression)
                        {
                            MemberExpression memberExpression = binaryExpression.Right as MemberExpression;
                            rightEquation = InterpreterFactory.GetInterpreter(typeof(MemberExpression)).InterpreteExpression<T>(memberExpression);
                        }
                        else if (binaryExpression.Right is MethodCallExpression)
                        {
                            MethodCallExpression methodCallExpression = binaryExpression.Right as MethodCallExpression;
                            rightEquation = InterpreterFactory.GetInterpreter(typeof(MethodCallExpression)).InterpreteExpression<T>(methodCallExpression);
                        }
                        else
                            rightEquation = MetadataAccessor.GetColumnNameByEdmProperty<T>(binaryExpression.Right.ToString().Split('.')[1]);


                        return string.Format("({0}){1}({2})", leftEquation, GetOperatorString(binaryExpression.NodeType), rightEquation);
                    }
                    else
                    {
                        return string.Format("({0}{1}{2})", GetStringExpression(binaryExpression.Left), GetOperatorString(binaryExpression.NodeType), GetStringExpression(binaryExpression.Right));
                    }
                }
                else if (expression is UnaryExpression)
                {
                    UnaryExpression unaryExpression = expression as UnaryExpression;
                    return InterpreterFactory.GetInterpreter(unaryExpression.Operand.GetType()).InterpreteExpression<T>(unaryExpression.Operand);
                }
                else if (expression is MemberExpression)
                {
                    if (expression.NodeType == ExpressionType.MemberAccess)
                    {
                        if ((expression as MemberExpression).Expression.Type == typeof(T))
                            return MetadataAccessor.GetColumnNameByEdmProperty<T>(expression.ToString().Split('.')[1]);
                        else
                        {
                            return InterpreterFactory.GetInterpreter(typeof(MemberExpression)).InterpreteExpression<T>(expression);
                        }
                    }
                    else
                        return MetadataAccessor.GetColumnNameByEdmProperty<T>(expression.ToString());
                }
                else if (expression is ConstantExpression)
                {
                    return InterpreterFactory.GetInterpreter(typeof(ConstantExpression)).InterpreteExpression<T>(expression);
                }
                else if (expression is MethodCallExpression)
                {
                    return InterpreterFactory.GetInterpreter(typeof(MethodCallExpression)).InterpreteExpression<T>(expression);
                }
                else
                    throw new InvalidExpressionException("Invalid Expression Three");
            }
            else
                throw new NullReferenceException("NULL Expression");
        }
        #endregion

        #region IExpressionParser Members

        Expression mExpression;
        /// <summary>
        /// <see cref="Utility.Extension.Parser.IExpressionParser"/>
        /// </summary>
        public System.Linq.Expressions.Expression Expression
        {
            get
            {
                return mExpression;
            }
            set
            {
                mExpression = value;
            }
        }
        /// <summary>
        /// <see cref="Utility.Extension.Parser.IExpressionParser"/>
        /// </summary>
        /// <returns> <see cref="Utility.Extension.Parser.IExpressionParser"/></returns>
        public string ParseExpression()
        {
            if (Expression != null)
            {
                LambdaExpression expression = Expression as LambdaExpression;
                if (expression != null)
                {
                    Expression<Func<T, bool>> inner = expression as Expression<Func<T, bool>>;
                    if (inner != null)
                    {
                        string bodyTypeName = inner.Body.GetType().Name;
                        switch (bodyTypeName)
                        {
                            case "BinaryExpression":
                                {
                                    BinaryExpression binaryExpression = (BinaryExpression)inner.Body;

                                    this.principalCondition = GetOperatorString(binaryExpression.NodeType);

                                    this.principalLeftCondition = GetStringExpression(binaryExpression.Left as Expression);
                                    this.principalRightCondition = GetStringExpression(binaryExpression.Right as Expression);

                                    return string.Format(" WHERE ({0}{1}{2})", principalLeftCondition, principalCondition, principalRightCondition);
                                }
                            case "LogicalBinaryExpression":
                                {                                    
                                    BinaryExpression binaryExpression = (BinaryExpression)inner.Body;

                                    this.principalCondition = GetOperatorString(binaryExpression.NodeType);

                                    this.principalLeftCondition = GetStringExpression(binaryExpression.Left as Expression);
                                    this.principalRightCondition = GetStringExpression(binaryExpression.Right as Expression);

                                    return string.Format(" WHERE ({0}{1}{2})", principalLeftCondition, principalCondition, principalRightCondition);
                                }
                            default: throw new NotImplementedException("Expression not valid to parse");
                        }

                    }
                    else
                        throw new NotImplementedException("WhereExpression not is valid Expression");
                }
                else
                    throw new InvalidOperationException("WhereExpression not is LambdaExpression");
            }
            else
                throw new InvalidOperationException("WhereExpression is empty");
        }

        #endregion
    }
}
