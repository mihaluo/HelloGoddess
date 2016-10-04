using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HelloGoddess.Core.MongoDb
{
    public static class UpdateDefiinitionGenerate<TEntity>
    {
        public static UpdateDefinition<TEntity> Gen(TEntity entity)
        {
            UpdateDefinition<TEntity> updateDefinition = null;
            var properties = entity.GetType().GetTypeInfo().GetProperties();
            foreach (var item in properties)
            {
                var value = item.GetValue(entity);
                if (value == null)
                {
                    continue;
                }
                if (item.PropertyType == typeof(string))
                {
                    updateDefinition = GenerateUpdateDefinitionByType(updateDefinition, item.Name, value.ToString());
                }
                else if (item.PropertyType == typeof(int))
                {
                    updateDefinition = GenerateUpdateDefinitionByType(updateDefinition, item.Name, Convert.ToInt32(value));
                }
                else if (item.PropertyType == typeof(long))
                {
                    updateDefinition = GenerateUpdateDefinitionByType(updateDefinition, item.Name, Convert.ToInt64(value));
                }
                else if (item.PropertyType == typeof(Guid))
                {
                    updateDefinition = GenerateUpdateDefinitionByType(updateDefinition, item.Name, Guid.Parse(value.ToString()));
                }
                else if (item.PropertyType == typeof(DateTime))
                {
                    updateDefinition = GenerateUpdateDefinitionByType(updateDefinition, item.Name, DateTime.Parse(value.ToString()));
                }
            }
            return updateDefinition;
        }

        private static UpdateDefinition<TEntity> GenerateUpdateDefinitionByType<TType>(UpdateDefinition<TEntity> updateDefinition, string fieldName, TType value)
        {
            FieldDefinition<TEntity, TType> fieldDefinition = new StringFieldDefinition<TEntity, TType>(fieldName);
            if (updateDefinition == null)
            {
                return Builders<TEntity>.Update.Set(fieldDefinition, value);
            }
            return updateDefinition.Set(fieldDefinition, value);
        }
    }
}
