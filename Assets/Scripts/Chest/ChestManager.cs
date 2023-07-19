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
        private List<ChestView> runningChest = new();

        [SerializeField] private List<ChestType> chestTypes;
        [SerializeField] private Availablity[] chestSlot = new Availablity[MAXSLOT];
        [SerializeField] private int maxQueue = 2;

        private ChestModel[] model = new ChestModel[MAXSLOT];
        private ChestController[] controller = new ChestController[MAXSLOT];
        private int current = 0;

        public static event Action<string> PopUp;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                int slotIndex = GetAvailableSlot();
                if (slotIndex != -1 && chestSlot[slotIndex].gameObject)
                {
                    int index = random.Next(chestTypes.Count);
                    model[slotIndex] = new ChestModel(chestTypes[index],chestSlot[slotIndex],slotIndex);
                    controller[slotIndex] = new ChestController(model[slotIndex]);
                    current++;
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

        public Process ActivateTimer(ChestView chest)
        {
            if (runningChest.Count == 0)
            {
                chest.timerOn = true;
                runningChest.Add(chest);
                Debug.Log("(AT1)running chest count : " + runningChest.Count);
                return Process.processing;
            }
            else if(runningChest.Count <= maxQueue)
            {
                runningChest.Add(chest);
                Debug.Log("(AT1)running chest count : " + runningChest.Count);
                return Process.inQueue;
            }
            else
            {
                PopUp("Queue Full");
                return Process.cancelled;
            }
        }

        public Process RemoveFromQueue(ChestView chest)
        {
            Debug.Log("(RFQ)running chest count : " + runningChest.Count);
            if(runningChest.Count == 0)
            {
                Debug.Log("queue list empty");
                return Process.cancelled;
            }
            else if(runningChest[0].name == chest.name)
            {
                runningChest.Remove(chest);
                Debug.Log("removing chest from front");

                if(runningChest.Count != 0)
                {
                    runningChest[0].timerOn = true;
                    Debug.Log("and starting timer of front chest.");
                    return Process.processing;
                }
            }
            else if (ChestAvailable(chest))
            {
                runningChest.Remove(chest);
                Debug.Log("removing chest from the middle.");
                return Process.complete;
            }
            Debug.Log("uncought process in removefromqueue");
            return Process.failled;
        }
        public bool ChestAvailable(ChestView chestView)
        {
            foreach(ChestView chest in runningChest)
            {
                if(chest.name == chestView.name)
                {
                    return true;
                }
            }
            return false;
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