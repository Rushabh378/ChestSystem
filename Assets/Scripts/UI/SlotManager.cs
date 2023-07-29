using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Chest;

namespace ChestSystem.UI
{
    public class SlotManager : GenericSingleton<SlotManager>
    {
        [SerializeField] private List<ChestSlot> chestSlots;

        private ChestView runningChest = null;

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
            foreach(ChestSlot chestSlot in chestSlots)
            {
                if (chestSlot.IsInQueue())
                {
                    ChestController chest = chestSlot.GetController;
                    chest.StartTimer();
                    return;
                }
                else
                {
                    runningChest = null;
                }
            }
        }
    }
}