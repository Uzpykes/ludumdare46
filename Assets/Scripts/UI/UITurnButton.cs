using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITurnButton : MonoBehaviour
{
    public GameState GameState;
    public TextMeshProUGUI ButtonText;
    public Button Button;

    public void Start()
    {
        Button.onClick.AddListener(
            () => {
                GameBoardManager.instance.TryAdvance(); //oof
            }
        );
        UpdateButton();
        GameState.onCapitalPlace += UpdateButton;
        GameState.onTurnSet += UpdateButton;
        GameState.onNextEventTurnSet += UpdateButton;
    }

    void OnDisable()
    {
        GameState.onCapitalPlace -= UpdateButton;
        GameState.onTurnSet -= UpdateButton;
        GameState.onNextEventTurnSet -= UpdateButton;
    }

    private void UpdateButton()
    {
        if (!GameState.CapitalPlaced || (GameState.CurrentTurn == GameState.NextEventTurn && (GameState.EventShown && !GameState.EventHandled)))
            Button.interactable = false;
        else
            Button.interactable = true;

        if (GameState.CurrentTurn == GameState.NextEventTurn && !GameState.EventShown)
        {
            ButtonText.text = "Draw Event";
        }
        else
        {
            ButtonText.text = "Next Year";
        }
    }

}
