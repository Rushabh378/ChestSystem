using System.Collections;
using UnityEngine;
using ChestSystem.Chest;

namespace ChestSystem.UI
{
    public class ChestSlot : MonoBehaviour
    {
        private ChestController controller;

        public bool IsAvailable;
        public Transform ChestPosition;

        public ChestController Chest { get => controller; set => controller = value; }

    }
}