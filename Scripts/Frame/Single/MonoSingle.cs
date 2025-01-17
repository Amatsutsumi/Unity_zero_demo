using UnityEngine;

public class MonoSingle<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject(typeof(T).ToString());
            //¹ý³¡¾°²»ÒÆ³ý
            DontDestroyOnLoad(go);
            instance = go.AddComponent<T>();
        }
        return instance;
    }
}
