using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetManager : LobbyManager{

	/*----------------------------------------------------------------------------------------------------------------
	----------------------------------------------------- MENU  ------------------------------------------------------
	----------------------------------------------------------------------------------------------------------------*/
	[Header("UI Settings")]
	public GameObject settingsPanel;
	public Dropdown qualityDropdown;
	public Slider volumeSlider;
	public Dropdown screenResolutions;
	public Toggle fullscreenToggle;
	public Resolution[] resolutions;
	List<string> resolutionStrings = new List<string> ();

	private static NetManager instance;
	public static NetManager Instance{
		get{ 
			return instance;
		}
	}


    public override void Start()
    {
		instance = this;
        base.Start();
        SceneManager.sceneLoaded += LevelOnLoad;
		resolutions = Screen.resolutions;

		for (int i = 0; i < resolutions.Count(); ++i)
			resolutionStrings.Add (resolutions [i].width.ToString () + " x " + resolutions [i].height);
		screenResolutions.ClearOptions ();
		screenResolutions.AddOptions(resolutionStrings);
		screenResolutions.value = resolutions.ToList().FindIndex(x => x.Equals(Screen.currentResolution));
		qualityDropdown.ClearOptions ();
		qualityDropdown.AddOptions (QualitySettings.names.ToList ());
		qualityDropdown.value = QualitySettings.GetQualityLevel ();
		fullscreenToggle.isOn = Screen.fullScreen;
		currentTargetPanel = mainPanel;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelOnLoad;
    }


    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
	{
		var spawnLocations = GameObject.FindObjectsOfType<NetworkStartPosition>().ToList();
		var playerSpawns = spawnLocations.FindAll(x => x.gameObject.CompareTag("playerSpawn")).ToList();
		Transform spawnLocation = playerSpawns[Random.Range(0, playerSpawns.Count)].transform;
		GameObject go = Instantiate(gamePlayerPrefab, spawnLocation.position, spawnLocation.rotation) as GameObject;
		return go;

	}

	public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
	{
		LobbyPlayer player = lobbyPlayer.GetComponent<LobbyPlayer>();
		NetworkedThirdPersonCharacter pl = gamePlayer.GetComponent<NetworkedThirdPersonCharacter>();
		pl.name = player.playerName;
		pl.playerName = player.playerName;
		pl.playerColour = player.playerColor;
		pl.playerID = lobbySlots.ToList ().FindIndex (x => x.GetComponent<LobbyPlayer>() == player);// conn.connectionId;// playerControllerId;
		return true;
	}

	public void ShowSettings(bool show)
	{
		settingsPanel.SetActive(show);
	}

	public void ApplyQualitySettings()
	{
		QualitySettings.SetQualityLevel(qualityDropdown.value);
		PlayerPrefs.SetFloat("Setting.Volume", volumeSlider.value);
		ReturnToPreviousPanel ();
	}

	public void LevelOnLoad(Scene newScene, LoadSceneMode mode)
	{
		if(newScene.buildIndex != 0 && !GameManager.instance.enabled)
		{
			GameManager.instance.enabled = true;
            //GameManager.instance.enemiesRemainigText = GameObject.Find("EnemiesRemaining").GetComponent<Text>();
			StartCoroutine( GameManager.instance.SpawnFirstWaveWithDelay(5.0f));
		    
		}
	}

	public GameObject quitConfirmPanel, serverBrowserPanel, mainPanel, lobbyPlayerListPanel;
	GameObject previousPanel, currentTargetPanel;

    public void QuitGame()
    {
		TransitionPanel (quitConfirmPanel);
    }

	public void ConfirmQuit()
	{
		Application.Quit ();
	}

	public void PlayGame()
	{
		TransitionPanel (serverBrowserPanel);
	}

	public void ShowSettings()
	{
		TransitionPanel (settingsPanel);
	}

	public void TransitionFromLobbyToMenu()
	{
		TransitionPanel (mainPanel);
		StopHost();
		StopServer ();
		StopClient ();

		TransitionPanel (serverBrowserPanel);

	}

	void TransitionPanel(GameObject targetPanel)
	{
		previousPanel = currentTargetPanel;
		previousPanel.SetActive (false);
		currentTargetPanel = targetPanel;
		currentTargetPanel.SetActive (true);
	}

	public void TransitonToLobbyPlayerList()
	{
		StartHost ();
		TransitionPanel (lobbyPlayerListPanel);
	}

	public void ReturnToPreviousPanel()
	{
		var pp = previousPanel;
		previousPanel = currentTargetPanel;
		previousPanel.SetActive (false);
		currentTargetPanel = pp;
		currentTargetPanel.SetActive (true);

	}


	public override void OnLobbyStopHost()
	{
		ResetToMenu ();
	}

	public override void OnLobbyStopClient()
	{
		ResetToMenu ();
	}

	void ResetToMenu()
	{
		mainPanel.SetActive (true);
		GameManager.instance.CancelInvoke ();
		GameManager.instance.Reset ();
		GameManager.instance.gameObject.SetActive (false);
		currentTargetPanel = mainPanel;
	}
}