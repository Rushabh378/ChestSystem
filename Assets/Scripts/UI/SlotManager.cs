using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Chest;

namespace ChestSystem.UI
{
    public class SlotManager : GenericSingleton<SlotManager>
    {
        [SerializeField] [Range(1, 4)] private int _maxQueue = 2;
        [SerializeField] private List<ChestSlot> _chestSlots;

        private ChestView _runningChest = null;
        private int _queueChests = 0;

        public bool Enqueue()
        {
            if(_queueChests < _maxQueue)
            {
                _queueChests++;
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
            if(_queueChests > 0)
            {
                _queueChests--;
                return true;
            }
            else
            {
                Debug.LogWarning("you are trying to remove element from empty queue.");
                return false;
            }
        }

        public ChestSlot AvailableSlot()
        {
            foreach(ChestSlot chestSlot in _chestSlots)
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
            _runningChest = null;
        }

        public void StartTimer(ChestView view)
        {
            if(_runningChest == null)
            {
                view.timerOn = true;
                _runningChest = view;
            }
        }

        public bool IsChestRunning() => (_runningChest != null);

        public bool IsChestRunning(ChestView chest)
        {
            if(_runningChest != null)
            {
                return (_runningChest == chest);
            }
            return false;
        }

        public void StartNextChestInQueue()
        {
            if (_runningChest == null)
            {
                foreach (ChestSlot chestSlot in _chestSlots)
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