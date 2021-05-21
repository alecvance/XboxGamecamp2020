using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : InteractableObject
{


    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public enum TorchState { unlit, lit };

    public TorchState state = TorchState.unlit;

    Sprite defaultSprite;

    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();

        defaultSprite = spriteRenderer.sprite;

        if(state == TorchState.lit){
            LightTorch();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LightTorch()
    {  
        animator.enabled = true;
        state = TorchState.lit;
    }

    public void ExtinguishTorch()
    {
        animator.enabled = false;
        spriteRenderer.sprite = defaultSprite;
        state = TorchState.unlit;
    }



    override public void StartInteraction(PlayerMovement player)
    {
        if (state == TorchState.unlit)
        {
            LightTorch();
        }else{
            ExtinguishTorch();
        }
    }
}
