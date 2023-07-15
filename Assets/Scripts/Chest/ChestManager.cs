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
                    model[current] = new ChestModel(chestTypes[index],chestSlot[slotIndex],UIManager.Instance.chestStand[slotIndex]);
                    controller[current] = new ChestController(model[current]);
                    current++;
                }
                else
                {
                    PopUp("Slot Full");
                }
            }
        }

        public Process OpenChest(ChestView chest)
        {
            if (runningChest.Count == 0)
            {
                chest.timerOn = true;
                runningChest.Add(chest);
                return Process.done;
            }
            else if(runningChest.Count <= maxQueue)
            {
                runningChest.Add(chest);
                return Process.inQueue;
            }
            else
            {
                PopUp("Queue Full");
                return Process.cancelled;
            }
        }

        public void RemoveFromQueue(ChestView chest)
        {
            if(runningChest[0] == chest)
            {
                runningChest.Remove(chest);
                if(runningChest[0] != null)
                {
                    runningChest[0].timerOn = true;
                }
            }
            else
            {
                runningChest.Remove(chest);
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