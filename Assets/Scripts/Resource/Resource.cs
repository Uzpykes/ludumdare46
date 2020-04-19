using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Resource : ScriptableObject
{
    public ResourceTuple ResourceInfo;
    public string ResourceDescription;
    public string ResourceName;
    public Sprite ResourceSprite;

    private void OnEnable()
    {
        Reset();
        SceneManager.sceneLoaded += (Scene, LoadSceneMode) => {
            Reset();
        };
    }

    private void OnDisable()
    {
        Reset();
    }

    private void Reset()
    {
        ResourceInfo.Count = 0;
    }

}

[System.Serializable]
public class ResourceTuple
{
    [SerializeField]
    public ResourceType Type;
    [SerializeField] //hmmmm
    private int m_Count;
    public int Count {
        get {
            return m_Count;
        }
        set {
            m_Count = value;
            onCountChanged?.Invoke();
        }
    }

    public delegate void CountChange();
    public CountChange onCountChanged;

    public ResourceTuple(ResourceType type, int count)
    {
        Type = type;
        Count = count;
    }
}


public enum ResourceType
{
    // Food stuff
    Grain,
    Fish,
    Bread,
    Beer,
    // Building stuff
    Wood,
    Clay,
    Brick,
    Iron,
    Cloth,
    Prosperity
}