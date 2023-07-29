using UnityEngine;
using TMPro;

namespace ChestSystem.CurrancySpace
{
    public class Currancy : GenericSingleton<Currancy>
    {
        public Coins coins;
        public Gems gems;

        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI gemsText;

        public void Start()
        {
            UpdateCurrancy();
        }

        public void UpdateCurrancy()
        {
            coinText.text = coins.Amount.ToString();
            gemsText.text = gems.Amount.ToString();
        }
    }
}