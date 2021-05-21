using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingObject : MonoBehaviour
{
    public enum SlidingState {here, forward, there, backward};
    
    public AudioClip movingSound;
    public float movingTime;
    
    public GameObject destination;
    
    public EasingFunction.Ease easing = EasingFunction.Ease.Linear;

    [HideInInspector] public Vector3 origin;
    [HideInInspector] public Vector3 fixedDestination;
    
    public SlidingState state;

    private AudioSource audioSource;

    private float startTime;
    [HideInInspector] public Vector3 moveVector;

    private EasingFunction.Function easingFunction;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init() {
        origin = transform.position;
        state = SlidingState.here;
        fixedDestination = destination.transform.position; // store this value so it doesn't move WITH the object!

        if(movingSound != null) audioSource = gameObject.AddComponent<AudioSource>();
        moveVector = fixedDestination - origin;

        easingFunction = EasingFunction.GetEasingFunction(easing);
        // EasingFunction.EasingFunc func = GetEasingFunction(easing);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
        if(state == SlidingState.forward){

            float timeElapsed = (Time.timeSinceLevelLoad - startTime);

            if(timeElapsed >= movingTime ){
                // we are there.
                transform.position = fixedDestination;
                state = SlidingState.there;

                if (movingSound != null) audioSource.Stop();

            }else{
                // keep moving!


                //Linear function: transform.position = origin + moveVector * (timeElapsed/movingTime); // linear 

                //Easing function:
                float moveRatio =  easingFunction(0f, 1f,  (timeElapsed / movingTime)); // origin + moveVector * (timeElapsed/movingTime)
                transform.position = origin + moveVector * moveRatio;
            }
        }
        
    }

    public void Slide(){

       // Debug.Log(name + " Slide!");
        startTime = Time.timeSinceLevelLoad;
        state = SlidingState.forward;

        if (movingSound != null){
            // audioSource.PlayOneShot(movingSound);
            audioSource.clip = movingSound;
            audioSource.loop = true;
            audioSource.Play();

        }
  

    }


}
