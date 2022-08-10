using NHibernate;
using System;

namespace testWebApi.Migrations
{
    public class Migration
    {
        public Migration()
        { }

        public void DoMigrate(string version, NHibernate.ISession session)
        {
            string script = File.ReadAllText($"/Users/yuqing.lu/my-projects/testWebApi/Migrations/{version}.sql");
            session.CreateSQLQuery(script).ExecuteUpdate();
        }
    }
}
