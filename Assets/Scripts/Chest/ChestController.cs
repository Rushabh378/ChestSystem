using System;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.UI;
using ChestSystem.CurrancySpace;
using TMPro;
using UnityEngine.Events;

namespace ChestSystem.Chest
{
    public class ChestController
    {
        private ChestModel model;
        private ChestView view;

        private float minute = 60f;

        public Enums.State ChestState;

        public static event Action<int, int, UnityAction> GetRewords;

        public ChestController(ChestModel model)
        {
            view = GameObject.Instantiate<ChestView>(model.chestType.chestPrefeb, model.Position, Quaternion.identity);

            this.model = model;

            this.view.SetController(this);
            this.model.SetController(this);

            ChestState = Enums.State.idle;
        }
        private int ChestCost()
        {
            return (int)Mathf.Ceil(model.Timer / (minute * 10));
        }
        public void OpeningOption()
        {
            string title = "Open Now?";
            string message = "Do you want open chest for " + ChestCost() + " Gems";

            UIManager.Instance.ShowPopup(title, message, OpenImmediatly, StartTimer, "Open Now", "Start Timer");
        }
        private void StartTimer()
        {
            view.timerOn = true;
        }
        private void OpenImmediatly()
        {
            Debug.Log("Opening immediatly");
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
            view.timerOn = false;
            view.GetComponent<Animator>().SetBool("Open", true);

            GetRewords.Invoke(model.GetCoins(), model.GetGems(), RemoveChest);
        }
        public void RemoveChest()
        {
            GameObject.Destroy(view.gameObject);
            ChestManager.Instance.RemoveChest(model);
        }

        private void UpdateTimer(float currentTime)
        {
            currentTime += 1;

            float hours = Mathf.FloorToInt(currentTime / 3600);
            float minutes = Mathf.FloorToInt((currentTime % 3600)/60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            model.ButtonText = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);
        }
    }
}