using UnityEngine;
using UnityEngine.Events;

namespace ChestSystem
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "Event Channels/Void Event")]
    public class VoidEventChannel : ScriptableObject
    {
        public UnityEvent unityEvent;

        public void RaiseEvent()
        {
            if(unityEvent != null)
            {
                unityEvent.Invoke();
            }
        }
    }
}