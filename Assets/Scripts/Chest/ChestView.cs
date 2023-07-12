using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestView : MonoBehaviour
    {
        private ChestController controller;
        internal bool setTimer = false;

        public void Start()
        {
            controller.ActivateStand();
        }
        public void Update()
        { 
            if (setTimer)
            {
                controller.SetTimer();
            }
        }
        public void SetController(ChestController controller)
        {
            this.controller = controller;
        }

        public void OnDestroy()
        {
            controller.DeactivatStand();
        }
    }
}