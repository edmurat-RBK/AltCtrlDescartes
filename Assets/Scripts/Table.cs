using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Table
    {
        public Slot[] slots;

        public Table()
        {
            slots = new Slot[3];
        }
    }
}