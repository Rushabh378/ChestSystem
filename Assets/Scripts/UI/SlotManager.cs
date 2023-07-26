using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public class SlotManager : GenericSingleton<SlotManager>
    {
        [SerializeField] private List<ChestSlot> chestSlots;

        public ChestSlot AvailableSlot()
        {
            foreach(ChestSlot chestSlot in chestSlots)
            {
                if (chestSlot.IsAvailable)
                {
                    return chestSlot;
                }
            }
            return null;
        }
    }
}