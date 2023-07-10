using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.Chest;

namespace ChestSystem
{
    public class UIManager : GenericSingleton<UIManager>
    {
        [SerializeField] private List<GameObject> panels;

        public void Start()
        {
            ChestManager.PopUp += PopUpWindow;
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
    }
}