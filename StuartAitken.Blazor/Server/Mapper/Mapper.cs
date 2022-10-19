namespace StuartAitken.Blazor.Server.Mapper
{
    public static class Mapper
    {
        #region Public Methods

        public static TDest Map<TSource, TDest>(TSource source) where TDest : new()
        {
            if (source == null)
                throw new Exception("TSource is null!");

            var props = source
                .GetType()
                .GetProperties(
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance
                );

            var dest = new TDest();
            var destProps = dest.GetType()
                .GetProperties(
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance
                );

            foreach (var prop in props)
            {
                var value = prop.GetValue(source, null);

                var destProp = destProps.FirstOrDefault(
                    p => p.Name == prop.Name && p.PropertyType == prop.PropertyType
                );

                if (destProp != null)
                {
                    destProp.SetValue(dest, value, null);
                }
            }

            return dest;
        }

        public static IEnumerable<TDest> Map<TSource, TDest>(this IEnumerable<TSource> values)
            where TDest : new()
        {
            return values.Select(i => Map<TSource, TDest>(i));
        }

        public static IQueryable<TDest> Map<TSource, TDest>(this IQueryable<TSource> values)
            where TDest : new()
        {
            return values.Select(i => Map<TSource, TDest>(i));
        }

        #endregion Public Methods
    }
}
