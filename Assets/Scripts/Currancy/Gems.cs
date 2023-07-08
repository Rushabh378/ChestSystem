using System;
using System.Collections;
using UnityEngine;

namespace ChestSystem.Currancy
{
    [Serializable]public class Gems : ICurrancy
    {
        public int Amount;

        public void add(int value)
        {
            Amount += value;
        }

        public int get()
        {
            return Amount;
        }
    }
}