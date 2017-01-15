using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Room : MonoBehaviour {

	private List<Collider> colliders = new List<Collider>();
	private List<EnemyBase> enemies = new List<EnemyBase>();
	private List<NetworkedThirdPersonCharacter> players = new List<NetworkedThirdPersonCharacter>();
	public int enemyCount { get{ return enemies.Count; } }
	public int playerCount { get{ return players.Count; } }
	private List<Collider> doors = new List<Collider>();
    public List<Collider> Doors { get { return doors; } }
	private List<ReflectionProbe> roomReflectionProbes = new List<ReflectionProbe> ();

	// Use this for initialization
	void Start () {
		colliders = GetComponentsInChildren<Collider>().ToList();
		doors = colliders.FindAll(x => x.gameObject.tag == "Door").ToList();
		roomReflectionProbes = GetComponentsInChildren<ReflectionProbe> ().ToList ();
	}

 //   void FixedUpdate()
 //   {
 //       if (playerCount < enemyCount)
 //       {
 //           for (int i = 0; i < playerCount; ++i)
 //           {
 //               players[i].fearLevel += (playerCount / enemyCount) * Time.deltaTime;
 //               players[i].frost.FrostAmount = players[i].fearLevel / players[i].maxFearLevel;
 //           }
 //       }
 //       else if(playerCount >= enemyCount)
 //       {
 //           for (int i = 0; i < playerCount; ++i)
 //           {
 //               if (players[i].fearLevel > 0)
 //               {
 //                   players[i].fearLevel -= (playerCount / (enemyCount == 0 ? 1 : enemyCount)) * Time.deltaTime;
 //                   players[i].frost.FrostAmount = players[i].fearLevel / players[i].maxFearLevel;

 //               }
 //           }
 //       }
 //   }

	//public void OnTriggerEnter(Collider other)
	//{
	//	if(other.GetComponent<EnemyBase>())
	//	{
	//		if(!enemies.Contains(other.GetComponent<EnemyBase>()))
	//			enemies.Add(other.GetComponent<EnemyBase>());
	//	}
	//	else if(other.GetComponent<NetworkedThirdPersonCharacter>())
	//	{
	//		if(!players.Contains(other.GetComponent<NetworkedThirdPersonCharacter>()))
	//			players.Add(other.GetComponent<NetworkedThirdPersonCharacter>());
	//	}
	//}

	//public void OnTriggerStay(Collider other)
	//{
	//    if (other.gameObject.CompareTag("Player") && !updatingProbes)
	//    {
	//        StartCoroutine(UpdatingReflectionProbe());
	//    }
	//}

	//public void OnTriggerExit(Collider other)
	//{
	//	if(other.GetComponent<EnemyBase>())
	//	{
	//		enemies.Remove(other.GetComponent<EnemyBase>());
	//		enemies.TrimExcess();
	//	}
	//	else if(other.GetComponent<NetworkedThirdPersonCharacter>())
	//	{
	//		players.Remove(other.GetComponent<NetworkedThirdPersonCharacter>());
	//	}
	//}

	//private bool updatingProbes = false;
	//private IEnumerator UpdatingReflectionProbe()
	//{
	//	updatingProbes = true;
	//	yield return null;
	//	for (int i = 0; i < roomReflectionProbes.Count; ++i) {
	//		roomReflectionProbes [i].RenderProbe ();		
	//	}
	//	updatingProbes = false;
	//}
}
