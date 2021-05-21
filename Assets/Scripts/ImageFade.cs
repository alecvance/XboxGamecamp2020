using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFade : MonoBehaviour
{
    private bool activated = false;
    private SpriteRenderer spriteRenderer;

    [Range(0f,1f)] public float minAlpha;
    [Range(0f, 1f)] public float maxAlpha;
    [Range(0.0001f, 30f)] public float speed = 4f;

    public void Start(){
      
     
    }

    void OnEnable() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        Activate();
    }

    void OnDisable(){
        activated = false;
    }

    public void Activate()
    {
        if(!activated){
            activated = true;
            
        // fades the image out when you click
           StartCoroutine(FadeImage(true));
           
        }
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        Color newColor = spriteRenderer.color;
        while(activated){
            // fade from opaque to transparent
            if (fadeAway)
            {
                // loop over 1 second backwards
                
                //for (float i = 1; i >= 0; i -= Time.deltaTime)
                for (float f = maxAlpha; f >= minAlpha; f -= speed * Time.deltaTime)

                    {
                    // set color with i as alpha
                    //img.color = new Color(1, 1, 1, i);
                   // Debug.Log(f);
                    newColor.a = f;
                    spriteRenderer.color = newColor;

                    yield return null;
                }
                fadeAway = false;
            }
            // fade from transparent to opaque
            else
            {
                // loop over 1 second
                ///for (float i = 0; i <= 1; i += Time.deltaTime)
                for (float f = minAlpha; f <=maxAlpha; f += speed * Time.deltaTime)

                {
                    Debug.Log(f);
                    // set color with i as alpha
                    //img.color = new Color(1, 1, 1, i);
                    newColor.a = f;
                    spriteRenderer.color = newColor;
                    yield return null;
                }
                fadeAway = true;
            }
        }
    }
}