using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Data.Objects;

namespace Utility.Extension.ObjectQueryExtensions
{
    /// <summary>
    /// SqlServer Execution Context
    /// </summary>
    internal sealed class SqlContext
    {
        #region Members

        string mProviderConnectionString;
        
        #endregion

        public ObjectContext Context
        {
            get;
            private set;
        }

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="providerConnectionString">Entity Data Model ConnectionString</param>
        public SqlContext(ObjectContext context, string providerConnectionString)
        {
            this.Context = context;
            this.mProviderConnectionString = providerConnectionString;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Execute Command to underliying database
        /// </summary>
        /// <param name="command">Command text to execute</param>
        /// <returns>Rows affected</returns>
        public int ExecuteCommand(string command)
        {
            
            using (SqlConnection conn = new SqlConnection(mProviderConnectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = command;

                if ((conn.State & ConnectionState.Open) != ConnectionState.Open)
                    conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public void ExecuteCommandTransaction(IEnumerable<string> commands)
        {
            using (SqlConnection conn = new SqlConnection(mProviderConnectionString))
            //using (DbConnection conn = this.Context.Connection)
            {
                ConnectionState initialState = conn.State;
                DbTransaction transaction = null;

                try
                {                    
                    if ((conn.State & ConnectionState.Open) != ConnectionState.Open)
                        conn.Open();

                    transaction = conn.BeginTransaction();

                    foreach (string command in commands)
                    {
                        DbCommand cmd = conn.CreateCommand();
                        cmd.Transaction = transaction;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = command;
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    if (initialState != ConnectionState.Open)
                        conn.Close(); // only close connection if not initially open
                }           
            }
        }
        
        #endregion
    }
}
