
using UnityEngine;

public class Monosingleton<T> : MonoBehaviour where T : Monosingleton<T>
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if(_instance)
            {
                Destroy(FindObjectOfType<T>());
            }
            else
            {
                _instance = FindObjectOfType<T>();
            }
 
            return _instance;
        }
    }
}
