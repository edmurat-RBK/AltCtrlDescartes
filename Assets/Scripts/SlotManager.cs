using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    #region Singleton
    public static SlotManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            SlotManager[] managers = FindObjectsOfType(typeof(SlotManager)) as SlotManager[];
            if (managers.Length == 0)
            {
                Debug.LogWarning("SlotManager not present on the scene. Creating a new one.");
                SlotManager manager = new GameObject("Slot Manager").AddComponent<SlotManager>();
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
                Debug.LogError("You can only use one SlotManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    private static SlotManager _instance = null;
    #endregion

    public SlotAgent[] slotAgents;
    public bool[] cardDown;

    private void Awake()
    {
        slotAgents = new SlotAgent[6];
        for(int i = 0; i<6; i++)
        {
            slotAgents[i] = new SlotAgent();
        }

        cardDown = new bool[2] { false, false };
    }

    public void GetData(string data, UduinoDevice board)
    {
        string[] dataSplit = data.Split(':');
        string device = dataSplit[0];
        string reader = dataSplit[1]; 
        string uid = dataSplit[2];

        //Debug.Log("Data received from Arduino\n"+data);

        if(GameManager.Instance.state == GameState.WAIT_BOTH || GameManager.Instance.state == GameState.WAIT_CASTOR || GameManager.Instance.state == GameState.WAIT_POLLUX)
        {
            if (device.Equals("CASTOR") && !cardDown[(int)PlayerName.CASTOR])
            {
                if (reader.Equals("ALPHA") && !slotAgents[(int)SlotName.ALPHA].locked)
                {
                    slotAgents[(int)SlotName.ALPHA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
                else if (reader.Equals("BETA") && !slotAgents[(int)SlotName.BETA].locked)
                {
                    slotAgents[(int)SlotName.BETA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
                else if (reader.Equals("GAMMA") && !slotAgents[(int)SlotName.GAMMA].locked)
                {
                    slotAgents[(int)SlotName.GAMMA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
            }
            else if (device.Equals("POLLUX") && !cardDown[(int)PlayerName.POLLUX])
            {
                if (reader.Equals("ALPHA") && !slotAgents[(int)SlotName.DELTA].locked)
                {
                    slotAgents[(int)SlotName.DELTA].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
                else if (reader.Equals("BETA") && !slotAgents[(int)SlotName.EPSILON].locked)
                {
                    slotAgents[(int)SlotName.EPSILON].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
                else if (reader.Equals("GAMMA") && !slotAgents[(int)SlotName.DZETA].locked)
                {
                    slotAgents[(int)SlotName.DZETA].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
            }
        }
    }

    public bool CheckSuccess(out CardSymbol symbol)
    {
        for(var i=0; i<3; i++)
        {
            if(slotAgents[i].state != SlotState.EMPTY && slotAgents[i].state == slotAgents[i+3].state)
            {
                switch(slotAgents[i].state)
                {
                    case SlotState.PINK:
                        symbol = CardSymbol.PINK;
                        break;

                    case SlotState.BLUE:
                        symbol = CardSymbol.BLUE;
                        break;

                    case SlotState.ORANGE:
                        symbol = CardSymbol.ORANGE;
                        break;

                    default:
                        throw new System.Exception();
                }
                return true;
            }
        }
        symbol = CardSymbol.PINK;
        return false;
    }
}
