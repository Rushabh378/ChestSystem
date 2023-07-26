using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestView : MonoBehaviour
    {
        private ChestController controller;
        internal bool timerOn;

        public void Start()
        {
            timerOn = false;
        }
        public void Update()
        {
            if (timerOn)
            {
                controller.SetTimer();
            }
        }
        public void SetController(ChestController controller)
        {
            this.controller = controller;
        }
    }
}