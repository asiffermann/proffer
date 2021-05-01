namespace Proffer.Testing
{
    public static class GenericExtensions
    {
        public static T[] Yield<T>(this T item)
        {
            return new T[1] { item };
        }
    }
}
