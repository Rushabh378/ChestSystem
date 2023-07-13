using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private VoidEventChannel clickedOpen;
        private ChestController controller;
        internal bool timerOn;

        public void Start()
        {
            timerOn = false;
            controller.ActivateStand();
            clickedOpen.unityEvent.AddListener(ActivateTimer);

        }
        public void Update()
        {
            if (timerOn)
            {
                controller.SetTimer();
            }
        }
        public void ActivateTimer()
        {
            timerOn = true;
            Debug.Log("timer is set to " + timerOn + " for " + gameObject.name);
        }
        public void SetController(ChestController controller)
        {
            this.controller = controller;
        }

        public void OnDisable()
        {
            controller.DeactivatStand();
        }
    }
}