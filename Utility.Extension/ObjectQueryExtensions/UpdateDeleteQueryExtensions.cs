using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Linq.Expressions;
using Utility.Extension.Parser;
using System.Data.Objects.DataClasses;
using System.Configuration;
using Utility.Extension.Metadata;
using System.Data.EntityClient;
using System.IO;
using System.Reflection;

namespace Utility.Extension.ObjectQueryExtensions
{
    /// <summary>
    /// Extensions methods in ObjectQuery
    /// </summary>
    public static class UpdateDeleteQueryExtensions
    {
        #region ObjectQuery Extensions Methods

        /// <summary>
        /// Extension method to update multiple entities of type {T}
        /// </summary>
        /// <typeparam name="T">type of entity</typeparam>
        /// <param name="objectQuery">ObjectQuery associated </param>
        /// <param name="setExpression">expression of the resulting objects</param>
        /// <param name="whereExpression">expression of entities to update</param>
        /// <returns></returns>
        public static int Update<T>(this ObjectQuery<T> objectQuery, Expression<Func<T, T>> setExpression, Expression<Func<T, bool>> whereExpression)
            where T:EntityObject,new()
        {
            return Update<T>(objectQuery, new CommandExpression<T>(setExpression, whereExpression));
            //Recover EF ConnectionString
            //string efConnectionString = GetConnectionString(objectQuery.Context.Connection.ConnectionString);

            //Populate MetadataAccessor,
            //MetadataAccessor.PopulateMetadata(efConnectionString, typeof(T).Assembly);

            //Create parser class
            //DmlExpressionParser<T> updateExpressionParser = new UpdateExpressionParser<T>(setExpression, whereExpression);
            //string updateCommand = updateExpressionParser.GetDmlCommand();

            //Execute command 

                //Get providerconnectionstring
            //EntityConnectionStringBuilder connectionBuilder = new EntityConnectionStringBuilder(efConnectionString);

            //SqlContext context = new SqlContext(connectionBuilder.ProviderConnectionString);
                //Log Command
            //if (logger != null) logger.WriteLine(updateCommand);

            //return context.ExecuteCommand(updateCommand);
        }
        
        public static int Update<T>(this ObjectQuery<T> objectQuery, params CommandExpression<T>[] expressionCommands)
            where T : EntityObject, new()
        {            
            string efConnectionString = GetConnectionString(objectQuery.Context.Connection.ConnectionString);
            MetadataAccessor.PopulateMetadata(efConnectionString, typeof(T).Assembly);

            List<string> commands = new List<string>();

            foreach (CommandExpression<T> expressionCommand in expressionCommands)
            {
                DmlExpressionParser<T> updateExpressionParser = new UpdateExpressionParser<T>(expressionCommand.SetExpression, expressionCommand.WhereExpression);
                string updateCommand = updateExpressionParser.GetDmlCommand();
                commands.Add(updateCommand);
                //if (logger != null) logger.WriteLine(updateCommand);
            }
        
            EntityConnectionStringBuilder connectionBuilder = new EntityConnectionStringBuilder(efConnectionString);
            SqlContext context = new SqlContext(objectQuery.Context, connectionBuilder.ProviderConnectionString);
            
            context.ExecuteCommandTransaction(commands);
            return 0;
        }
        
        /*public static int Delete<T>(this ObjectQuery<T> objectQuery,Expression<Func<T,bool>> whereExpression,TextWriter logger = null)
            where T:EntityObject,new()
        {
            //Recover EF ConnectionString
            string efConnectionString = GetConnectionString(objectQuery.Context.Connection.ConnectionString);

            //Populate MetadataAccessor,
            MetadataAccessor.PopulateMetadata(efConnectionString, typeof(T).Assembly);

            //Create parser class
            DmlExpressionParser<T> deleteExpressionParser = new DeleteExpressionParser<T>(whereExpression);
            string deleteCommand = deleteExpressionParser.GetDmlCommand();

            //Execute command 

            //Get providerconnectionstring
            EntityConnectionStringBuilder connectionBuilder = new EntityConnectionStringBuilder(efConnectionString);

            SqlContext context = new SqlContext(connectionBuilder.ProviderConnectionString);
                //Log Command
            if (logger != null) logger.WriteLine(deleteCommand);

            return context.ExecuteCommand(deleteCommand);
        }*/
        
        /// <summary>
        /// Extension method to delete multiple entities of type {T} 
        /// </summary>
        /// <typeparam name="T">type of entity</typeparam>
        /// <param name="objectQuery">ObjectQuery associated</param>
        /// <param name="whereExpressions">collection filter of objects to delete</param>
        /// <returns></returns>
        public static int Delete<T>(this ObjectQuery<T> objectQuery, params Expression<Func<T, bool>>[] whereExpressions)
            where T : EntityObject, new()
        {
            
            string efConnectionString = GetConnectionString(objectQuery.Context.Connection.ConnectionString);            
            MetadataAccessor.PopulateMetadata(efConnectionString, typeof(T).Assembly);

            List<string> commands = new List<string>();
            foreach(Expression<Func<T, bool>> where in whereExpressions)
            {
                DmlExpressionParser<T> deleteExpressionParser = new DeleteExpressionParser<T>(where);
                string deleteCommand = deleteExpressionParser.GetDmlCommand();
                commands.Add(deleteCommand);
                //if (logger != null) logger.WriteLine(deleteCommand);
            }
            
            EntityConnectionStringBuilder connectionBuilder = new EntityConnectionStringBuilder(efConnectionString);
            SqlContext context = new SqlContext(objectQuery.Context, connectionBuilder.ProviderConnectionString);
            context.ExecuteCommandTransaction(commands);
            return 0;
        }
        #endregion

        #region Private Methods
        private static string GetConnectionString(string connectionStringName)
        {
            
            
            string efConnectionName = (connectionStringName.Split('='))[1];
            //Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return System.Configuration.ConfigurationManager.ConnectionStrings[efConnectionName].ConnectionString;
            
            //return configuration.ConnectionStrings.ConnectionStrings[efConnectionName].ConnectionString;
            
        }
        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommandExpression<T>
        where T:EntityObject,new()
    {
        public CommandExpression()
        {

        }

        public CommandExpression(Expression<Func<T, T>> setExpression, Expression<Func<T, bool>> whereExpression)        
        {
            this.SetExpression = setExpression;
            this.WhereExpression = whereExpression;
        }

        public Expression<Func<T, T>> SetExpression { get; set; }
        public Expression<Func<T, bool>> WhereExpression { get; set; }
    }
}
