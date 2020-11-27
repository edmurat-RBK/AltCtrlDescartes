using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
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
                case CardSymbol.PINK:
                    state = SlotState.PINK;
                    break;

                case CardSymbol.BLUE:
                    state = SlotState.BLUE;
                    break;

                case CardSymbol.ORANGE:
                    state = SlotState.ORANGE;
                    break;
            }
        }
    }

    public void ResetToNextTurn(bool isStartRound)
    {
        if(state == SlotState.EMPTY && locked)
        {
            locked = false;
        }
        else if(state != SlotState.EMPTY && !locked)
        {
            state = SlotState.EMPTY;
            if(!isStartRound)
                locked = true;
        }
    }
}
