using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Utility.Extension.Parser
{
    /// <summary>
    /// Base class for all dml expression parsers
    /// </summary>
    /// <typeparam name="T">Typeof EntityObject to use in parser</typeparam>
    internal abstract class DmlExpressionParser<T>
        where T:EntityObject,new()
    {
        #region Properties

        /// <summary>
        /// Get the TSQL  command
        /// </summary>
        /// <returns>TSQL command</returns>
        public abstract string GetDmlCommand();

        #endregion
    }
}
