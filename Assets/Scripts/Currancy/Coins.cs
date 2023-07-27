﻿using System;
using UnityEngine;

namespace ChestSystem.CurrancySpace
{
    [Serializable]
    public class Coins : ICurrancy
    {
        [SerializeField] private int amount = 100;

        public int Amount => amount;

        public void Add(int value)
        {
            amount += value;
        }

        public void Subtract(int value)
        {
            amount -= value;
        }
    }
}