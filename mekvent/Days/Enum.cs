using System;

namespace mekvent.Days
{
    public static class Enum
    {
        public static T ParseEnum<T>(string input)
        {
            if(!System.Enum.TryParse(typeof(T), input, true, out object e))
            {
                throw new ArgumentException($"Could not parse {input} to enum of type {typeof(T).Name}");
            }

            return (T)e;
        }
    }
}