using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Utility.Extension.Parser
{
    /// <summary>
    /// Interface of all expression parsers
    /// </summary>
    internal interface IExpressionParser
    {
        /// <summary>
        /// Expression to parse
        /// </summary>
        Expression @Expression { get; set; }

        /// <summary>
        /// Convert the expression in a equivalent string
        /// </summary>
        /// <returns></returns>
        string ParseExpression();
    }
}
