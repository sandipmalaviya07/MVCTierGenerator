using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;
using Utility.Extension.Metadata;
using System.Reflection;
using Utility.Extension.Parser.Interpreter;

namespace Utility.Extension.Parser
{
    /// <summary>
    /// Expression parser 
    /// </summary>
    /// <typeparam name="T">type of entityObject</typeparam>
    internal sealed class MemberInitExpressionParser<T> :IExpressionParser
        where T:EntityObject,new()
    {
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
        ///<see cref="Utility.Extension.Parser.IExpressionParser"/>
        /// </summary>
        /// <returns> <see cref="Utility.Extension.Parser.IExpressionParser"/></returns>
        public string ParseExpression()
        {
            Expression<Func<T, T>> inner = Expression as Expression<Func<T, T>>;
            if (inner != null)
            {
                MemberInitExpression initExpression = inner.Body as MemberInitExpression;
                if (initExpression != null)
                {
                    List<MemberAssignment> memberAssignments = (from m in initExpression.Bindings.OfType<MemberAssignment>() select m).ToList();
                    
                    var result = (from m in initExpression.Bindings.OfType<MemberAssignment>()
                                  select new { PropertyName = m.Member.Name, Value = ParseValueExpression(m.Expression) }).ToList();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SET ");
                    sb.Append(MetadataAccessor.GetColumnNameByEdmProperty<T>(result[0].PropertyName));
                    sb.Append("=");
                    sb.Append(result[0].Value.Replace("\"","'"));

                    for (int i = 1; i < result.Count; i++)
                    {
                        sb.Append(",");
                        sb.Append(MetadataAccessor.GetColumnNameByEdmProperty<T>(result[i].PropertyName));
                        sb.Append("=");
                        sb.Append(result[i].Value.Replace("\"","'"));
                    }
                    return sb.ToString();
                }
                else
                    throw new InvalidOperationException("Expression is not  a valid expresion");
            }
            else
                throw new InvalidOperationException("Expression is not  a valid expresion");
        }

        #endregion

        #region Private Methods

        private string ParseValueExpression(Expression expression)
        {
            if (expression is ConstantExpression)
                return InterpreterFactory.GetInterpreter(typeof(ConstantExpression)).InterpreteExpression<T>(expression);

            else if (expression is UnaryExpression)
            {
                Expression operandExpression = ((UnaryExpression)expression).Operand;
                if (operandExpression is MemberExpression)
                {
                    return InterpreterFactory.GetInterpreter(typeof(MemberExpression)).InterpreteExpression<T>(operandExpression);
                }
                else if (operandExpression is NewExpression)
                {
                    return InterpreterFactory.GetInterpreter(typeof(NewExpression)).InterpreteExpression<T>(operandExpression);
                }
                else if (operandExpression is ConstantExpression)
                {
                    return InterpreterFactory.GetInterpreter(typeof(ConstantExpression)).InterpreteExpression<T>(operandExpression);
                }
                else
                    throw new InvalidOperationException("Invalid Expression in SET Expression");
            }
            else if (expression is MemberExpression)
            {
                return InterpreterFactory.GetInterpreter(typeof(MemberExpression)).InterpreteExpression<T>(expression);               
            }
            else if (expression.GetType().Name == "MethodBinaryExpression")
            {
                string left = string.Empty;
                string right = string.Empty;
                string method = string.Empty;

                BinaryExpression binary = expression as BinaryExpression;
                left = ParseValueExpression(binary.Left);
                right = ParseValueExpression(binary.Right);

                switch (binary.Method.Name)
                {
                    case "Concat":
                        method = " + ";
                        break;
                }

                return string.Format("{0}{1}{2}", left, method, right);                
            }
            else
                throw new InvalidOperationException("Invalid Expression in SET Expression");
        }
        #endregion
    }
}
