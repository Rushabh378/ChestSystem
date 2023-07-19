using System;
using System.Collections;
using UnityEngine;

namespace ChestSystem.CurrancySpace
{
    [Serializable]
    public class Coins : MonoBehaviour, ICurrancy
    {
        public int Amount = 100;

        public void add(int value)
        {
            Amount += value;
        }

        public string get()
        {
            return Amount.ToString();
        }

        public void minus(int value)
        {
            Amount -= value;
        }
    }
}