using System.Collections;
using UnityEngine;
using ChestSystem.UI;
using TMPro;

namespace ChestSystem.CurrancySpace
{
    public class Currancy : GenericSingleton<Currancy>
    {
        [HideInInspector]public Coins coins;
        [HideInInspector]public Gems gems;

        private TextMeshProUGUI[] updaters;

        public void Start()
        {
            coins = GetComponentInChildren<Coins>();
            gems = GetComponentInChildren<Gems>();
            updaters = GetComponentsInChildren<TextMeshProUGUI>();
            UpdateCurrancy();
        }

        public void UpdateCurrancy()
        {
            updaters[0].text = coins.get();
            updaters[1].text = gems.get();
        }
    }
}