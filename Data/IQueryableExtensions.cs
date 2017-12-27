namespace System.Linq
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumber, int pageLength)
            where T : class
        {
            return query.Skip(pageNumber * pageLength).Take(pageLength);
        }
    }
}
