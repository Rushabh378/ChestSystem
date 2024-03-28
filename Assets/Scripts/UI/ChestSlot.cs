using UnityEngine;
using UnityEngine.UI;
using ChestSystem.Chest;
using TMPro;

namespace ChestSystem.UI
{
    public class ChestSlot : MonoBehaviour
    {

        private ChestController _controller;
        private Animator _animator;
        private Button _open;

        public Enums.States State;
        public Transform ChestPosition;
        public TextMeshProUGUI SlotText;

        public void Start()
        {
            _animator = GetComponent<Animator>();
            _open = GetComponent<Button>();

            State = Enums.States.Empty;
        }

        public bool IsAvailable() => (State == Enums.States.Empty);
        public bool IsInQueue() => (State == Enums.States.Enqueued);

        public ChestController GetController => _controller;

        public void Activate(ChestController chest)
        {
            _controller = chest;
            SlotText.text = "Open";
            State = Enums.States.Equiped;
            _animator.SetBool("Activate", true);
            _open.onClick.AddListener(_controller.OpeningOption);
        }

        public void Deactivate(ChestView chest)
        {
            State = Enums.States.Empty;
            _animator.SetBool("Activate", false);
            _open.onClick.RemoveListener(_controller.OpeningOption);
            if (SlotManager.Instance.IsChestRunning(chest))
            {
                SlotManager.Instance.RemoveRunningChest();
            }
            _controller = null;
            
        }

        public void ToggleQueue()
        {
            if(State == Enums.States.Enqueued)
            {
                if (SlotManager.Instance.Dequeue())
                {
                    State = Enums.States.Equiped;
                    SlotText.text = "Open";
                }
            }
            else
            {
                if (SlotManager.Instance.Enqueue())
                {
                    State = Enums.States.Enqueued;
                    SlotText.text = "Enqueue";
                }
            }
        }
    }
}