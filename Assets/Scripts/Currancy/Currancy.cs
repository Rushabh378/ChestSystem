using UnityEngine;
using TMPro;

namespace ChestSystem.CurrancySpace
{
    public class Currancy : GenericSingleton<Currancy>
    {
        public Coins Coins;
        public Gems Gems;

        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private TextMeshProUGUI _gemsText;

        public void Start()
        {
            UpdateCurrancy();
        }

        public void UpdateCurrancy()
        {
            _coinText.text = Coins.Amount.ToString();
            _gemsText.text = Gems.Amount.ToString();
        }
    }
}