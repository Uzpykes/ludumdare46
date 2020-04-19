using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UITurnCounter : MonoBehaviour
{
    public TextMeshProUGUI TurnField;
    public GameState GameState;

    void OnEnable()
    {
        GameState.onTurnSet += UpdateValue;
        SceneManager.sceneLoaded += (Scene, LoadMode) => {
            UpdateValue();
        };
        UpdateValue();
    }



    void OnDisable()
    {
        GameState.onTurnSet -= UpdateValue;
    }

    void UpdateValue()
    {
        TurnField.text = $"Year {GameState.CurrentTurn}";
    }
}
