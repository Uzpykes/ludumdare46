using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Subtitle;
    public Button Replay;
    public Button MainMenu;
    public Button Exit;

    public GameState GameState;

    public void Start()
    {
        Replay.onClick.AddListener(Reload);
        MainMenu.onClick.AddListener(LoadMainMenu);
        Exit.onClick.AddListener(ExitGame);
    }

    public void SetResult(string title, string subtitle)
    {
        Title.text = title;
        Subtitle.text = subtitle;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Reload()
    {
        LoadScene(1);
    }

    public void LoadMainMenu()
    {
        LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
}
