using System;
using UnityEngine;

namespace ChestSystem.CurrancySpace
{
    [Serializable]
    public class Gems : ICurrancy
    {
        [SerializeField] private int _amount = 50;

        public int Amount => _amount;

        public void Add(int value)
        {
            _amount += value;
        }

        public void Subtract(int value)
        {
            _amount -= value;
        }
    }
}