using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotAgent : MonoBehaviour
{
    public int index;

    [Space(10)]

    private SpriteRenderer spriteRenderer;
    public ParticleSystem lockingParticuleSystem;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(SlotManager.Instance.slots[index].locked)
        {
            spriteRenderer.sprite = SlotManager.Instance.lockedSlotSprite;
        }
        else
        {
            switch (SlotManager.Instance.slots[index].state)
            {
                case SlotState.EMPTY:
                    spriteRenderer.sprite = SlotManager.Instance.emptySlotSprite;
                    break;

                case SlotState.PINK:
                    spriteRenderer.sprite = SlotManager.Instance.pinkSlotSprite;
                    break;

                case SlotState.BLUE:
                    spriteRenderer.sprite = SlotManager.Instance.blueSlotSprite;
                    break;

                case SlotState.ORANGE:
                    spriteRenderer.sprite = SlotManager.Instance.orangeSlotSprite;
                    break;
            }
        }
    }
}
