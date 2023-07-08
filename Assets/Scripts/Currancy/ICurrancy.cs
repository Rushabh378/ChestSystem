using UnityEngine;

namespace ChestSystem.Currancy
{
    public interface ICurrancy
    {
        public void add(int value);
        public string get();
    }
}