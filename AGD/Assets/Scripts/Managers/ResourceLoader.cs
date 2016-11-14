using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResourceLoader : MonoBehaviour
{

    public static ResourceLoader inst;
    public Slider progressBar;
    public Text loadingString;
    private ResourceRequest async;
    private bool loadNextScene;
    public List<string> loadingStrings = new List<string>();

    void Awake()
    {
        if(inst == null)
            inst = this;
    }

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(LoadGameAssets());
        InvokeRepeating("ChangeText", 0.0f, 1.5f);
    }
	
	// Update is called once per frame
	void Update () {
	    if (async != null)
	    {
	        progressBar.value = async.progress;
            
            if(async.isDone && !loadNextScene)
                StartCoroutine(LoadNextScene());
        }
	}

    IEnumerator LoadGameAssets()
    {
        async = Resources.LoadAsync("");
        yield return  async;
    }

    IEnumerator LoadNextScene()
    {
        loadNextScene = true;
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }

    void ChangeText()
    {
        loadingString.text = loadingStrings[Random.Range(0, loadingStrings.Count)];
    }
}
