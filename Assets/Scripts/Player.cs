using UnityEngine;
using System.Collections;

public class Player
{
    public Side side;
    public Table table;
    public ObjectiveChain objectiveChain;

    public Player(Side playerSide)
    {
        side = playerSide;
        table = new Table();
        objectiveChain = new ObjectiveChain(2);
    }
}