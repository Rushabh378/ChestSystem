using UnityEngine;

namespace ChestSystem
{
    public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
    {

        private static T _instance;
        public static T Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
