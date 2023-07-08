using System;
using System.Collections;
using UnityEngine;

namespace ChestSystem.Currancy
{
    [Serializable]public class Coins : ICurrancy
    {
        public int Amount;

        public void add(int value)
        {
            Amount += value;
        }

        public string get()
        {
            return Amount.ToString();
        }
    }
}