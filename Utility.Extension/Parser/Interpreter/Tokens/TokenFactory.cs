using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Extension.Parser.Interpreter.Tokens
{
    abstract class TokenFactory
    {
        #region Properties

        /// <summary>
        /// Quote token
        /// </summary>
        public abstract string QuoteToken
        {
            get;
        }
        /// <summary>
        /// Now date function in SQL Dialect
        /// </summary>
        public abstract string NowDateFunction
        {
            get;
        }
        /// <summary>
        /// Utc date function in SQL Dialect
        /// </summary>
        public abstract string UtcDateFunction
        {
            get;
        }
        /// <summary>
        /// To Upper function in SQL Dialect
        /// </summary>
        public abstract string ToUpperFunction
        {
            get;
        }
        /// <summary>
        /// ToLower function in SQL Dialect
        /// </summary>
        public abstract string ToLowerFunction
        {
            get;
        }
        /// <summary>
        /// Substring function in SQL Dialect
        /// </summary>
        public abstract string SubstringFunction
        {
            get;
        }
        /// <summary>
        /// Or operator in SQL Dialect
        /// </summary>
        public abstract string OrElseOperator
        {
            get;
        }
        /// <summary>
        /// And operator in SQL Dialect
        /// </summary>
        public abstract string AndAlsoOperator
        {
            get;
        }
        /// <summary>
        /// Equal operator in SQL Dialect
        /// </summary>
        public abstract string EqualOperator
        {
            get;
        }
        /// <summary>
        /// Greather than operator in SQL Dialect
        /// </summary>
        public abstract string GreaterThanOperator
        {
            get;
        }
        /// <summary>
        /// Grether than or equal operator in SQL Dialect
        /// </summary>
        public abstract string GreaterThanOrEqualOperator
        {
            get;
        }
        /// <summary>
        /// Less than operator in SQL Dialect
        /// </summary>
        public abstract string LessThanOperator
        {
            get;
        }
        /// <summary>
        /// Less than or equal operator in SQL Dialect
        /// </summary>
        public abstract string LessThanOrEqualOperator
        {
            get;
        }
        /// <summary>
        /// Not equal operator in SQL Dialect
        /// </summary>
        public abstract string NotEqualOperator
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Replace invalid tokens
        /// </summary>
        /// <param name="element">Element</param>
        /// <returns>Element without invalid tokens</returns>
        public abstract string ReplaceQuoteTokens(string element);

        /// <summary>
        /// Get date function
        /// </summary>
        /// <param name="functionName">name of the function</param>
        /// <returns>SQL Dialect function</returns>
        public abstract string GetDateFunction(string functionName);

        /// <summary>
        /// Prepare element 
        /// </summary>
        /// <param name="element">Element to prepare</param>
        /// <param name="elementType">type of element</param>
        /// <returns>Prepared element</returns>
        public abstract string PrepareElement(object element,Type elementType);

        /// <summary>
        /// Get specific SQL dialect substring function
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="index">index</param>
        /// <param name="length">lengh</param>
        /// <returns>SQL Dialect substring function</returns>
        public abstract string GetSubstringFunction(string element, string index, string length);

        /// <summary>
        /// Get ToUpper function in SQL Dialect
        /// </summary>
        /// <param name="element">element</param>
        /// <returns>ToUpper function in SQL Dialect</returns>
        public abstract string GetUpperFuction(string element);

        /// <summary>
        /// Get ToLower function in SQL Dialect
        /// </summary>
        /// <param name="element">element</param>
        /// <returns>ToLower function in SQL Dialect</returns>
        public abstract string GetLowerFunction(string element);
        #endregion
    }
}
