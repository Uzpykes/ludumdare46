using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button HowToButton;
    public Button ExitGameButton;
    public RectTransform HowToPanel;

    public int GameSceneIndex;

    public void Start()
    {
        PlayButton.onClick.AddListener(LoadGameScene);
        HowToButton.onClick.AddListener(ShowHowTo);
        ExitGameButton.onClick.AddListener(Application.Quit);
    }

    public void ShowHowTo()
    {
        HowToPanel.gameObject.SetActive(true);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(GameSceneIndex, LoadSceneMode.Single);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
