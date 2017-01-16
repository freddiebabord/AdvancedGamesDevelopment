using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired.Utils.Classes.Data;

public class ScorePanel : MonoBehaviour
{

    private PlayerScoreItem template;
    private List<PlayerScoreItem> scoreItems = new List<PlayerScoreItem>();
    private bool ready = false;
	// Use this for initialization
	void Start ()
	{
	    if (!GameManager.instance)
	    {
	        enabled = false;
	        return;
	    }
        StartCoroutine(PostStartWait());
	}

    void FixedUpdate()
    {
        for (int i = 0; i < GameManager.instance.players.Count; ++i)
        {
            if (i >= scoreItems.Count) continue;
            if (scoreItems[i] == null) continue;
            if(GameManager.instance.ScoreTable.ContainsKey(i))
                scoreItems[i].SetScore(GameManager.instance.ScoreTable[i]);
            else if(ready)
                scoreItems[i].SetScore(0);
        }
    }

    IEnumerator PostStartWait()
    {
        yield return new WaitForSeconds(0.1f);
        template = GetComponentInChildren<PlayerScoreItem>();
        for (int i = 0; i < GameManager.instance.players.Count; ++i)
        {
            PlayerScoreItem playerScoreSub = (PlayerScoreItem)Instantiate(template, transform);
            playerScoreSub.SetName(GameManager.instance.players[i].playerName);
            scoreItems.Add(playerScoreSub);
            playerScoreSub.gameObject.name = "Player " + i.ToString();
        }

        template.gameObject.SetActive(false);
        ready = true;
    }


    public void PostScoreToThisBoard(int playerID, int newScore)
    {
        scoreItems[playerID].SetScore(newScore);
    }
}
