using System;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.UI;
using ChestSystem.CurrancySpace;
using TMPro;

namespace ChestSystem.Chest
{
    public class ChestController
    {
        private ChestModel model;
        private ChestView view;

        private float minute = 60f;

        public Enums.Process chestProcess = Enums.Process.idle;

        public static event Action<int, int> GetRewords;
        public static event Action<string> PopUp;

        public ChestController(ChestModel model)
        {
            view = GameObject.Instantiate<ChestView>(model.chestType.chestPrefeb, model.ChestPosition, Quaternion.identity);

            this.model = model;

            this.view.SetController(this);
            this.model.SetController(this);
        }
        public int ChestCost()
        {
            return (int)Mathf.Ceil(model.Timer / (minute * 10));
        }
        public void SetTimer()
        {
            model.Timer -= Time.deltaTime;
            UpdateTimer(model.Timer);

            if (model.Timer <= 0)
            {
                OpenChest();
            }
        }
        private void OpenChest()
        {

        }
        private void UpdateTimer(float currentTime)
        {
            currentTime += 1;

            float hours = Mathf.FloorToInt(currentTime / 3600);
            float minutes = Mathf.FloorToInt((currentTime % 3600)/60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            model.StandText = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);
        }
    }
}