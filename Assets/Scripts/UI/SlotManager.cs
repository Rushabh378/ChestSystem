using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Chest;

namespace ChestSystem.UI
{
    public class SlotManager : GenericSingleton<SlotManager>
    {
        [SerializeField] [Range(1, 4)] private int maxQueue = 2;
        [SerializeField] private List<ChestSlot> chestSlots;

        private ChestView runningChest = null;
        private int queueChests = 0;

        public bool AddInQueue()
        {
            if(queueChests < maxQueue)
            {
                queueChests++;
                return true;
            }
            else
            {
                UIManager.Instance.ShowPopup("Queue Full", "Queue is full please wait for chest to open.");
                return false;
            }
        }
        public bool RemoveFromQueue()
        {
            if(queueChests > 0)
            {
                queueChests--;
                return true;
            }
            else
            {
                Debug.LogWarning("you are trying to remove element from epmty queue.");
                return false;
            }
        }

        public ChestSlot AvailableSlot()
        {
            foreach(ChestSlot chestSlot in chestSlots)
            {
                if (chestSlot.IsAvailable())
                {
                    return chestSlot;
                }
            }
            return null;
        }

        public void RemoveRuningChest()
        {
            runningChest = null;
        }

        public void StartTimer(ChestView view, ChestSlot slot)
        {
            if (IsRunningChest(view))
            {
                view.timerOn = true;
                return;
            }
            else if(runningChest == null)
            {
                view.timerOn = true;
                runningChest = view;
            }
            else
            {
                slot.ToggleQueue();
            }
        }

        public bool IsRunningChest(ChestView chest)
        {
            if(runningChest != null)
            {
                return (runningChest == chest);
            }
            return false;
        }

        public void StartNextChestInQueue()
        {
            if (runningChest == null)
            {
                foreach (ChestSlot chestSlot in chestSlots)
                {
                    if (chestSlot.IsInQueue())
                    {
                        ChestController chest = chestSlot.GetController;
                        RemoveFromQueue();
                        chest.StartTimer();
                        return;
                    }
                }
            }
        }
    }
}