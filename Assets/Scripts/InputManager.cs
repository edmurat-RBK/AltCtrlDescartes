using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Start InputManager");
    }

    void Update()
    {
        // DEBUG Player Selection
        if (Input.GetKey(KeyCode.A))
        {
            playerIsSelect = true;
            playerSelect = PlayerNum.PLAYER_1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            playerIsSelect = true;
            playerSelect = PlayerNum.PLAYER_2;
        }
        else
        {
            playerIsSelect = false;
        }

        // DEBUG Slot Selection
        if (Input.GetKey(KeyCode.Q))
        {
            slotIsSelect = true;
            slotSelect = SlotNum.LEFT;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            slotIsSelect = true;
            slotSelect = SlotNum.MIDDLE;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            slotIsSelect = true;
            slotSelect = SlotNum.RIGHT;
        }
        else
        {
            slotIsSelect = false;
        }

        // DEBUG Card Selection
        if (Input.GetKey(KeyCode.W))
        {
            cardIsSelect = true;
            cardSelect = CardNum.TRIANGLE;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            cardIsSelect = true;
            cardSelect = CardNum.SQUARE;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            cardIsSelect = true;
            cardSelect = CardNum.CIRCLE;
        }
        else
        {
            cardIsSelect = false;
        }
    }
}
