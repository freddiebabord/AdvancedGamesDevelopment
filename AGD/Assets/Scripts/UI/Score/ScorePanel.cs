using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired.Utils.Classes.Data;

public class ScorePanel : MonoBehaviour
{

    private PlayerScoreItem template;
    private List<PlayerScoreItem> scoreItems = new List<PlayerScoreItem>();

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(PostStartWait());
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

        }

        template.gameObject.SetActive(false);
    }


    public void PostScoreToThisBoard(int playerID, int newScore)
    {
        scoreItems[playerID].SetScore(newScore);
    }
}
