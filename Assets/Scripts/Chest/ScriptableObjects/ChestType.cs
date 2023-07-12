using UnityEngine;

namespace ChestSystem.Chest
{
    [CreateAssetMenu(fileName = "ChestType", menuName = "ChestType")]
    public class ChestType : ScriptableObject
    {
        public ChestView chestPrefeb;
        public int coinsRange = 100;
        public int to = 200;
        public int gemsRange = 10;
        public int _to = 20;
        public float timer = 15f;
    }
}