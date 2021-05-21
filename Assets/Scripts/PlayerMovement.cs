using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public RectTransform healthBar;
	float healthBarFullSize;

    public FlashImage flashImage;
    public int maxHitPoints = 10;
    public int currHP = 10;

	public AudioClip takeDamageSound;
	private AudioSource audioSource;

    private Rigidbody2D rigidBody;
	public float walkSpeed = 40f;
	float horizontalMove = 0f;
	bool jump = false; // true only when the jump button was JUST pressed! WAIT -- is this even being used?
	int jumpPhase = 0;
	bool crouch = false;
	public bool dead = false;
	public bool enterMode = false;
	public InteractableObject interactionObject;
	public DialogText dialogText;

	void Start() {
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
		audioSource = gameObject.GetComponent<AudioSource>();
		healthBarFullSize = healthBar.sizeDelta.x;
	}


	// Update is called once per frame
	void Update () {


		if(! dead) {

			// get inputs (if not dead)
            horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true; // button pressed
            }

			if(horizontalMove == 0 && !jump){
				if(Input.GetButtonDown("Enter")){
					enterMode = true;
				}
			}

        }

		//animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //animator.SetFloat("VertSpeed", rigidBody.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

       
		if(jump==true && jumpPhase ==0){
			jumpPhase = 1; // going up!
            //Debug.Log("Jump!");
		}else{
			
			if(jumpPhase == 1){
				// if we were already in the air, going up....
					if (rigidBody.velocity.y < -0.001)
					{
						// then transition to falling
						jumpPhase = 2; // falling down
					}
			}
			else {
				if(jumpPhase == 2){
                    // if we have stopped moving downwards
                    if (rigidBody.velocity.y >= -0.001)
                    {
                        // then transition to stopped
                        jumpPhase = 0; // stopped
                    }
				}
			}
			
		}
/*
		if (Input.GetButtonDown("Crouch"))
		{
			//if(jumpPhase==0){
				crouch = true;
				animator.SetBool("IsCrouching", true);
			//}

		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
			animator.SetBool("IsCrouching", false);
		}
*/
        animator.SetInteger("JumpPhase", jumpPhase); 


    }

	void FixedUpdate ()
	{

		if(enterMode){
			// find door
			if(interactionObject != null){
				Debug.Log("Player found interactible object "+ interactionObject);
				interactionObject.StartInteraction(this);
			}
		}

		// Move our character in the Character Controller 2d Script.
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false; // clear the jump button state
		enterMode = false;

	}

	public void OnLanding ()
    {
		/*
		if(jumpPhase == 2){
            Debug.Log("OnLanding()");
			jumpPhase = 0; // jump stop!


            animator.SetInteger("JumpPhase", jumpPhase); 
        }
		*/
    }

	public void OnCrouching (bool isCrouching)
    {
		/*
        Debug.Log("OnCrouching("+isCrouching+")");
        animator.SetBool("IsCrouching", isCrouching);
		*/
    }


	public void Die() 
	{
        Debug.Log("Player DIES!");

        currHP = 0;
        UpdateHealthBar();
		dead = true;
		horizontalMove = 0f;
		jump = false;
		crouch = false;
		animator.SetBool("Dead", true);

        float flashTime = 3.0f; //0.5f * (1.0f - (0.5f * currHP/maxHitPoints));
        float flashMaxAlpha = 0.6f; //0.4f + (hp/10f);
        flashImage.StartFlash(flashTime, flashMaxAlpha);

		dialogText.SetText("Game Over!");

		StartCoroutine(WaitToRestart());

    }

	public void TakeDamage(int hp){
		if(!dead){

			audioSource.PlayOneShot(takeDamageSound);
			
			currHP -= hp;
			if(currHP <0 ) currHP = 0;
            UpdateHealthBar();
			Debug.Log("Player TAKES " + hp + " HP damage and has " + currHP + " HP left.");

            float flashTime = 0.25f; //0.5f * (1.0f - (0.5f * currHP/maxHitPoints));
            float flashMaxAlpha = 0.6f; //0.4f + (hp/10f);
            flashImage.StartFlash(flashTime, flashMaxAlpha);


            if(currHP <=0 ){
				Die();
			}
		}
	}

	public void RestoreHP(int hp){
		if(! dead){
			currHP += hp;
			if(currHP > maxHitPoints ) currHP = maxHitPoints;
            UpdateHealthBar();
			Debug.Log("Player RESTORES " + hp + " and now has " + currHP + " .");

			/*
            float flashTime = 0.25f; //0.5f * (1.0f - (0.5f * currHP/maxHitPoints));
            float flashMaxAlpha = 0.6f; //0.4f + (hp/10f);
            flashImage.StartFlash(flashTime, flashMaxAlpha);
			*/

		}
	}

	public void UpdateHealthBar(){
		float w = healthBarFullSize * currHP / maxHitPoints;

		//healthBar.sizeDelta = new Vector2(w, healthBar.sizeDelta.y);

        healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        healthBar.ForceUpdateRectTransforms();


    }

	IEnumerator WaitToRestart(){

        for (float t = 0; t <= 5.0f; t += Time.deltaTime)
        {
            yield return null;
		}

		SceneManager.LoadScene("Level_1");

	}

}
