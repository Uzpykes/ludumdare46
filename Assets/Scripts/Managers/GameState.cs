using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    private int m_NextEventTurn;
    public int NextEventTurn {
        get {
            return m_NextEventTurn;
        }
        set {
            m_NextEventTurn = value;
            EventShown = false;
            onNextEventTurnSet?.Invoke();
        }
    }

    private int m_CurrentTurn;
    public int CurrentTurn {
        get {
            return m_CurrentTurn;
        }
        set {
            m_CurrentTurn = value;
            onTurnSet?.Invoke(); 
        }}


    private bool m_CapitalSpawned;
    public bool CapitalSpawned {
        get {
            return m_CapitalSpawned;
        }
        set {
            m_CapitalSpawned = value;
            if (m_CapitalSpawned == true) 
                onCapitalSpawn?.Invoke(); 
        }}

    private bool m_CapitalPlaced;
    public bool CapitalPlaced {
        get {
            return m_CapitalPlaced;
        }
        set {
            m_CapitalPlaced = value;
            if (m_CapitalPlaced == true) 
                onCapitalPlace?.Invoke(); 
        }}

    private bool m_EventShown;
    public bool EventShown {
        get {
            return m_EventShown;
        }
        set {
            m_EventShown = value;
        }
    }

    
    private bool m_EventHandled;
    public bool EventHandled {
        get {
            return m_EventHandled;
        }
        set {
            m_EventHandled = value;
        }
    }

    private bool m_Victory;
    public bool Victory {
        get {
            return m_Victory;
        }
        set {
            m_Victory = value;
        }
    }

    private bool m_Loss;
    public bool Loss {
        get {
            return m_Loss;
        }
        set {
            m_Loss = value;
        }
    }



    public delegate void StateChange();
    public StateChange onCapitalSpawn;
    public StateChange onCapitalPlace;

    public delegate void ValueSet();
    public ValueSet onTurnSet;
    public ValueSet onNextEventTurnSet;


    public void OnEnable()
    {
        Reset();
        SceneManager.sceneLoaded += (scene, loadedScene) => {
            Reset();
        };
    }

    private void Reset()
    {
        m_CurrentTurn = 0;
        m_NextEventTurn = 3;
        m_EventShown = false;
        m_CapitalPlaced = false;
        m_CapitalSpawned = false;
        m_Loss = false;
        m_Victory = false;
    }
}
