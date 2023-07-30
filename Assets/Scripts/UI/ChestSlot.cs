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

        public Enums.States State;
        public Transform ChestPosition;
        public TextMeshProUGUI SlotText;

        public void Start()
        {
            animator = GetComponent<Animator>();
            open = GetComponent<Button>();

            State = Enums.States.empty;
        }

        public bool IsAvailable() => (State == Enums.States.empty);
        public bool IsInQueue() => (State == Enums.States.inQueue);

        public ChestController GetController => controller;

        public void Activate(ChestController chest)
        {
            controller = chest;
            SlotText.text = "Open";
            State = Enums.States.equiped;
            animator.SetBool("Activate", true);
            open.onClick.AddListener(controller.OpeningOption);
        }

        public void Deactivate()
        {
            State = Enums.States.empty;
            animator.SetBool("Activate", false);
            open.onClick.RemoveListener(controller.OpeningOption);
            controller = null;
            SlotManager.Instance.RemoveRuningChest();
        }

        public void ToggleQueue()
        {
            if(State == Enums.States.inQueue)
            {
                if (SlotManager.Instance.RemoveFromQueue())
                {
                    State = Enums.States.equiped;
                    SlotText.text = "Open";
                }
            }
            else
            {
                if (SlotManager.Instance.AddInQueue())
                {
                    State = Enums.States.inQueue;
                    SlotText.text = "InQueue";
                }
            }
        }
    }
}