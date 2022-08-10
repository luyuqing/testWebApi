using testWebApi.Domain;
using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace testWebApi.Map
{
    public class BookMap : ClassMapping<Book>
    {
        public BookMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Guid);
                x.Type(NHibernateUtil.Guid);
                x.Column("Id");
                x.UnsavedValue(Guid.Empty);
            });

            Property(b => b.Title, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.StringClob);
                x.NotNullable(true);
            });

            Property(b => b.Publisher, x =>
            {
                x.Length(30);
                x.Type(NHibernateUtil.StringClob);
                x.NotNullable(true);
            });

            Table("Books");
        }
    }
}