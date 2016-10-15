using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyBase : NetworkBehaviour {

	public bool isHigher = false;
	public bool isLower = false;
	public bool isSame = false;
	public bool firstPass = false;

	private NavMeshAgent agent;
	[SerializeField] private float walkRadius = 10;
	[SyncVar][SerializeField] public Vector3 target = Vector3.zero;


	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame on the server
	void Update () {
		if(!isServer)
			return;
		
		// TODO: Ad actual logic
		//Currently gets a random point on the navmesh
		if(!agent.hasPath)
		{
			Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
			randomDirection += transform.position;
			NavMeshHit hit;
			NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
			Vector3 finalPosition = hit.position;
			target = finalPosition;
			RpcSetPosition(target);
		}
	}

	//Set the target of the ai agent across the network
	[ClientRpc]
	public void RpcSetPosition(Vector3 position)
	{
		target = position;
		agent.SetDestination(target);
	}

	//Draw the agents path towards the target
	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(target, 0.5f);
		Gizmos.DrawLine(transform.position, agent.path.corners[0]);
		for (int i = 0; i < agent.path.corners.Length - 1; i++)
		{
			Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
		}
	}
}
