using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Extension.Parser.Interpreter.Tokens
{
    /// <summary>
    /// <see cref="Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
    /// </summary>
    sealed class SqlClientTokenFactory
        :TokenFactory
    {
        #region Properties

        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string QuoteToken
        {
            get { return "'"; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string NowDateFunction
        {
            get { return "GETDATE()"; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string UtcDateFunction
        {
            get { return "GETUTCDATE()"; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string ToUpperFunction
        {
            get { return "UPPER"; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string ToLowerFunction
        {
            get { return "LOWER"; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string SubstringFunction
        {
            get { return "SUBSTRING"; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string OrElseOperator
        {
            get { return " OR "; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string AndAlsoOperator
        {
            get { return " AND "; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string EqualOperator
        {
            get { return " = "; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string GreaterThanOperator
        {
            get { return " > "; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string GreaterThanOrEqualOperator
        {
            get { return " >= "; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string LessThanOperator
        {
            get { return " < "; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string LessThanOrEqualOperator
        {
            get { return " <= "; }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </summary>
        public override string NotEqualOperator
        {
            get { return " <> "; }
        }
       

        #endregion

        #region Methods
        
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory.GetDateFunction"/>
        /// </summary>
        /// <param name="functionName"> <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/></param>
        /// <returns>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.TokenFactory"/>
        /// </returns>
        public override string GetDateFunction(string functionName)
        {
            switch (functionName)
            {
                case "Now":
                    {
                        return NowDateFunction;
                    }
                case "Today":
                    {
                        return NowDateFunction;
                    }
                case "UtcNow":
                    {
                        return UtcDateFunction;
                    }
                default: throw new ArgumentException("Invalid Token", functionName);
            }
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.ReplaceQuoteTokens"/>
        /// </summary>
        /// <param name="element"><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.ReplaceQuoteTokens"/></param>
        /// <returns><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.ReplaceQuoteTokens"/></returns>
        public override string ReplaceQuoteTokens(string element)
        {
            return element.Replace("\"", QuoteToken);
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.PrepareElement"/>
        /// </summary>
        /// <param name="value">
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.PrepareElement"/>
        /// </param>
        /// <param name="elementType"><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.PrepareElement"/></param>
        /// <returns><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.PrepareElement"/></returns>
        public override string PrepareElement(object value,Type elementType)
        {
            switch(elementType.Name)
            {
                case "String":
                    {
                        return string.Format("'{0}'", value.ToString().Replace("'",""));
                    }
                case "DateTime":
                    {
                        return string.Format("'{0}'", value.ToString());
                    }
                case "Boolean":
                    {
                        return string.Format("'{0}'", value.ToString());
                    }
                default: return value.ToString();
            }            
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetSubstringFunction"/>
        /// </summary>
        /// <param name="element"><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetSubstringFunction"/></param>
        /// <param name="index"><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetSubstringFunction"/></param>
        /// <param name="length"><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetSubstringFunction"/></param>
        /// <returns><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetSubstringFunction"/></returns>
        public override string GetSubstringFunction(string element, string index, string length)
        {
            return string.Format("{0}({1},{2},{3})", SubstringFunction, element, index.ToString(), length.ToString());
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetLowerFunction"/>
        /// </summary>
        /// <param name="element"><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetLowerFunction"/></param>
        /// <returns><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetLowerFunction"/></returns>
        public override string GetLowerFunction(string element)
        {
            return string.Format("{0}({1})", ToLowerFunction, element);
        }
        /// <summary>
        /// <see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetUpperFuction"/>
        /// </summary>
        /// <param name="element"><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetUpperFuction"/></param>
        /// <returns><see cref="M:Utility.Extension.Parser.Interpreter.Tokens.GetUpperFuction"/></returns>
        public override string GetUpperFuction(string element)
        {
            return string.Format("{0}({1})", ToUpperFunction, element);
        }

        #endregion
    }
}
