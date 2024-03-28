using UnityEngine;
using TMPro;
using ChestSystem.UI;

namespace ChestSystem.Chest
{
    public class ChestModel
    {
        private ChestController _controller;
        private ChestType _chestType;

        public ChestSlot ChestSlot;
        public float Timer;
        public int ChestId;

        public ChestModel(ChestType chestType, int chestId, ChestSlot chestSlot)
        {
            this._chestType = chestType;
            this.ChestId = chestId;
            this.ChestSlot = chestSlot;

            Timer = chestType.timerSeconds;
        }
        public Vector3 Position => ChestSlot.ChestPosition.position;
        public string ButtonText { get => ChestSlot.SlotText.text; set => ChestSlot.SlotText.text = value; }

        public ChestView Chest => _chestType.chestPrefeb;

        public int GetCoins()
        {
            return Random.Range(_chestType.coinsRange, _chestType.to + 1);
        }
        public int GetGems()
        {
            return Random.Range(_chestType.gemsRange, _chestType._to + 1);
        }

        public void SetController(ChestController controller)
        {
            this._controller = controller;
        }
    }
}