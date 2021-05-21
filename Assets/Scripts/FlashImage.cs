using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class FlashImage : MonoBehaviour
{

    Image _image = null;
    Coroutine _currentFlashRoutine = null;

    private EasingFunction.Function easingFunction = EasingFunction.Linear;


    void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void StartFlash(float secondsForOneFlash, float maxAlpha)
    {
           maxAlpha = Mathf.Clamp(maxAlpha, 0f, 1f);

        if (_currentFlashRoutine != null)
        {
            StopCoroutine(_currentFlashRoutine);
        }

        _currentFlashRoutine = StartCoroutine(Flash(secondsForOneFlash, maxAlpha));

    }

    public void StartFlash(float secondsForOneFlash, float maxAlpha, Color newColor){
        _image.color = newColor;

        StartFlash(secondsForOneFlash, maxAlpha);

    }

    IEnumerator Flash(float secondsForOneFlash, float maxAlpha) {
        // animate flash in
        float flashInDuration = secondsForOneFlash / 2.0f; 
        for(float t=0; t <= flashInDuration; t += Time.deltaTime){

            // create a new color change
            Color colorThisFrame = _image.color;

            // modify the alpha
            colorThisFrame.a = easingFunction(0f, maxAlpha, (t / flashInDuration));

            //apply it
            _image.color = colorThisFrame;

            //wait until next frame
            yield return null;

        }

        // animate flash out
        float flashOutDuration = secondsForOneFlash / 2.0f;
        for (float t = 0; t <= flashOutDuration; t += Time.deltaTime)
        {

            // create a new color change
            Color colorThisFrame = _image.color;

            // modify the alpha
            colorThisFrame.a = easingFunction(maxAlpha, 0f, (t / flashOutDuration));

            //apply it
            _image.color = colorThisFrame;

            //wait until next frame
            yield return null;

        }

    }
}
