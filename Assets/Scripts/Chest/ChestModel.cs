using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ChestSystem.Chest
{
    public class ChestModel
    {
        private ChestController controller;
        public ChestType chestType;
        private GameObject chestSlot;
        private TextMeshProUGUI standText;

        public Animator StandAnimtor;
        public Button btnOpen;
        public float Timer;

        public ChestModel(ChestType chestType, Availablity chestSlot, GameObject chestStand)
        {
            this.chestType = chestType;
            this.chestSlot = chestSlot.gameObject;

            StandAnimtor = chestStand.GetComponent<Animator>();
            standText = chestStand.GetComponentInChildren<TextMeshProUGUI>();
            btnOpen = chestStand.GetComponent<Button>();
            Timer = chestType.timerSeconds;
        }

        public Vector3 ChestPosition => chestSlot.transform.position;
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