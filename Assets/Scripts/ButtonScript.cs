using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{

 
    private void onMouseOver()
    {
        Debug.Log("MouseOver");
    }

    private void OnMouseDown()
    {
        Debug.Log("Licke");
    }


}
