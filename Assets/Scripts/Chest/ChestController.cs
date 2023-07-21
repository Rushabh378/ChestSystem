using System;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.UI;
using ChestSystem.CurrancySpace;

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

            model.btnOpen.onClick.AddListener(ChestOpeningProcess);
        }

        public void ChestOpeningProcess()
        {
            UIManager.Instance.OptionToOpen(ChestCost(), this);
        }
        public void AddChestToQueue()
        {
            chestProcess = ChestManager.Instance.AddToQueue(model);
        }
        public int ChestCost()
        {
            return (int)Mathf.Ceil(model.Timer / (minute * 10));
        }

        internal void SetTimer()
        {
            model.Timer -= Time.deltaTime;
            UpdateTimer(model.Timer);

            if (model.Timer <= 0)
            {
                OpenChest();
            }
        }

        private void RemoveChest()
        {
            view.GetComponent<Animator>().SetBool("Open", true);

            GetRewords?.Invoke(model.GetCoins(), model.GetGems());

            ChestManager.Instance.OpenChestInQueue();
            ChestManager.Instance.RemoveChest(model.SlotNumber);
            view.gameObject.SetActive(false);
        }

        private void OpenChest()
        {
            view.timerOn = false;
            RemoveChest();

        }

        public void OpenImmediatly()
        {
            int cost = ChestCost();
            if(Currancy.Instance.gems.Amount >= cost)
            {
                Currancy.Instance.gems.minus(cost);
                RemoveChest();
            }
            else
            {
                PopUp?.Invoke("Not Enough Gems");
            }
            
        }

        internal void CancelQueue()
        {
            chestProcess = ChestManager.Instance.RemoveFromQueue(model);
        }

        private void UpdateTimer(float currentTime)
        {
            currentTime += 1;

            float hours = Mathf.FloorToInt(currentTime / 3600);
            float minutes = Mathf.FloorToInt((currentTime % 3600)/60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            model.StandText = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);
        }
        public void StartOpeningChest()
        {
            ChestManager.Instance.ChestRunning = view.timerOn = true;
            chestProcess = Enums.Process.opening;
        }

        internal void ActivateStand()
        {
            model.StandAnimtor.SetBool("Activate", true);
        }
        internal void DeactivatStand()
        {
            if(model.StandAnimtor != null)
            {
                model.StandText = "Open";
                model.StandAnimtor.SetBool("Activate", false);
            }
        }
    }
}