using UnityEngine;

// From here: https://www.youtube.com/watch?v=6kWUGEQiMUI&ab_channel=whateep
// Use it:
// 1) Create a subclass:
//    public class MySingletonSO : SingletonScriptableObject<MySingletonSO> {}
// 2) Create the new Asset into the /Assets/Resources folder
// 3) Access to it:
//    MySingletonSO.Instance
//
public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] assets = Resources.LoadAll<T>("");
                if(assets == null || assets.Length < 1)
                {
                    throw new System.Exception($"Not found Singleton Scriptable Object of type: {typeof(T).ToString()}");
                } else if (assets.Length > 1)
                {
                    throw new System.Exception($"More than 1 instance of Singleton Scriptable Object of type: {typeof(T).ToString()} found");
                }
                instance = assets[0];
            }

            return instance;
        }
    }
}
