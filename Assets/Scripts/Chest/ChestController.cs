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

            model.btnOpen.onClick.AddListener(ActivateTimer);
        }

        internal void SetTimer()
        {
            model.Timer -= Time.deltaTime;
            UpdateTimer(model.Timer);
            if (model.Timer <= 0)
            {
                view.timerOn = false;
                view.GetComponent<Animator>().SetBool("Open", true);
            }
        }

        private void UpdateTimer(float currentTime)
        {
            currentTime += 1;

            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            model.StandText = string.Format("{0:00} : {1:00}", minutes, seconds);
        }

        private void ActivateTimer() => view.timerOn = true;

        internal void ActivateStand()
        {
            model.StandAnimtor.SetBool("Activate", true);
        }
        internal void DeactivatStand()
        {
            if(model.StandAnimtor != null)
            {
                model.StandAnimtor.SetBool("Activate", false);
            }
        }
    }
}