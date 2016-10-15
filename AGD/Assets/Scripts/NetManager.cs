using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prototype.NetworkLobby;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.UI;

public class NetManager :  LobbyManager{

	/*----------------------------------------------------------------------------------------------------------------
	----------------------------------------------------- MENU  ------------------------------------------------------
	----------------------------------------------------------------------------------------------------------------*/
	[Header("UI Settings")]
	public GameObject settingsPanel;
	public Dropdown qualityDropdown;
	public Slider volumeSlider;
    
    

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
	{
		var spawnLocations = GameObject.FindObjectsOfType<NetworkStartPosition>().ToList();
		var playerSpawns = spawnLocations.FindAll(x => x.gameObject.CompareTag("playerSpawn")).ToList();
		Transform spawnLocation = playerSpawns[Random.Range(0, playerSpawns.Count)].transform;
		GameObject go = Instantiate(gamePlayerPrefab, spawnLocation.position, spawnLocation.rotation) as GameObject;
		LobbyPlayer player = lobbySlots[conn.connectionId].gameObject.GetComponent<LobbyPlayer>();
		NetworkedFirstPersonController pl = go.GetComponent<NetworkedFirstPersonController>();
		pl.name = player.playerName;
		pl.playerName = player.playerName;
		pl.playerColour = player.playerColor;
		return go;
	}

	public void ShowSettings(bool show)
	{
		settingsPanel.SetActive(show);
	}

	public void ApplyQualitySettings()
	{
		QualitySettings.SetQualityLevel(qualityDropdown.value);
		PlayerPrefs.SetFloat("Setting.Volume", volumeSlider.value);
		ShowSettings(false);
	}


}