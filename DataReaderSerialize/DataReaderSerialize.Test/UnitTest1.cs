using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;

namespace DataReaderSerialize.Test
{
    [TestClass]
    public class UnitTest1
    {

        public DbDataReader dataReader { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            DataTable table = new DataTable();
            DataColumn idColumn = table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Rows.Add(new object[] { 1, "Iran" });
            table.Rows.Add(new object[] { 2, "Irak" });
            table.Rows.Add(new object[] { 3, "Isaac" });

            dataReader = table.CreateDataReader();
        }

        [TestMethod]
        public void TestMethod1()
        {
            List<Item> items = Helper.GetListAs<Item>(dataReader);

            Assert.AreEqual("Iran", items[0].Name);
            Assert.AreEqual("Irak", items[1].Name);
            Assert.AreEqual("Isaac", items[2].Name);
        }
    }
}
