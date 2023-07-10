using System;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.ObjectPooling;

namespace ChestSystem.Chest
{
    public class ChestManager : GenericSingleton<ChestManager>
    {
        private const int MAXSLOT = 4;
        private System.Random rnd = new System.Random();
        [SerializeField] private Availablity[] chestSlot = new Availablity[MAXSLOT];

        public static event Action<string> PopUp;
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                GameObject availableSlot = GetAvailableSlot();
                if (availableSlot)
                {
                    GameObject chest = ObjectPooler.Instance.GetFromPool((Enums.PoolTag)rnd.Next(1,4), availableSlot.transform.position, availableSlot.transform.rotation);
                }
                else
                {
                    PopUp("Slot Full");
                }
            }
        }
        private GameObject GetAvailableSlot()
        {
            for(int i = 0; i < MAXSLOT; i++)
            {
                if(chestSlot[i] == null)
                {
                    return null;
                }
                if (chestSlot[i].available)
                {
                    chestSlot[i].available = false;
                    return chestSlot[i].gameObject;
                }
            }
            return null;
        }
    }
}