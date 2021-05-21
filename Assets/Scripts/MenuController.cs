using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{

    public GameObject settingsPanel;
    public Button startGameButton;
    public Button settingsButton;
    public Button closeButton;
    public GameObject gameLogo;
   

    void Start()
    {
        this.hideSettings();

        var startGameBtn = startGameButton.GetComponent<Button>();
        startGameBtn.onClick.AddListener(StartGame);

        var settingsBtn = settingsButton.GetComponent<Button>();
        settingsButton.onClick.AddListener(ShowSettings);
      

        var closeBtn = closeButton.GetComponent<Button>();
        closeBtn.onClick.AddListener(hideSettings);



    }

    void Update()
    {

      
    }

    void clickSound(BaseEventData baseEventData)
    {
        Debug.Log("Hello");
    }

    void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("Level_1");
    }

    void ShowSettings()
    {

        settingsPanel.SetActive(true);
        settingsButton.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        gameLogo.gameObject.SetActive(false);

    }

    void hideSettings()
    {

        settingsButton.gameObject.SetActive(true);
        startGameButton.gameObject.SetActive(true);
        settingsPanel.SetActive(false);
        closeButton.gameObject.SetActive(false);
        gameLogo.gameObject.SetActive(true);
      
    }

    void turnOffBtns()
    {
        Debug.Log("Turn off Buttons");
    }

   

}
