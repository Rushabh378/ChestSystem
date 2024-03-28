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
        private ChestModel _model;
        private ChestView _view;

        private float _minute = 60f;

        public Enums.States ChestState;

        public static event Action<int, int, UnityAction> GetRewords;

        public ChestController(ChestModel model)
        {
            this._view = GameObject.Instantiate<ChestView>(model.Chest, model.Position, Quaternion.identity);

            this._model = model;

            this._view.SetController(this);
            this._model.SetController(this);
        }
        private int ChestCost()
        {
            return (int)Mathf.Ceil(_model.Timer / (_minute * 10));  // lowering cost by minute
        }
        public void OpeningOption()
        {
            string title = "Open Now?";
            string message = "Do you want open chest for " + ChestCost() + " Gems";

            if (SlotManager.Instance.IsChestRunning())
            {
                UIManager.Instance.ShowPopup(title, message, OpenImmediatly, _model.ChestSlot.ToggleQueue, "Open Now", "Enqueue/Dequeue");
            }
            else
            {
                UIManager.Instance.ShowPopup(title, message, OpenImmediatly, StartTimer, "Open Now", "Start Timer");
            }
        }
        public void StartTimer()
        {
            SlotManager.Instance.StartTimer(_view);
        }
        private void OpenImmediatly()
        {
            int cost = ChestCost();
            if(Currancy.Instance.Gems.Amount >= cost)
            {
                Currancy.Instance.Gems.Subtract(cost);

                if (_model.ChestSlot.IsInQueue())
                {
                    SlotManager.Instance.Dequeue();
                }

                OpenChest();
            }
            else
            {
                UIManager.Instance.ShowPopup("Attention", "You dont have Enough Gems to open chest");
            }
        }
        public void SetTimer()
        {
            _model.Timer -= Time.deltaTime;
            UpdateTimer(_model.Timer);

            if (_model.Timer <= 0)
            {
                OpenChest();
            }
        }
        private void OpenChest()
        {
            _view.timerOn = false;
            _view.GetComponent<Animator>().SetBool("Open", true);
            if (SlotManager.Instance.IsChestRunning(_view))
            {
                SlotManager.Instance.RemoveRunningChest();
            }
            GetRewords.Invoke(_model.GetCoins(), _model.GetGems(), RemoveChest);
        }
        public void RemoveChest()
        {
            GameObject.Destroy(_view.gameObject);
            ChestManager.Instance.RemoveChest(_model);
        }

        private void UpdateTimer(float currentTime)
        {
            currentTime += 1;

            float hours = Mathf.FloorToInt(currentTime / 3600);
            float minutes = Mathf.FloorToInt((currentTime % 3600)/60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            _model.ButtonText = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);
        }
    }
}