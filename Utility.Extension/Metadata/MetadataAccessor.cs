using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.Xml.Linq;
using System.Reflection;
using System.IO;

namespace Utility.Extension.Metadata
{
    /// <summary>
    /// Accesor to metadata information 
    /// </summary>
    internal  static class MetadataAccessor
    {
        #region Members

        static List<EntityMapping> entityMappings = null;

        #endregion

        #region Private Methods

        private static XElement GetXmlMslFragment(string mslSpec, Assembly assemblyMetadata)
        {
            //Get resource paths
            string[] resourcePaths = mslSpec.Split('/');
            
            //Get resourceElement
            if (resourcePaths[0] != "*")
            {
                string assemblyName = resourcePaths[0];
                Assembly assembly = Assembly.Load(assemblyName);
                StringBuilder sb = new StringBuilder();

                for(int i=1;i<resourcePaths.Length;i++)
                    sb.Append(resourcePaths[i]).Append(".");

                sb.Length = sb.Length - 1; // remove last dot

                Stream resourceStream = assembly.GetManifestResourceStream(sb.ToString());
                return XElement.Load(GetStringMslFragment(resourceStream),LoadOptions.None);
            }
            else
            {
                //Stream resourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream(resourcePaths[1]);
                Stream resourceStream = assemblyMetadata.GetManifestResourceStream(resourcePaths[1]);
                return XElement.Load(GetStringMslFragment(resourceStream),LoadOptions.None);
            }
        }
        private static StreamReader GetStringMslFragment(Stream resourceStream)
        {
            StreamReader reader = new StreamReader(resourceStream);
            return reader;
        }
        private static void CreateEntityMappings(XElement entityContainerMappingElement,string @namespace)
        {
            entityMappings = (from e in entityContainerMappingElement.Descendants()
                                where e.Name == XName.Get("EntitySetMapping", @namespace)
                                select EntityMapping.CreateEntityMapping(e, e.GetDefaultNamespace())).ToList();
        }
        #endregion

        #region Public Static Methods

        /// <summary>
        /// Populate C-S Specification into a internal List
        /// </summary>
        /// <param name="connectionString">ConnectionString of Model</param>
        public static void PopulateMetadata(string connectionString, Assembly assemblyMetadata)
        {
            if (entityMappings == null)
            {
                EntityConnectionStringBuilder efConnectionStringBuilder = new EntityConnectionStringBuilder(connectionString);

                string mslSpec = (from s in efConnectionStringBuilder.Metadata.Split('|')
                                  where s.Contains(".msl")
                                  select s).Single();

                XElement entityContainerMappingElement = GetXmlMslFragment(mslSpec.Replace("res://", ""), assemblyMetadata);
                XNamespace entityContainerMappingNamespace = entityContainerMappingElement.GetDefaultNamespace();

                CreateEntityMappings(entityContainerMappingElement, entityContainerMappingNamespace.NamespaceName);
            }

        }
        /// <summary>
        /// Get Column name in storage table 
        /// </summary>
        /// <typeparam name="T">type of entity</typeparam>
        /// <param name="edmPropertyName">edm property name</param>
        /// <returns>column name in storage table</returns>
        public static string GetColumnNameByEdmProperty<T>(string edmPropertyName)
            where T : EntityObject,new()
        {
            EntityMapping entitymapping =  (from e in entityMappings 
                                            where e.EntityName == typeof(T).Name
                                            select e).Single();

            return string.Format("[{0}]",entitymapping[edmPropertyName]);
                    


        }
        /// <summary>
        /// Get the table name
        /// </summary>
        /// <param name="edmTypeName">Edm entity type name</param>
        /// <returns>table name in C-S mapping for entity</returns>
        public static string GetTableNameByEdmType(string edmTypeName)
        {
            string tableName = (from e in entityMappings
                    where e.EntityName == edmTypeName 
                    select e.TableName).Single();

            return string.Format("[{0}]", tableName);
        }
        #endregion
    }
}
