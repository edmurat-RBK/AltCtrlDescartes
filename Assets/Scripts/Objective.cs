using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Objective
    {
        public Symbol symbol;
        public bool completed = false;

        public Objective()
        {
            symbol = (Symbol)Random.Range(0, 2);
        }
    }
}