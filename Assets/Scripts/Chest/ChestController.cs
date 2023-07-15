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
                OnChestOpen();
            }
        }

        private void OnChestOpen()
        {
            view.timerOn = false;
            ChestManager.Instance.RemoveFromQueue(view);
            view.GetComponent<Animator>().SetBool("Open", true);
        }

        private void UpdateTimer(float currentTime)
        {
            currentTime += 1;

            float hours = Mathf.FloorToInt(currentTime / 3600);
            float minutes = Mathf.FloorToInt((currentTime % 3600)/60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            model.StandText = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);
        }
        private void ActivateTimer()
        {
            if(model.StandText == "In Queue")
            {
                ChestManager.Instance.RemoveFromQueue(view);
                model.StandText = "Open";
                return;
            }

            if(ChestManager.Instance.OpenChest(view) == Enums.Process.inQueue)
            {
                model.StandText = "In Queue";
            }
        }

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