using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;
using Utility.Extension.Metadata;

namespace Utility.Extension.Parser
{
    internal sealed class UpdateExpressionParser<T> : DmlExpressionParser<T>
        where T: EntityObject,new()
    {
        #region Members

        private IExpressionParser setParser;
        private IExpressionParser whereParser;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of UpdateXpressionParser
        /// <see cref="Utility.Extension.Parser.DmlExpressionParser{T}"/>
        /// </summary>
        /// <param name="setExpression">Expression to construct SET part</param>
        /// <param name="whereExpression">Expression to construct WHERE part</param>
        public UpdateExpressionParser(Expression<Func<T, T>> setExpression, Expression<Func<T, bool>> whereExpression)
        {
            //Set Where Expression
            whereParser = new BinaryExpressionParser<T>();
            whereParser.Expression = whereExpression;

            //Set Set Expression
            setParser = new MemberInitExpressionParser<T>();
            setParser.Expression = setExpression;
        }

        #endregion

        #region Override Methods

        public override string GetDmlCommand()
        {
            //Recover Table Name

            StringBuilder updateCommand = new StringBuilder();
            updateCommand.Append("UPDATE ");
            updateCommand.Append(MetadataAccessor.GetTableNameByEdmType(typeof(T).Name));
            updateCommand.Append(" ");
            updateCommand.Append(setParser.ParseExpression());
            updateCommand.Append(whereParser.ParseExpression());

            return updateCommand.ToString();
        }

        #endregion
    }
}
