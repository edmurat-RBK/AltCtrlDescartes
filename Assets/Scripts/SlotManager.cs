using UnityEngine;
using System.Collections;

public class SlotManager : MonoBehaviour
{

    public CardNum card;
    public SlotNum position;
    public PlayerNum playerSide;

    public Sprite triangleCard;
    public Sprite squareCard;
    public Sprite circleCard;
    public Sprite emptyCard;
    public Sprite deadSlot;

    private InputManager input;
    private bool isDead = false;

    void Start()
    {
        input = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update()
    {
        if(input.playerIsSelect && input.slotIsSelect && input.cardIsSelect)
        {
            if(input.playerSelect == playerSide && input.slotSelect == position)
            {
                if(input.cardSelect == CardNum.TRIANGLE)
                {
                    // Set slot sprite with triangle card
                }
                else if (input.cardSelect == CardNum.SQUARE)
                {
                    // Set slot sprite with square card
                }
                else if (input.cardSelect == CardNum.CIRCLE)
                {
                    // Set slot sprite with circle card
                }
            }
        }
        else
        {
            //Reset slot sprite with empty card
        }
    }
}
