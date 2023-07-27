using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.Chest;
using TMPro;

namespace ChestSystem.UI
{
    public class ChestSlot : MonoBehaviour
    {

        private ChestController controller;
        private Animator animator;
        private Button open;

        public bool IsAvailable;
        public Transform ChestPosition;
        public TextMeshProUGUI slotText;

        public void Start()
        {
            animator = GetComponent<Animator>();
            open = GetComponent<Button>();
        }

        public void Activate(ChestController chest)
        {
            controller = chest;
            IsAvailable = false;
            slotText.text = "Open";
            animator.SetBool("Activate", true);
            open.onClick.AddListener(controller.OpeningOption);
        }

        public void Deactivate()
        {
            controller = null;
            IsAvailable = true;
            animator.SetBool("Activate", false);
        }
    }
}