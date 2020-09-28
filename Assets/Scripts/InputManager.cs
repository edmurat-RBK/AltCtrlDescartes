using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public enum PlayerNum
    {
        PLAYER_1 = 1,
        PLAYER_2 = 2
    }

    public enum SlotNum
    {
        LEFT = 1,
        MIDDLE = 2,
        RIGHT = 3
    }

    public enum CardNum
    {
        TRIANGLE = 1,
        SQUARE = 2,
        CIRCLE = 3
    }

    public class InputManager : MonoBehaviour
    {
        public bool playerIsSelect = false;
        public bool slotIsSelect = false;
        public bool cardIsSelect = false;

        public PlayerNum playerSelect;
        public SlotNum slotSelect;
        public CardNum cardSelect;

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {

            }
            else if (Input.GetKey(KeyCode.E))
            {

            }
        }
    }
}
