﻿using UnityEngine;
using TMPro;

namespace ChestSystem.Chest
{
    public class ChestModel
    {
        private ChestController controller;
        public ChestType chestType;
        private GameObject chestSlot;
        private TextMeshProUGUI standText;

        public Animator standAnimtor;
        
        public ChestModel(ChestType chestType, Availablity chestSlot, GameObject chestStand)
        {
            this.chestType = chestType;
            this.chestSlot = chestSlot.gameObject;

            standAnimtor = chestStand.GetComponent<Animator>();
            standText = chestStand.GetComponentInChildren<TextMeshProUGUI>();
        }

        public Vector3 ChestPosition => chestSlot.transform.position;
        public string StandText { get => standText.text; set => standText.text = value; }

        public void SetController(ChestController controller)
        {
            this.controller = controller;
        }
    }
}