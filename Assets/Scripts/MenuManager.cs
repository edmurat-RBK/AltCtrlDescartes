using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public MenuState menustate;
    private bool coroutineFeedback;
    // Start is called before the first frame update
    void Start()
    {
        menustate = MenuState.WAIT_BOTH;
    }

    // Update is called once per frame
    void Update()
    {

        switch (menustate)
        {
            case MenuState.WAIT_BOTH:
                if (SlotManager.Instance.cardDown[(int)PlayerName.CASTOR])
                {
                    menustate = MenuState.WAIT_POLLUX;
                    StartCoroutine(BackToInitialState());
                    //lancer le feedback d'attente 
                }

                else if (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX])
                {
                    menustate = MenuState.WAIT_CASTOR;
                    StartCoroutine(BackToInitialState());
                    // lancer le feedback d'attente 
                }
                break;


            case MenuState.WAIT_CASTOR:
                if (SlotManager.Instance.cardDown[(int)PlayerName.CASTOR] && !coroutineFeedback)
                {
                    StartCoroutine(SwitchStateFeedback());
                }
                break;

            case MenuState.WAIT_POLLUX:
                if (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX] && !coroutineFeedback)
                {
                    StartCoroutine(SwitchStateFeedback());
                }
                break;
        }
        // Il faut que je checke les feedbacks et que je retire ceux qui ne sont pas utiles içi
        // Il faut également que je rajoute le changement de scène après les feedbacks positifs 
        IEnumerator SwitchStateFeedback()
        {
            coroutineFeedback = true;

            CardSymbol successSymbol;
            int slotIndex;
            if (SlotManager.Instance.CheckFullSuccess(out successSymbol))
            {
                Debug.Log("Full Success");
                //audioManager.PlaySFX(feedbackPositif, volumePositif);
            }
            else
            {
                if (SlotManager.Instance.CheckSymbolSuccess(out successSymbol))
                {
                    Debug.Log("Symbol Success");
                    // audioManager.PlaySFX(feedbackSymbol, volumeSymbol);

                }

                if (SlotManager.Instance.CheckSlotSuccess(out slotIndex))
                {
                    Debug.Log("Slot Success");
                    //audioManager.PlaySFX(feedbackSlot, volumeSlot);

                }
            }
            yield return new WaitForSeconds(3f);
            menustate = MenuState.FEEDBACK;
            coroutineFeedback = false;
        }
        IEnumerator BackToInitialState()
        {
            yield return new WaitForSeconds(5);
            if (menustate == MenuState.WAIT_CASTOR || menustate == MenuState.WAIT_POLLUX)
            {
                menustate = MenuState.WAIT_BOTH;
            }
        }
    }
}
}
