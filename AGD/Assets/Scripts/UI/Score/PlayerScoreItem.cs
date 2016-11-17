using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreItem : MonoBehaviour
{

    public Text playerName, playerScore;

    public void SetName(string name)
    {
        playerName.text = name;
    }

    public void SetScore(int score)
    {
        playerScore.text = score.ToString();
    }
}
