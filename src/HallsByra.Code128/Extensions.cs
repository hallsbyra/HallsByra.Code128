namespace HallsByra.Code128;

internal static class Extensions
{
    public static (IEnumerable<T> truthy, IEnumerable<T> falsy) SplitBy<T>(this IEnumerable<T> self, Predicate<T> predicate)
    {
        var truthy = new LinkedList<T>();
        var falsy = new LinkedList<T>();
        foreach (var t in self)
            if (predicate(t))
                truthy.AddLast(t);
            else
                falsy.AddLast(t);
        return (truthy, falsy);
    }
}
