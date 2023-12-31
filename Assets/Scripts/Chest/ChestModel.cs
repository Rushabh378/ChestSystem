﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ChestSystem.UI;

namespace ChestSystem.Chest
{
    public class ChestModel
    {
        private ChestController controller;
        private ChestType chestType;
        private TextMeshProUGUI standText;

        public ChestSlot chestSlot;
        public float Timer;
        public int ChestId;

        public ChestModel(ChestType chestType, int chestId, ChestSlot chestSlot)
        {
            this.chestType = chestType;
            this.ChestId = chestId;
            this.chestSlot = chestSlot;

            Timer = chestType.timerSeconds;
        }
        public Vector3 Position => chestSlot.ChestPosition.position;
        public string ButtonText { get => chestSlot.SlotText.text; set => chestSlot.SlotText.text = value; }

        public ChestView Chest => chestType.chestPrefeb;

        public int GetCoins()
        {
            return Random.Range(chestType.coinsRange, chestType.to + 1);
        }
        public int GetGems()
        {
            return Random.Range(chestType.gemsRange, chestType._to + 1);
        }

        public void SetController(ChestController controller)
        {
            this.controller = controller;
        }

        public void FreeSlot()
        {
            chestSlot.Deactivate();
        }
    }
}