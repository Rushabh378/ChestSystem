using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ChestSystem.UI;

namespace ChestSystem.Chest
{
    public class ChestModel
    {
        private ChestController controller;
        public ChestType chestType;
        private TextMeshProUGUI standText;
        private GameObject chestStand;

        public float Timer;
        public int ChestId;
        public Vector3 ChestPosition;

        public ChestModel(ChestType chestType, int chestId, Vector3 position)
        {
            this.chestType = chestType;
            this.ChestId = chestId;
            this.ChestPosition = position;
            Timer = chestType.timerSeconds;
        }
        public string StandText { get => standText.text; set => standText.text = value; }

        public int GetCoins()
        {
            return Random.Range(chestType.coinsRange, chestType.to + 1);
        }
        public int GetGems()
        {
            return Random.Range(chestType.gemsRange, chestType._to + 1);
        }

        public void SetController(ChestController controller)
        {
            this.controller = controller;
        }
    }
}