using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective Set", menuName = "Scriptable Objects/ObjectiveSet", order = 1)]
public class ObjectiveSet : ScriptableObject
{
    public enum Difficulty
    {
        EASY,
        NORMAL,
        HARD
    }
    public Difficulty difficulty;

    public CardSymbol[] set;
}
