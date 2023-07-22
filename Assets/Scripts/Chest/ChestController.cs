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

            model.btnOpen.onClick.AddListener(ChestOpeningProcess);
        }

        public void ChestOpeningProcess()
        {
            //UIManager.Instance.OptionToOpen(ChestCost(), this);
            GameObject optionWindow = UIManager.Instance.GetWindow("Open Options");
            TextMeshProUGUI[] textHolder = new TextMeshProUGUI[2];
            textHolder[0] = optionWindow.GetComponentInChildren<TextMeshProUGUI>();
            Button[] buttons = optionWindow.GetComponentsInChildren<Button>();

            textHolder[0].text = ChestCost().ToString();
            buttons[0].onClick.AddListener(OpenImmediatly);

            textHolder[1] = buttons[1].GetComponent<TextMeshProUGUI>();

            if (ChestManager.Instance.ChestRunning && chestProcess == Enums.Process.idle)
            {
                buttons[1].onClick.AddListener(AddChestToQueue);
            }
            else if (chestProcess == Enums.Process.queue)
            {
                //textHolder[1].text = "cancel Queue";
                buttons[1].onClick.AddListener(CancelQueue);
            }
            else if (chestProcess == Enums.Process.idle)
            {
                //textHolder[1].text = "Start Timer";
                buttons[1].onClick.AddListener(StartOpeningChest);
            }

            optionWindow.SetActive(true);
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

        private void OpenChest()
        {
            view.timerOn = false;

            view.GetComponent<Animator>().SetBool("Open", true);

            GetRewords?.Invoke(model.GetCoins(), model.GetGems());

            if (chestProcess == Enums.Process.opening)
            {
                ChestManager.Instance.OpenChestInQueue();
            }
            else
            {
                ChestManager.Instance.RemoveFromQueue(model);
            }

            ChestManager.Instance.RemoveChest(model.SlotNumber);
            view.gameObject.SetActive(false);
        }

        public void OpenImmediatly()
        {
            int cost = ChestCost();
            if(Currancy.Instance.gems.Amount >= cost)
            {
                Currancy.Instance.gems.minus(cost);
                OpenChest();
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