using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Slot
    {
        bool empty;
        Card card;

        public Slot()
        {
            empty = true;
            card = null;
        }
    }
}