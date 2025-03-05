namespace Game.Ecs.Field.Systems
{
    using System;

    public static class ArrayExtensions
    {
        private static readonly System.Random _random = new System.Random();

        public static void Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            for (int i = n - 1; i > 0; i--)
            {
                int j = _random.Next(0, i + 1);
                (array[i], array[j]) = (array[j], array[i]); // Современный синтаксис swap
            }
        }
        
        public static void Shuffle<T>(this Span<T> span)
        {
            var random = new System.Random();
            int n = span.Length;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (span[i], span[j]) = (span[j], span[i]);
            }
        }
    }
    
    
}