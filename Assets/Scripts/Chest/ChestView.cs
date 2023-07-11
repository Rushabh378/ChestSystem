using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestView : MonoBehaviour
    {
        private ChestController controller;

        public void Start()
        {
            controller.ActivateStand();
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