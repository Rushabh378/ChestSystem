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
            public GameObject Panel;
            public TextMeshProUGUI Header;
            public TextMeshProUGUI Discription;
            public Button ButtonOkay;
            public Button ButtonCancel;
        }

        [SerializeField] private Popup _window;
        [SerializeField] private List<GameObject> _panels;

        public void Start()
        {
            ChestController.GetRewords += GiveRewords;
        }

        public void ShowPopup(string header, string Message, UnityAction okay = null, UnityAction cancel = null, string okayText = "Okay", string cancelText = "Cancel")
        {
            _window.Header.text = header;
            _window.Discription.text = Message;

            RemoveListnersOf(_window.ButtonOkay);
            RemoveListnersOf(_window.ButtonCancel);
            _window.ButtonCancel.gameObject.SetActive(false);

            if (okay != null)
            {
                _window.ButtonOkay.onClick.AddListener(okay);
            }

            if (cancel != null)
            {
                _window.ButtonCancel.gameObject.SetActive(true);
                _window.ButtonCancel.onClick.AddListener(cancel);
            }
            
            TextMeshProUGUI Okay = _window.ButtonOkay.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI Cancel = _window.ButtonCancel.GetComponentInChildren<TextMeshProUGUI>();

            Okay.text = okayText;
            Cancel.text = cancelText;
            
            _window.Panel.SetActive(true);
        }

        private void RemoveListnersOf(Button button)
        {
            button.onClick.RemoveAllListeners();
        }
        public GameObject GetWindow(string window)
        {
            foreach(GameObject panel in _panels)
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
            Currancy.Instance.Coins.Add(coins);
            Currancy.Instance.Gems.Add(Gems);

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