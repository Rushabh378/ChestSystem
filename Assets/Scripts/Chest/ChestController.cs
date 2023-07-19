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

        private Enums.Process chestProcess = Enums.Process.none;

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
            UIManager.Instance.OptionToOpen(ChestCost(),this);
        }

        public int ChestCost()
        {
            return (int)Mathf.Ceil(model.Timer / 10f);
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

        private void OpenChest()
        {
            if (chestProcess == Enums.Process.inQueue)
                return;

            view.timerOn = false;
            ChestManager.Instance.RemoveFromQueue(view);
            RemoveChest();
        }

        public void OpenImmediatly(int cost)
        {
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

        private void RemoveChest()
        {
            view.GetComponent<Animator>().SetBool("Open", true);

            GetRewords?.Invoke(model.GetCoins(), model.GetGems());

            ChestManager.Instance.RemoveChest(model.SlotNumber);
            GameObject.Destroy(view.gameObject);
        }

        private void UpdateTimer(float currentTime)
        {
            currentTime += 1;

            float hours = Mathf.FloorToInt(currentTime / 3600);
            float minutes = Mathf.FloorToInt((currentTime % 3600)/60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            model.StandText = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);
        }
        public void StartChestOpen()
        {
            if (chestProcess == Enums.Process.processing)
                return;

            if(chestProcess == Enums.Process.inQueue)
            {
                ChestManager.Instance.RemoveFromQueue(view); //will remove and start open chest timer in queue.
                model.StandText = "Open";
                return;
            }

            chestProcess = ChestManager.Instance.ActivateTimer(view);

            if(chestProcess == Enums.Process.inQueue)
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
                model.StandText = "Open";
                model.StandAnimtor.SetBool("Activate", false);
            }
        }
    }
}