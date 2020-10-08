using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotAgent
{
    public SlotState state;
    public bool locked = false;

    public SlotAgent()
    {
        state = SlotState.EMPTY;
    }

    public void EmptySlot()
    {
        if(state != SlotState.EMPTY)
        {
            state = SlotState.EMPTY;
            Lock();
        }
        else
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        locked = false;
    }

    public void Lock()
    {
        locked = true;
    }
}
