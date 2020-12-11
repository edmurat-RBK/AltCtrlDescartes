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

    public Slot[] slots;
    public bool[] cardDown;

    [Space(10)]
    #region graphs
    public Sprite emptySlotSprite;
    public Sprite lockedSlotSprite;
    public Sprite pinkSlotSprite;
    public Sprite blueSlotSprite;
    public Sprite orangeSlotSprite;
    #endregion
    private void Awake()
    {
        slots = new Slot[6];
        for(int i = 0; i<6; i++)
        {
            slots[i] = new Slot();
        }

        cardDown = new bool[2] { false, false };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SendFakeInput(SlotName.ALPHA, PlayerName.CASTOR, CardSymbol.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            SendFakeInput(SlotName.ALPHA, PlayerName.CASTOR, CardSymbol.ORANGE);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SendFakeInput(SlotName.ALPHA, PlayerName.CASTOR, CardSymbol.PINK);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            SendFakeInput(SlotName.BETA, PlayerName.CASTOR, CardSymbol.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SendFakeInput(SlotName.BETA, PlayerName.CASTOR, CardSymbol.ORANGE);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            SendFakeInput(SlotName.BETA, PlayerName.CASTOR, CardSymbol.PINK);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SendFakeInput(SlotName.GAMMA, PlayerName.CASTOR, CardSymbol.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SendFakeInput(SlotName.GAMMA, PlayerName.CASTOR, CardSymbol.ORANGE);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SendFakeInput(SlotName.GAMMA, PlayerName.CASTOR, CardSymbol.PINK);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            SendFakeInput(SlotName.DELTA, PlayerName.POLLUX, CardSymbol.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SendFakeInput(SlotName.DELTA, PlayerName.POLLUX, CardSymbol.ORANGE);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SendFakeInput(SlotName.DELTA, PlayerName.POLLUX, CardSymbol.PINK);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            SendFakeInput(SlotName.EPSILON, PlayerName.POLLUX, CardSymbol.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            SendFakeInput(SlotName.EPSILON, PlayerName.POLLUX, CardSymbol.ORANGE);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SendFakeInput(SlotName.EPSILON, PlayerName.POLLUX, CardSymbol.PINK);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            SendFakeInput(SlotName.DZETA, PlayerName.POLLUX, CardSymbol.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            SendFakeInput(SlotName.DZETA, PlayerName.POLLUX, CardSymbol.ORANGE);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SendFakeInput(SlotName.DZETA, PlayerName.POLLUX, CardSymbol.PINK);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            slots[(int)SlotName.ALPHA].state = SlotState.EMPTY;
            slots[(int)SlotName.BETA].state = SlotState.EMPTY;
            slots[(int)SlotName.GAMMA].state = SlotState.EMPTY;
            cardDown[(int)PlayerName.CASTOR] = false;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            slots[(int)SlotName.DELTA].state = SlotState.EMPTY;
            slots[(int)SlotName.EPSILON].state = SlotState.EMPTY;
            slots[(int)SlotName.DZETA].state = SlotState.EMPTY;
            cardDown[(int)PlayerName.POLLUX] = false;
        }
    }

    public void GetData(string data, UduinoDevice board)
    {
        string[] dataSplit = data.Split(':');
        string reader = dataSplit[0]; 
        string uid = dataSplit[1];

        //Debug.Log("Data received from Arduino\n"+data);

        if(GameManager.Instance.state == GameState.WAIT_BOTH || GameManager.Instance.state == GameState.WAIT_CASTOR || GameManager.Instance.state == GameState.WAIT_POLLUX)
        {
            if (!cardDown[(int)PlayerName.CASTOR])
            {
                if (reader.Equals("ALPHA") && !slots[(int)SlotName.ALPHA].locked)
                {
                    slots[(int)SlotName.ALPHA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
                else if (reader.Equals("BETA") && !slots[(int)SlotName.BETA].locked)
                {
                    slots[(int)SlotName.BETA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
                else if (reader.Equals("GAMMA") && !slots[(int)SlotName.GAMMA].locked)
                {
                    slots[(int)SlotName.GAMMA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
            }
            
            if (!cardDown[(int)PlayerName.POLLUX])
            {
                if (reader.Equals("DELTA") && !slots[(int)SlotName.DELTA].locked)
                {
                    slots[(int)SlotName.DELTA].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
                else if (reader.Equals("EPSILON") && !slots[(int)SlotName.EPSILON].locked)
                {
                    slots[(int)SlotName.EPSILON].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
                else if (reader.Equals("DZETA") && !slots[(int)SlotName.DZETA].locked)
                {
                    slots[(int)SlotName.DZETA].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
            }
        }
    }

    public bool CheckFullSuccess(out CardSymbol symbol, out int slotIndex)
    {
        for(var i=0; i<3; i++)
        {
            if(slots[i].state != SlotState.EMPTY && slots[i].state == slots[i+3].state)
            {
                switch(slots[i].state)
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
                slotIndex = i;
                return true;
            }
        }
        symbol = CardSymbol.PINK;
        slotIndex = -1;
        return false;
    }

    public bool CheckSymbolSuccess(out CardSymbol symbol)
    {
        for (var i = 0; i < 3; i++)
        {
            if (slots[i].state != SlotState.EMPTY)
            {
                for(int j = 3; j < 6; j++)
                {
                    if(slots[i].state == slots[j].state)
                    {
                        switch (slots[i].state)
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
            }
        }
        symbol = CardSymbol.PINK;
        return false;
    }

    public bool CheckSlotSuccess(out int slotIndex, out CardSymbol symbolCastor, out CardSymbol symbolPollux)
    {
        for (var i = 0; i < 3; i++)
        {
            if (slots[i].state != SlotState.EMPTY && slots[i+3].state != SlotState.EMPTY)
            {
                switch (slots[i].state)
                {
                    case SlotState.PINK:
                        symbolCastor = CardSymbol.PINK;
                        break;

                    case SlotState.BLUE:
                        symbolCastor = CardSymbol.BLUE;
                        break;

                    case SlotState.ORANGE:
                        symbolCastor = CardSymbol.ORANGE;
                        break;

                    default:
                        throw new System.Exception();
                }
                switch (slots[i+3].state)
                {
                    case SlotState.PINK:
                        symbolPollux = CardSymbol.PINK;
                        break;

                    case SlotState.BLUE:
                        symbolPollux = CardSymbol.BLUE;
                        break;

                    case SlotState.ORANGE:
                        symbolPollux = CardSymbol.ORANGE;
                        break;

                    default:
                        throw new System.Exception();
                }
                slotIndex = i;
                return true;

            }
        }
        symbolPollux = CardSymbol.PINK;
        symbolCastor = CardSymbol.PINK;
        slotIndex = -1;
        return false;
    }

    public void ReturnAll(out int slotCastor, out int slotPollux, out CardSymbol symbolCastor, out CardSymbol symbolPollux)
    {
        slotCastor = -1;
        slotPollux = -1;
        symbolCastor = CardSymbol.PINK;
        symbolPollux = CardSymbol.PINK;

        for (var i = 0; i < 3; i++)
        {
            if (slots[i].state != SlotState.EMPTY)
            {
                slotCastor = i;
                switch (slots[i].state)
                {
                    case SlotState.PINK:
                        symbolCastor = CardSymbol.PINK;
                        break;

                    case SlotState.BLUE:
                        symbolCastor = CardSymbol.BLUE;
                        break;

                    case SlotState.ORANGE:
                        symbolCastor = CardSymbol.ORANGE;
                        break;

                    default:
                        throw new System.Exception();
                }
            }
        }

        for (var i = 3; i < 6; i++)
        {
            if (slots[i].state != SlotState.EMPTY)
            {
                slotPollux = i;
                Debug.Log(i);
                switch (slots[i].state)
                {
                    case SlotState.PINK:
                        symbolPollux = CardSymbol.PINK;
                        break;

                    case SlotState.BLUE:
                        symbolPollux = CardSymbol.BLUE;
                        break;

                    case SlotState.ORANGE:
                        symbolPollux = CardSymbol.ORANGE;
                        break;

                    default:
                        throw new System.Exception();
                }
            }
        }

        if(slotCastor == -1 || slotPollux == -1)
        {
            throw new System.Exception();
        }
    }

    public void ReturnPollux(out int slotPollux, out CardSymbol symbolPollux)
    {
        slotPollux = -1;
        symbolPollux = CardSymbol.PINK;

        for (var i = 3; i < 6; i++)
        {
            if (slots[i].state != SlotState.EMPTY)
            {
                slotPollux = i;
                Debug.Log(i);
                switch (slots[i].state)
                {
                    case SlotState.PINK:
                        symbolPollux = CardSymbol.PINK;
                        break;

                    case SlotState.BLUE:
                        symbolPollux = CardSymbol.BLUE;
                        break;

                    case SlotState.ORANGE:
                        symbolPollux = CardSymbol.ORANGE;
                        break;

                    default:
                        throw new System.Exception();
                }
            }
        }

        if (slotPollux == -1)
        {
            throw new System.Exception();
        }
    }

    public void ReturnCastor(out int slotCastor, out CardSymbol symbolCastor)
    {
        slotCastor = -1;
        symbolCastor = CardSymbol.PINK;

        for (var i = 0; i < 3; i++)
        {
            if (slots[i].state != SlotState.EMPTY)
            {
                slotCastor = i;
                switch (slots[i].state)
                {
                    case SlotState.PINK:
                        symbolCastor = CardSymbol.PINK;
                        break;

                    case SlotState.BLUE:
                        symbolCastor = CardSymbol.BLUE;
                        break;

                    case SlotState.ORANGE:
                        symbolCastor = CardSymbol.ORANGE;
                        break;

                    default:
                        throw new System.Exception();
                }
            }
        }

        if (slotCastor == -1)
        {
            throw new System.Exception();
        }
    }

    public void SendFakeInput(SlotName slot, PlayerName player, CardSymbol symbol)
    {
        string send = "";
        switch (slot)
        {
            case SlotName.ALPHA:
                send += "ALPHA";
                break;

            case SlotName.BETA:
                send += "BETA";
                break;

            case SlotName.GAMMA:
                send += "GAMMA";
                break;

            case SlotName.DELTA:
                send += "DELTA";
                break;

            case SlotName.EPSILON:
                send += "EPSILON";
                break;

            case SlotName.DZETA:
                send += "DZETA";
                break;
        }

        send += ":";

        if(player == PlayerName.CASTOR)
        {
            switch (symbol)
            {
                case CardSymbol.PINK:
                    send += "B045D232";
                    break;

                case CardSymbol.BLUE:
                    send += "E42A772A";
                    break;

                case CardSymbol.ORANGE:
                    send += "020BF434";
                    break;
            }
        }
        else if(player == PlayerName.POLLUX)
        {
            switch (symbol)
            {
                case CardSymbol.PINK:
                    send += "14F4CD2B";
                    break;

                case CardSymbol.BLUE:
                    send += "F2EDD433";
                    break;

                case CardSymbol.ORANGE:
                    send += "D0DCFA32";
                    break;
            }
        }

        GetData(send, null);
    }
}
