using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * based on CharacterController2D (took out Jump, Crouch, etc but can be put back in by referring to that)
 */


public class Enemy : MonoBehaviour, IPausableObject 
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement

    [SerializeField] private bool m_FlipAnimations; // Flip animation when facing Left; otherwise use Animator
    [SerializeField] private bool m_UnFlippedIsLeft = true; // Flip animation when facing Left; otherwise use Animator
    [SerializeField] BoxCollider2D m_WeaponBoxCollider; // used to test collision with player. Trigger should be turned on.

    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    private Animator m_Animator;
    
    private GameObject m_Player; // the protagonist whom we will target, fetched using "Player" tag
 

    public enum EnemyState { idle = 0, patrol, chase, attack, dead };

    public EnemyState state = EnemyState.idle;

    public float patrolSpeed = 0.75f;
    public float chaseSpeed = 1.5f;


    public GameObject patrolTo;

    //[HideInInspector]    public GameObject nextTarget;

    private Vector3[] patrolStops;
    private int currPatrolStop;

    public Vector3 vectorToPlayer;
    public float distToPlayer = float.MaxValue;

    private Vector3 targetPosition;
    private float targetSpeed = 0f;


    public int maxHitPoints = 1;
    public int currHP = 1;
    public int weaponDamage = 4;

    private bool attackLanded = false;
    private bool isPaused = false;

    public AudioClip attackSound;
    private AudioSource audioSource;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();


        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();



        // Get player's GameObject by searching for Tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            m_Player = players[0];
        }
        else
        {
            Debug.Log("Enemy " + name + " could not find object with tag for Player.");
        }

        if (patrolTo != null)
        {
            patrolStops = new Vector3[] { transform.position, patrolTo.transform.position };
        }
        else
        {
            patrolStops = new Vector3[] { transform.position };
        }

        currPatrolStop = 0;

        //ChangeState(EnemyState.idle);

        // extra flip at the beginning if the artist made facing left art
        if (m_FlipAnimations && m_UnFlippedIsLeft)
        {   
            m_FacingRight = false;

            // // Multiply the character's x local scale by -1.
            // Vector3 theScale = transform.localScale;
            // theScale.x *= -1;
            // transform.localScale = theScale;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
      //  if (m_WeaponBoxCollider != null) m_WeaponBoxCollider.enabled = false; // turn off weapon box collider until we are in attack mode; the animation controls when it is active
        
    }

