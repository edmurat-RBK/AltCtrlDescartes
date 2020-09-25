using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {

        private Player[] playerList;
        public SyncScore syncScore;

        void Start()
        {
            playerList = new Player[2];
            playerList[(int)Side.LEFT] = new Player(Side.LEFT);
            playerList[(int)Side.RIGHT] = new Player(Side.RIGHT);

            syncScore = new SyncScore();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log(
                    "Player 1\n" +
                    "Table : \n" +
                    "   Slot 1 : " + playerList[(int)Side.LEFT].table.slots[0] + "\n" +
                    "   Slot 2 : " + playerList[(int)Side.LEFT].table.slots[1] + "\n" +
                    "   Slot 3 : " + playerList[(int)Side.LEFT].table.slots[2] + "\n" +
                    "Objectives : \n" +
                    "   Objective 1 : " + playerList[(int)Side.LEFT].objectiveChain.objectiveList[0].symbol + "\n" +
                    "   Objective 2 : " + playerList[(int)Side.LEFT].objectiveChain.objectiveList[1].symbol + "\n"
                    );
            }
        }
    }
}
