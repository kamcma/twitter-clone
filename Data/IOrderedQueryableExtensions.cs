namespace System.Linq
{
    public static class IOrderedQueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IOrderedQueryable<T> query, int pageNumber, int pageLength)
            where T : class
        {
            return query.Skip(pageNumber * pageLength).Take(pageLength);
        }
    }
}
