using UnityEngine;

public class SingleTon<T> where T : new()
{
    private static T instance;
    protected static readonly object _lock = new object();
    public static T Instance()
    {
        if (instance == null)
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new T();
                }
            }
        }
        return instance;
    }
}

