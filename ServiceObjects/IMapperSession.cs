using testWebApi.Domain;

namespace testWebApi.ServiceObjects
{
    public interface IMapperSession
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save(Book entity);
        Task Delete(Book entity);

        IQueryable<Book> Books { get; }
    }
}

