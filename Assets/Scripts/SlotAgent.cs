using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SlotAgent : MonoBehaviour
{
    public int index;

    [Space(10)]

    private SpriteRenderer spriteRenderer;
    public ParticleSystem lockingParticleSystem;
    public ParticleSystem pinkParticleSystem;
    public ParticleSystem blueParticleSystem;
    public ParticleSystem orangeParticleSystem;
    public VisualEffect yellowBasicFeedback;
    public VisualEffect blueBasicFeedback;
    public VisualEffect orangeBasicFeedback;
    public VisualEffect greenBasicFeedback;
    public VisualEffect activableEffect;
    public GameObject YellowFeedback;
    public GameObject BlueFeedback;
    public GameObject OrangeFeedback;
    public GameObject GreenFeedback;
    private bool visualeffectRun;
    private bool islocked = false;


    private bool particleSystemRun = false;

    private void Start()
    {
        activableEffect.Play();
        //       spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(SlotManager.Instance.slots[index].locked)
        {
            //  spriteRenderer.sprite = SlotManager.Instance.lockedSlotSprite;

            yellowBasicFeedback.Reinit();
            blueBasicFeedback.Reinit();
            orangeBasicFeedback.Reinit();

            greenBasicFeedback.Reinit();

            yellowBasicFeedback.Stop();
            blueBasicFeedback.Stop();
            orangeBasicFeedback.Stop();

            greenBasicFeedback.Stop();
            visualeffectRun = false;
            //YellowFeedback.SetActive(false);
            //BlueFeedback.SetActive(false);
            //OrangeFeedback.SetActive(false);
            if (islocked==false)
            {
                activableEffect.Stop();
                islocked = true;
                //lockingParticleSystem.Play();
            }

            particleSystemRun = false;
        }
        else
        {
            
            if (islocked==true)
            {
                activableEffect.Play();
                islocked = false;
            }

            switch (SlotManager.Instance.slots[index].state)
            {
                case SlotState.EMPTY:
                    yellowBasicFeedback.Reinit();
                    blueBasicFeedback.Reinit();
                    orangeBasicFeedback.Reinit();

                    greenBasicFeedback.Reinit();

                    yellowBasicFeedback.Stop();
                    blueBasicFeedback.Stop();
                    orangeBasicFeedback.Stop();

                    greenBasicFeedback.Stop();
                    visualeffectRun = false;


                    //YellowFeedback.SetActive(false);
                    //BlueFeedback.SetActive(false);
                    //OrangeFeedback.SetActive(false);

                    //spriteRenderer.sprite = SlotManager.Instance.emptySlotSprite;
                    break;

                case SlotState.PINK:
                    if (!visualeffectRun)
                    {
                        YellowFeedback.SetActive(true);
                        yellowBasicFeedback.Play();
                        Debug.Log("play");
                        visualeffectRun = true;
                    }

                    //spriteRenderer.sprite = SlotManager.Instance.pinkSlotSprite;
                    //if (!particleSystemRun)
                    //{
                    //    particleSystemRun = true;
                    //    pinkParticleSystem.Play();
                    //}
                    break;

                case SlotState.BLUE:
                    if (!visualeffectRun)
                    {
                        BlueFeedback.SetActive(true);
                        blueBasicFeedback.Play();
                        Debug.Log("play");
                        visualeffectRun = true;
                    }
                    //spriteRenderer.sprite = SlotManager.Instance.blueSlotSprite;

                    //if (!particleSystemRun)
                    //{
                    //    particleSystemRun = true;
                    //    blueParticleSystem.Play();
                    //}
                    break;

                case SlotState.ORANGE:
                    if (!visualeffectRun)
                    {
                        OrangeFeedback.SetActive(true);
                        orangeBasicFeedback.Play();
                        Debug.Log("play");
                        visualeffectRun = true;
                    }
                    //spriteRenderer.sprite = SlotManager.Instance.orangeSlotSprite;

                    //if (!particleSystemRun)
                    //{
                    //    particleSystemRun = true;
                    //    orangeParticleSystem.Play();
                    //}

                    break;



                case SlotState.GREEN:

                    if (!visualeffectRun)
                    {
                        GreenFeedback.SetActive(true);
                        greenBasicFeedback.Play();
                        Debug.Log("play");
                        visualeffectRun = true;
                    }

                    break;
            }
        }
    }
}
