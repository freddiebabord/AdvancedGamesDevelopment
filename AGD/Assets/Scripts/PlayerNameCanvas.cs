using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerNameCanvas : MonoBehaviour
{

    public Camera targetCamera;
    public CanvasGroup canvasGoup;
    public float minVisibleDistance;
    public Text playerNameText;
    public NetworkedThirdPersonCharacter npc;

    // Use this for initialization
    void Start ()
	{
	    canvasGoup = GetComponent<CanvasGroup>();
	    playerNameText.text = npc.playerName;
	}
	
	// Update is called once per frame
	void Update () {
	    if (targetCamera == null)
	    {
	        var cams = FindObjectsOfType<Camera>();
	        for (var i = 0; i < cams.Length; i++)
	        {
	            if (cams[i].gameObject.activeInHierarchy)
	            {
	                targetCamera = cams[i];
	                break;
	            }
	        }
	    }
	    transform.LookAt(targetCamera.transform);
        float dist = Vector3.Distance(transform.position, targetCamera.transform.position);
	    if (dist > minVisibleDistance)
            dist = 1;
	    else
            dist /= minVisibleDistance;
	    canvasGoup.alpha = Mathf.Lerp(0, 1, dist);
	}
}
