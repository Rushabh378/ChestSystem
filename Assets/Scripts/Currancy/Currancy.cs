using System.Collections;
using UnityEngine;
using ChestSystem.UI;

namespace ChestSystem.Currancy
{
    public class Currancy : GenericSingleton<Currancy>
    {
        [SerializeReference]public ICurrancy coins = new Coins();
        [SerializeReference]public ICurrancy gems = new Gems();

        private UpdateCurrancy[] updaters;

        public void Start()
        {
            updaters = GetComponentsInChildren<UpdateCurrancy>();
        }

        public void UpdateCurrancy()
        {
            foreach(UpdateCurrancy updater in updaters)
            {
                updater.Refresh();
            }
        }
    }
}