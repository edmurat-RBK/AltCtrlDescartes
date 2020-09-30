using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveChain
{
    public List<Objective> objectiveList;

    public ObjectiveChain(int objectiveCount)
    {
        objectiveList = new List<Objective>();
        for(int i=0; i<objectiveCount; i++)
        {
            objectiveList.Add(new Objective());
        }
    }
}