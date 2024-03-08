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

        public bool Enqueue()
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
        public bool Dequeue()
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

        public void RemoveRunningChest()
        {
            runningChest = null;
        }

        public void StartTimer(ChestView view)
        {
            if(runningChest == null)
            {
                view.timerOn = true;
                runningChest = view;
            }
        }

        public bool IsChestRunning() => (runningChest != null);

        public bool IsChestRunning(ChestView chest)
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
                        Dequeue();
                        chest.StartTimer();
                        return;
                    }
                }
            }
        }
    }
}