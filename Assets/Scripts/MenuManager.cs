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
                while (SlotManager.Instance.cardDown[(int)PlayerName.CASTOR])
                {
                    menustate = MenuState.WAIT_POLLUX;
                    //lancer le feedback d'attente une fois et le faire boucler (contour d'aura de la couleur autour de l'emplacement selectionné, le graph de base s'illumine)
                }

                while (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX])
                {
                    menustate = MenuState.WAIT_CASTOR;
                    // lancer le feedback d'attente et le faire boucler
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
    }
    }
}
