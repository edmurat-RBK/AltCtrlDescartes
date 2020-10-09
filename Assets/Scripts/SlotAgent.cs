using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotAgent
{
    public SlotState state = SlotState.EMPTY;
    public bool locked = false;

    public void Update(string uid)
    {
        if (locked)
        {
            Debug.LogWarning("This slot is locked");
            return;
        }

        NFCManager.EncyclopediaEntry ee;
        if(NFCManager.Instance.encyclopedia.TryGetValue(uid, out ee))
        {
            switch(ee.card)
            {
                case CardSymbol.TRIANGLE:
                    state = SlotState.TRIANGLE;
                    break;

                case CardSymbol.SQUARE:
                    state = SlotState.SQUARE;
                    break;

                case CardSymbol.CIRCLE:
                    state = SlotState.CIRCLE;
                    break;
            }
        }
    }

    public void ResetToNextTurn()
    {
        if(state == SlotState.EMPTY && locked)
        {
            locked = false;
        }
        else if(state != SlotState.EMPTY && !locked)
        {
            state = SlotState.EMPTY;
            locked = true;
        }
    }
}
