using UnityEngine;

namespace ChestSystem.CurrancySpace
{
    public interface ICurrancy
    {
        public void add(int value);
        public void minus(int value);
        public string get();
    }
}