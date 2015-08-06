using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;
using Utility.Extension.Metadata;

namespace Utility.Extension.Parser
{
    /// <summary>
    /// Parser of DELETE command
    /// </summary>
    /// <typeparam name="T">type of entity</typeparam>
    internal sealed class DeleteExpressionParser<T>
        :DmlExpressionParser<T>
    where T:EntityObject,new()
    {

        #region Members

        IExpressionParser whereExpressionParser;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="whereExpression">Binary Expression to create WHERE expression in DELETE command </param>
        public DeleteExpressionParser(Expression<Func<T, bool>> whereExpression)
        {
            whereExpressionParser = new BinaryExpressionParser<T>();
            whereExpressionParser.Expression = whereExpression;
        }

        #endregion

        #region DmlExpressionParser Overrides

        /// <summary>
        /// GET DELETE COMMAND to execute
        /// </summary>
        /// <returns>Delete command</returns>
        public override string GetDmlCommand()
        {
            StringBuilder deleteCommand = new StringBuilder();
            deleteCommand.Append(" DELETE ");
            deleteCommand.Append(" FROM ");
            deleteCommand.Append(MetadataAccessor.GetTableNameByEdmType(typeof(T).Name));
            deleteCommand.Append(whereExpressionParser.ParseExpression());

            return deleteCommand.ToString();
        }

        #endregion
    }
}
