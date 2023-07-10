using UnityEngine;

namespace ChestSystem
{
    public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
    {

        private static T instance;
        public static T Instance { get { return instance; } }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
