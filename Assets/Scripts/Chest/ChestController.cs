using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestController : MonoBehaviour
    {
        private ChestModel model;
        private ChestView view;
        public ChestController(ChestModel model)
        {
            view = Instantiate<ChestView>(model.chestType.chestPrefeb, model.ChestPosition, Quaternion.identity);

            this.model = model;

            this.view.SetController(this);
            this.model.SetController(this);
        }

        internal void ActivateStand()
        {
            model.standAnimtor.SetBool("Activate", true);
        }
        internal void DeactivatStand()
        {
            model.standAnimtor.SetBool("Activate", false);
        }
    }
}