using System;
using System.Collections;
using UnityEngine;

namespace ChestSystem.CurrancySpace
{
    public class Gems : MonoBehaviour, ICurrancy
    {
        public int Amount = 50;

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