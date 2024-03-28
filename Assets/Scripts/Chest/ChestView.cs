using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestView : MonoBehaviour
    {
        private ChestController _controller;
        internal bool timerOn;

        public void Start()
        {
            timerOn = false;
        }
        public void Update()
        {
            if (timerOn)
            {
                _controller.SetTimer();
            }
        }
        public void SetController(ChestController controller)
        {
            this._controller = controller;
        }
    }
}