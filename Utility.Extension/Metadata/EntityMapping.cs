using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Utility.Extension.Metadata
{
    /// <summary>
    /// Structure to storage mapping information
    /// </summary>
    internal sealed class EntityMapping
    {  
        #region Members

        Dictionary<string, string> columnMappings;

        #endregion

        #region Properties

        private string mTableName;
        /// <summary>
        /// Get the table name
        /// </summary>
        public string TableName
        {
            get
            {
                return mTableName;
            }
        }
        private string mEntityName;
        /// <summary>
        /// Get the entity Name
        /// </summary>
        public string EntityName
        {
            get
            {
                return mEntityName;
            }
        }
        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="propertyName">Property Name of entity</param>
        /// <returns>Column name mapped </returns>
        public string this[string propertyName]
        {
            get
            {
                return columnMappings[propertyName];
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="tableName">Name of Storage Table</param>
        /// <param name="entityName">Name of Entity in EDM</param>
        EntityMapping(string tableName,string entityName)
        {
            this.columnMappings = new Dictionary<string, string>();

            this.mTableName = tableName;
            this.mEntityName = entityName;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add entity property mapping
        /// </summary>
        /// <param name="propertyName">Name of Property</param>
        /// <param name="columnName">Name of column in table</param>
        public void AddPropertyMapping(string propertyName, string columnName)
        {
            this.columnMappings.Add(propertyName, columnName);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create EntityMapping instance 
        /// </summary>
        /// <param name="entityMappingElement">EntityMapping XML Fragment</param>
        /// <param name="defaultNamespace">Namespace of XML file</param>
        /// <returns>EntityMapping of specific Entity</returns>
        public static EntityMapping CreateEntityMapping(XElement entityMappingElement,XNamespace defaultNamespace)
        {
            //Recover entity name and table name
            string entityName = entityMappingElement.Attribute(XName.Get("Name", "")).Value;

            XElement mappingFragmentElement = (from e in entityMappingElement.Descendants() 
                                        where e.Name == XName.Get("MappingFragment", defaultNamespace.NamespaceName) 
                                        select e).FirstOrDefault<XElement>();

            XElement entityTypeMapping = (from e in entityMappingElement.Descendants()
                               where e.Name == XName.Get("EntityTypeMapping", defaultNamespace.NamespaceName)
                               select e).FirstOrDefault<XElement>();

            string typeName = entityTypeMapping.Attribute(XName.Get("TypeName", "")).Value;
            typeName = typeName.Contains(".") ? typeName.Substring(typeName.LastIndexOf('.') + 1) : typeName;

            entityName = typeName;

            string tableName = mappingFragmentElement.Attribute(XName.Get("StoreEntitySet", "")).Value;
            

            //Create EntityMapping instance
            EntityMapping entityMapping = new EntityMapping(tableName, entityName);

            //Recover ScalarProperty and add items to entityMapping

            IEnumerable<XElement> scalarPropertyElementCollection = from e in mappingFragmentElement.Descendants() 
                                                                    where e.Name == XName.Get("ScalarProperty", defaultNamespace.NamespaceName) 
                                                                    select e;
            string propertyName = string.Empty;
            string columnName = string.Empty;
            foreach (XElement item in scalarPropertyElementCollection)
            {
                propertyName = item.Attribute(XName.Get("Name", "")).Value;
                columnName = item.Attribute(XName.Get("ColumnName", "")).Value;

                entityMapping.AddPropertyMapping(propertyName, columnName);
            }



            return entityMapping;
        }

        #endregion
    }
}
