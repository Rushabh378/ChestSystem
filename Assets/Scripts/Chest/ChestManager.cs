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
        [SerializeField] private Availablity[] chestSlot = new Availablity[MAXSLOT];
        [SerializeField] private int maxQueue = 2;

        private ChestModel[] model = new ChestModel[MAXSLOT];
        private ChestController[] controller = new ChestController[MAXSLOT];

        public bool ChestRunning = false;
        public static event Action<string> PopUp;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                int slotIndex = GetAvailableSlot();
                if (slotIndex != -1)
                {
                    int index = random.Next(chestTypes.Count);
                    model[slotIndex] = new ChestModel(chestTypes[index],chestSlot[slotIndex], slotIndex);
                    controller[slotIndex] = new ChestController(model[slotIndex]);

                    Debug.Log(chestTypes[index].name + " is created with slot = " + slotIndex);
                }
                else
                {
                    PopUp("Slot Full");
                }
            }
        }
        public void RemoveChest(int slot)
        {
            if(slot < MAXSLOT && slot >= 0)
            {
                model[slot] = null;
                controller[slot] = null;
                chestSlot[slot].available = true;
            }
        }

        public bool QueueEmpty()
        {
            return (queueList.Count == 0);
        }

        public Process AddToQueue(ChestModel model)
        {
            if (queueList.Count > maxQueue)
            {
                PopUp.Invoke("Queue Full");
                return Process.idle;
            }
            model.StandText = "InQueue";
            queueList.Add(model.SlotNumber);
            Debug.Log("added to queue :" + model.SlotNumber);
            return Process.queue;
        }

        public Process RemoveFromQueue(ChestModel model)
        {
            foreach(int number in queueList)
            {
                if(number == model.SlotNumber)
                {
                    queueList.Remove(number);
                    model.StandText = "Open";
                    Debug.Log("added to queue :" + model.SlotNumber);
                    return Process.idle;
                }
            }
            Debug.LogWarning("Trying to remove chest that isn't in queue this can effect your chest process.");
            return Process.cancelled;
        }

        public void OpenChestInQueue()
        {
            if(QueueEmpty() == false)
            {
                controller[queueList[0]].StartOpeningChest();
                RemoveFromQueue(model[queueList[0]]);
            } 
            else
            {
                ChestRunning = false;
            }
        }
        private int GetAvailableSlot()
        {
            for(int i = 0; i < MAXSLOT; i++)
            {
                if(chestSlot[i] == null)
                {
                    return -1;
                }
                if (chestSlot[i].available)
                {
                    chestSlot[i].available = false;
                    return i;
                }
            }
            return -1;
        }
    }
}