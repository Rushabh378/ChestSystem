using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ChestSystem.Chest;
using ChestSystem.CurrancySpace;
using UnityEngine.Events;

namespace ChestSystem.UI
{
    public class UIManager : GenericSingleton<UIManager>
    {
        [Serializable]
        public struct Popup
        {
            public GameObject panel;
            public TextMeshProUGUI header;
            public TextMeshProUGUI discription;
            public Button buttonOkay;
            public Button buttonCancel;
        }

        [SerializeField] private Popup window;
        [SerializeField] private List<GameObject> panels;

        public void Start()
        {
            ChestController.GetRewords += GiveRewords;
        }

        public void ShowPopup(string header, string Message, UnityAction okay = null, UnityAction cancel = null, string okayText = "Okay", string cancelText = "Cancel")
        {
            window.header.text = header;
            window.discription.text = Message;

            RemoveListnersOf(window.buttonOkay);
            RemoveListnersOf(window.buttonCancel);

            if (okay != null)
            {
                window.buttonOkay.onClick.AddListener(okay);
            }

            if (cancel != null)
            {
                window.buttonCancel.gameObject.SetActive(true);
                window.buttonCancel.onClick.AddListener(cancel);
            }
            
            TextMeshProUGUI Okay = window.buttonOkay.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI Cancel = window.buttonCancel.GetComponentInChildren<TextMeshProUGUI>();

            Okay.text = okayText;
            Cancel.text = cancelText;
            
            window.panel.SetActive(true);
        }

        private void RemoveListnersOf(Button button)
        {
            button.onClick.RemoveAllListeners();
        }
        public GameObject GetWindow(string window)
        {
            foreach(GameObject panel in panels)
            {
                if(panel.name == window)
                {
                    return panel;
                }
            }
            return null;
        }

        public void GiveRewords(int coins, int Gems, UnityAction RemoveChest)
        {
            Currancy.Instance.coins.Add(coins);
            Currancy.Instance.gems.Add(Gems);

            GameObject RewordsWindow = GetWindow("Rewords");
            Button Okay = RewordsWindow.GetComponentInChildren<Button>();
            TextMeshProUGUI[] texts = RewordsWindow.GetComponentsInChildren<TextMeshProUGUI>(true);

            texts[1].text = coins.ToString();
            texts[2].text = Gems.ToString();

            RemoveListnersOf(Okay);
            Okay.onClick.AddListener(RemoveChest);

            RewordsWindow.SetActive(true);

            Currancy.Instance.UpdateCurrancy();
        }
    }
}