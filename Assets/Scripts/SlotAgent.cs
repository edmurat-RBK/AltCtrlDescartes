using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotAgent : MonoBehaviour
{
    public int index;

    [Space(10)]

    private SpriteRenderer spriteRenderer;
    public ParticleSystem lockingParticleSystem;
    public ParticleSystem pinkParticleSystem;
    public ParticleSystem blueParticleSystem;
    public ParticleSystem orangeParticleSystem;

    private bool particleSystemRun = false;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(SlotManager.Instance.slots[index].locked)
        {
            spriteRenderer.sprite = SlotManager.Instance.lockedSlotSprite;

            if(!lockingParticleSystem.isPlaying)
            {
                lockingParticleSystem.Play();
            }

            particleSystemRun = false;
        }
        else
        {
            if(lockingParticleSystem.isPlaying)
            {
                lockingParticleSystem.Stop();
            }

            switch (SlotManager.Instance.slots[index].state)
            {
                case SlotState.EMPTY:
                    spriteRenderer.sprite = SlotManager.Instance.emptySlotSprite;
                    break;

                case SlotState.PINK:
                    spriteRenderer.sprite = SlotManager.Instance.pinkSlotSprite;
                    
                    if (!particleSystemRun)
                    {
                        particleSystemRun = true;
                        pinkParticleSystem.Play();
                    }
                    break;

                case SlotState.BLUE:
                    spriteRenderer.sprite = SlotManager.Instance.blueSlotSprite;
                    
                    if (!particleSystemRun)
                    {
                        particleSystemRun = true;
                        blueParticleSystem.Play();
                    }
                    break;

                case SlotState.ORANGE:
                    spriteRenderer.sprite = SlotManager.Instance.orangeSlotSprite;
                    
                    if (!particleSystemRun)
                    {
                        particleSystemRun = true;
                        orangeParticleSystem.Play();
                    }
                    break;
            }
        }
    }
}