// via interface IPausableObject

    public void Pause()
    {
        Debug.Log("PAUSE");
        isPaused = true;
    } 

    public void Resume()
    {
        Debug.Log("RESUME");

        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(isPaused) return;

        // Find distance to player

            vectorToPlayer = m_Player.transform.position - transform.position;
        vectorToPlayer.z = 0f;
        distToPlayer = vectorToPlayer.magnitude;

        if (state != EnemyState.dead)
        {
            if (currHP <= 0)
            {
                state = EnemyState.dead;
            }
        }

        switch (state)
        {
            case EnemyState.idle:
                UpdateIdle(Time.deltaTime);
                break;

            case EnemyState.patrol:
                UpdatePatrol(Time.deltaTime);
                break;

            case EnemyState.chase:
                UpdateChase(Time.deltaTime);
                break;

            case EnemyState.attack:
                UpdateAttack(Time.deltaTime);
                break;

            case EnemyState.dead:
                UpdateDead(Time.deltaTime);
                break;

            default:
                break;
        }

        Move(targetSpeed);
    }

    public void Move(float move)
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move, m_Rigidbody2D.velocity.y);

        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        //m_Rigidbody2D.velocity = targetVelocity; // not sure why ABOVE doesn't work very well at smaller velocities, but it doesn't. 

        //Debug.Log("Velocity.x target = " + targetVelocity.x + " actual = " + m_Rigidbody2D.velocity.x);

        // check for direction change
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the character.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the character.
            Flip();
        }
    }

    private void Flip()
    {

        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        if (m_FlipAnimations )
        {

            // Multiply the character's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else
        {

            // Use the Animator to set the state; note: must have this Bool set up in the Animator
            m_Animator.SetBool("Facing_Right", m_FacingRight);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        Debug.Log("Enemy " + name + " ChangeState: newState = " + newState);

        if (state != newState)
        {

            // add exit old state code here...

            // switch states
            state = newState;

            //add enter new state here
            switch (state)
            {
                case EnemyState.idle:
                    //    m_Animator.SetTrigger("Idle");
                    break;


                case EnemyState.patrol:
                    m_Animator.SetTrigger("Walk");
                    NextPatrolTarget();
                    break;

                case EnemyState.chase:
                    m_Animator.SetTrigger("Run");
                    break;

                case EnemyState.attack:
                    //if (m_WeaponBoxCollider != null) m_WeaponBoxCollider.enabled = true;
                    attackLanded = false;
                    PlayAttackSound();
                    m_Animator.SetTrigger("Attack");
                    break;

                case EnemyState.dead:
                    m_Animator.SetTrigger("Die");
                    break;

                default:
                    break;
            }
        }
    }

    public virtual void UpdateIdle(float dT)
    {
        if (distToPlayer < 40f)
        {
            ChangeState(EnemyState.patrol);
        }
    }

    public virtual void UpdatePatrol(float dT)
    {
       

        if ((distToPlayer < 10f) && (Mathf.Abs(vectorToPlayer.y) < 1.5f))
        {
            ChangeState(EnemyState.chase);

        }else{

            float distToNextPatrolStop = targetPosition.x - transform.position.x;
            //        Debug.Log("Enemy " + name + " UpdatePatrol: distToNextPatrolStop = " + distToNextPatrolStop);

            if (Mathf.Abs(distToNextPatrolStop * Time.deltaTime) < Mathf.Abs(targetSpeed * Time.deltaTime))
            {
                NextPatrolTarget();
            }
        }
    }

    public virtual void UpdateChase(float dT)
    {
        targetPosition = m_Player.transform.position;

        if (targetPosition.x - transform.position.x < 0)
        {
            targetSpeed = -chaseSpeed;
        }
        else
        {
            targetSpeed = chaseSpeed;
        }

        if (distToPlayer < 1f)
        {
            ChangeState(EnemyState.attack);
        }else{

            // check to see if on a different level
            if(Mathf.Abs(vectorToPlayer.y) > 2.0f){
                ChangeState(EnemyState.patrol);
            }else{
                if(distToPlayer > 15f){
                    ChangeState(EnemyState.patrol);
                }
            }
        }
    }


    public virtual void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);

    }

    public virtual void UpdateAttack(float dT)
    {
        targetSpeed = 0f;

        if (distToPlayer > 2f)
        {
            ChangeState(EnemyState.chase);
        }

    }

    public void AttackAnimationFinished(){
        ChangeState(EnemyState.chase);
    }

    public virtual void UpdateDead(float dT)
    {
        // hmm what to do here?

    }

    public void NextPatrolTarget()
    {
        Debug.Log("NextPatrolTarget()!");

        currPatrolStop++;
        if (currPatrolStop >= patrolStops.Length)
        {
            currPatrolStop = 0;
        }
        targetPosition = patrolStops[currPatrolStop];

        if (targetPosition.x - transform.position.x < 0)
        {
            targetSpeed = -patrolSpeed;
        }
        else
        {
            targetSpeed = patrolSpeed;
        }

    }

//the weapon collider was enabled and triggered this
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(name + ": " + other.name + " trigger enter : " + Time.time);

        if (other.tag == "Player")
        {
            if(! attackLanded){
                Debug.Log(name + " HITS!!!");
                PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
                player.TakeDamage(weaponDamage);
                attackLanded = true;
            }
       

        }

    }


}
