using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    #region Singleton
    public static ObjectiveManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            ObjectiveManager[] managers = FindObjectsOfType(typeof(ObjectiveManager)) as ObjectiveManager[];
            if (managers.Length == 0)
            {
                Debug.LogWarning("ObjectiveManager not present on the scene. Creating a new one.");
                ObjectiveManager manager = new GameObject("Game Manager").AddComponent<ObjectiveManager>();
                _instance = manager;
                return _instance;
            }
            else
            {
                return managers[0];
            }
        }
        set
        {
            if (_instance == null)
                _instance = value;
            else
            {
                Debug.LogError("You can only use one ObjectiveManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    private static ObjectiveManager _instance = null;
    #endregion

    public ObjectiveSet[] objectiveSetList;

    public ObjectiveSet pickRandom()
    {
        return objectiveSetList[Random.Range(0,objectiveSetList.Length)];
    }
}
