using UnityEngine;
using System.Collections;
using Prototype.NetworkLobby;
using UnityEngine.Networking;
using System.Linq;

public class GameManager :  LobbyManager{

	public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
	{
		var spawnLocations = GameObject.FindObjectsOfType<NetworkStartPosition>().ToList();
		Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)].transform;
		GameObject go = Instantiate(gamePlayerPrefab, spawnLocation.position, spawnLocation.rotation) as GameObject;
		LobbyPlayer player = lobbySlots[conn.connectionId].gameObject.GetComponent<LobbyPlayer>();
		NetworkedFirstPersonController pl = go.GetComponent<NetworkedFirstPersonController>();
		pl.name = player.playerName;
		pl.playerName = player.playerName;
		pl.playerColour = player.playerColor;
		return go;
	}

}
