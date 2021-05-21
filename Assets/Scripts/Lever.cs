using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractableObject
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject activator; // a game object that gets activated when the switch is thrown. can be an empty one with scripts.

    public enum LeverState { off, on };

    public LeverState state = LeverState.off;
    public bool canReset = false;

    Sprite defaultSprite;

    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();

        defaultSprite = spriteRenderer.sprite;

        if (state == LeverState.on)
        {
            ThrowSwitch();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ThrowSwitch()
    {
        animator.enabled = true;
        state = LeverState.on;
        activator.SetActive(true);
    }

    public void ResetSwitch()
    {
        animator.enabled = false;
        spriteRenderer.sprite = defaultSprite;
        state = LeverState.off;
        activator.SetActive(false);
    }


    override public void StartInteraction(PlayerMovement player)
    {
        if (state == LeverState.off)
        {
            ThrowSwitch();
        }
        else
        {
            if(canReset){
                ResetSwitch();
            }
        }
    }
}

