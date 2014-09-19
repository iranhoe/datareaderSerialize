using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataReaderSerialize
{
    public class Helper
    {
        public static List<T> GetListAs<T>(DbDataReader reader)
        {
            List<T> items = new List<T>();
            while (reader.Read())
            {
                items.Add(GetAs<T>(reader));
            }

            return items;
        }
        private static T GetAs<T>(DbDataReader reader)
        {
            T genObject = Activator.CreateInstance<T>();
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach(var item in props)
            {
                if (ColumnExists(reader, item.Name) && reader[item.Name] != DBNull.Value)
                {
                    typeof(T).InvokeMember(item.Name, BindingFlags.SetProperty,
                        null, genObject, new Object[] { reader[item.Name] });
                }
            }
            return genObject;
        }
        private static bool ColumnExists(DbDataReader reader, string columnName)
        {
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return (reader.GetSchemaTable().DefaultView.Count > 0);
        }
    }
}
