using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ChestSystem.Chest;
using ChestSystem.CurrancySpace;

namespace ChestSystem.UI
{
    public class UIManager : GenericSingleton<UIManager>
    {
        [SerializeField] private List<GameObject> panels;
        public GameObject[] chestStand = new GameObject[4];

        public void Start()
        {
            ChestManager.PopUp += PopUpWindow;
            ChestController.PopUp += PopUpWindow;
            ChestController.GetRewords += GiveRewords;
        }

        private void PopUpWindow(string window)
        {
            foreach(GameObject panel in panels)
            {
                if(panel.name == window)
                {
                    panel.SetActive(true);
                }
            }
        }

        private GameObject GetWindow(string window)
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

        public void OptionToOpen(int cost, ChestController controller)
        {
            GameObject optionWindow = GetWindow("Open Options");
            TextMeshProUGUI textHolder = optionWindow.GetComponentInChildren<TextMeshProUGUI>();
            Button[] buttons = optionWindow.GetComponentsInChildren<Button>();

            textHolder.text = cost.ToString();
            buttons[0].onClick.AddListener(controller.OpenImmediatly);

            textHolder = buttons[1].GetComponent<TextMeshProUGUI>();

            if (ChestManager.Instance.ChestRunning && controller.chestProcess == Enums.Process.idle)
            {
                buttons[1].onClick.AddListener(controller.AddChestToQueue);
            }
            else if (controller.chestProcess == Enums.Process.queue)
            {
                //textHolder.text = "cancel Queue";
                buttons[1].onClick.AddListener(controller.CancelQueue);
            }
            else if (controller.chestProcess == Enums.Process.idle)
            {
                //textHolder.text = "Start Timer";
                buttons[1].onClick.AddListener(controller.StartOpeningChest);
            }
            
            

            optionWindow.SetActive(true);
        }

        public void GiveRewords(int coins, int Gems)
        {
            Currancy.Instance.coins.add(coins);
            Currancy.Instance.gems.add(Gems);

            GameObject RewordsWindow = GetWindow("Rewords");
            TextMeshProUGUI[] texts = RewordsWindow.GetComponentsInChildren<TextMeshProUGUI>(true);

            texts[1].text = coins.ToString();
            texts[2].text = Gems.ToString();

            RewordsWindow.SetActive(true);

            Currancy.Instance.UpdateCurrancy();
        }
    }
}