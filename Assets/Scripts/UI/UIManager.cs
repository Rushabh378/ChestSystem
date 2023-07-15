using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.Chest;

namespace ChestSystem.UI
{
    public class UIManager : GenericSingleton<UIManager>
    {
        [SerializeField] private List<GameObject> panels;
        public GameObject[] chestStand = new GameObject[4];

        public void Start()
        {
            ChestManager.PopUp += PopUpWindow;
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

        public void GiveRewords(int coins, int Gems)
        {

        }
    }
}