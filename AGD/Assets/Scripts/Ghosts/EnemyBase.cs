using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel=1, sendInterval=0.2f)]
public class EnemyBase : NetworkBehaviour {

    public float maxFloatHeight = 50f;

    public NavMeshAgent agent;
	[SerializeField] private float walkRadius = 10;
	[SerializeField] public Vector3 target = Vector3.zero;
    //GhostBodyAdjustments ghostPosition;
	GhostBehaviour ghostBehaviour;

	void Awake()
	{
		//agent = GetComponent<NavMeshAgent>();
		//ghostPosition = GetComponentInChildren<GhostBodyAdjustments>();
		ghostBehaviour = GetComponent<GhostBehaviour> ();
	}

	Vector3 randomDirection;
	NavMeshHit hit;
	float randY;



	//Set the target of the ai agent across the network
	[ClientRpc]
	public void RpcSetPosition(Vector3 position)
	{
		target = position;
      //  if(agent.isOnNavMesh)
      // // Vector3.Slerp(transform.position, position, );
		    //agent.SetDestination(target);
      //  else
      //      GetComponent<GhostBehaviour>().Kill();
	}

	//Draw the agents path towards the target
	void OnDrawGizmos()
	{
        //if (Application.isPlaying)
        //{
        //    Gizmos.color = Color.cyan;
        //    Gizmos.DrawWireSphere(target, 0.5f);
        //    if (!agent.hasPath)
        //        return;
        //    Gizmos.DrawLine(transform.position, agent.path.corners[0]);
        //    for (int i = 0; i < agent.path.corners.Length - 1; i++)
        //    {
        //        Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
        //    }
        //}
	}
}
