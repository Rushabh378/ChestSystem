using System;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestController
    {
        private ChestModel model;
        private ChestView view;
        public ChestController(ChestModel model)
        {
            view = GameObject.Instantiate<ChestView>(model.chestType.chestPrefeb, model.ChestPosition, Quaternion.identity);

            this.model = model;

            this.view.SetController(this);
            this.model.SetController(this);

            model.standButton.clicked += ActivateTimer;
        }

        internal void SetTimer()
        {
            model.Timer -= Time.deltaTime;
            model.StandText = model.Timer.ToString();

            if(model.Timer <= 0)
            {
                view.GetComponent<Animator>().SetBool("Open", true);
            }
        }

        private void ActivateTimer() => view.setTimer = true;

        internal void ActivateStand()
        {
            model.StandAnimtor.SetBool("Activate", true);
        }
        internal void DeactivatStand()
        {
            model.StandAnimtor.SetBool("Activate", false);
        }
    }
}