using TMPro;
using UnityEngine;
using ChestSystem.CurrancySpace;
using static ChestSystem.Enums;

namespace ChestSystem.UI
{
    public class UpdateCurrancy : MonoBehaviour
    {
        [SerializeField] private ECurrancy currancyType;
        private TextMeshProUGUI currancy;

        public void Start()
        {
            currancy = GetComponentInChildren<TextMeshProUGUI>();
            Refresh();
        }
        public void Refresh()
        {
            switch (currancyType)
            {
                case ECurrancy.coin:
                    currancy.text = Currancy.Instance.coins.get();
                    break;
                case ECurrancy.gem:
                    currancy.text = Currancy.Instance.gems.get();
                    break;
                default:
                    Debug.Log("couldn't found currancy type");
                    break;
            }
        }
    }
}