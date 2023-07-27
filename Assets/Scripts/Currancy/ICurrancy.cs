using UnityEngine;

namespace ChestSystem.CurrancySpace
{
    public interface ICurrancy
    {
        public void Add(int value);
        public void Subtract(int value);
    }
}