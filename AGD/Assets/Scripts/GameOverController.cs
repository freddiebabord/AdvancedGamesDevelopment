using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct GOHolder
{
    public Text score;
    public Transform position;
}

public class GameOverController : MonoBehaviour
{
    public List<GOHolder> dataContainers = new List<GOHolder>();
    
}
