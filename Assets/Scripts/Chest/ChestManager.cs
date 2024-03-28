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
        private System.Random _random = new System.Random();

        [SerializeField] private List<ChestType> _chestTypes;

        private ChestSlot _chestSlot;
        private List<ChestModel> _models = new();
        private List<ChestController> _controllers = new();

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                _chestSlot = SlotManager.Instance.AvailableSlot();

                if (_controllers.Count <= MAXSLOT && _chestSlot != null)
                {
                    int index = _random.Next(_chestTypes.Count);
                    int id = _random.Next(1000, 10000);

                    ChestModel model = new ChestModel(_chestTypes[index], id, _chestSlot);
                    ChestController controller = new ChestController(model);

                    _chestSlot.Activate(controller);

                    _models.Add(model);
                    _controllers.Add(controller);

                    Debug.Log(_chestTypes[index].name + " is created with ID = " + id);
                }
                else
                {
                    UIManager.Instance.ShowPopup("Slot Full", "Slot is full");
                }
            }
        }

        public void RemoveChest(ChestModel model)
        {
            model.ChestSlot.Deactivate(model.Chest);
            SlotManager.Instance.StartNextChestInQueue();

            int index = _models.FindIndex(M => M.ChestId == model.ChestId);
            _models.RemoveAt(index);
            _controllers.RemoveAt(index);
        }
    }
}