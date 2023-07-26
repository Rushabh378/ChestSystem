using System;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.UI;
using static ChestSystem.Enums;

namespace ChestSystem.Chest
{
    public class ChestManager : GenericSingleton<ChestManager>
    {

        private const int MAXSLOT = 4;
        private System.Random random = new System.Random();
        private List<int> queueList = new();

        [SerializeField] private List<ChestType> chestTypes;
        [SerializeField] private int maxQueue = 2;

        private ChestSlot chestSlot;
        private List<ChestModel> models;
        private List<ChestController> controllers;

        public bool ChestRunning = false;
        public static event Action<string> PopUp;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                chestSlot = SlotManager.Instance.AvailableSlot();

                if (controllers.Count <= MAXSLOT && chestSlot != null)
                {
                    int index = random.Next(chestTypes.Count);
                    int id = random.Next(1000, 10000);

                    ChestModel model = new ChestModel(chestTypes[index], id, chestSlot.ChestPosition.position);
                    ChestController controller = new ChestController(model);

                    chestSlot.Chest = controller;

                    models.Add(model);
                    controllers.Add(controller);

                    Debug.Log(chestTypes[index].name + " is created with ID = " + id);
                }
                else
                {
                    PopUp("Slot Full");
                }
            }
        }
    }
}