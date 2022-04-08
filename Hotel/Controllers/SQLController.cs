using Hotel.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class SQLController : Controller
    {
        // GET: SQL
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SQL(string query)
        {

            using (var ctx = new Db())
            using (var cmd = ctx.Database.Connection.CreateCommand())
            {
                ctx.Database.Connection.Open();
                cmd.CommandText = query;
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        var model = Read(reader).ToList();
                        return View(model);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.SQLerror = e.Message;
                    return View("SQLerror");
                }
            }
        }

        private static IEnumerable<object[]> Read(DbDataReader reader)
        {
            while (reader.Read())
            {
                var values = new List<object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetValue(i));
                }
                yield return values.ToArray();
            }
        }
    }
}