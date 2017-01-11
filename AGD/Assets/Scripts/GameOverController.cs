using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

[System.Serializable]
public struct GOHolder
{
    public Text score;
    public Transform position;
}

public class GameOverController : NetworkBehaviour
{
    public List<GOHolder> dataContainers = new List<GOHolder>();


    public void ReturnToMenu()
    {
        if (isServer)
            NetManager.Instance.StopHost();
        else
            NetManager.Instance.StopClient();
    }
}
