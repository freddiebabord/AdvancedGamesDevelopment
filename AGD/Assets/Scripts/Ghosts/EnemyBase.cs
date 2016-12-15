using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyBase : NetworkBehaviour {

    public enum EnemyMap { Higher, Lower, Same, Nullus };
    public EnemyMap enemyMap = EnemyMap.Nullus;
    public bool firstPass = false;
    public float maxFloatHeight = 50f;

    public NavMeshAgent agent;
	[SerializeField] private float walkRadius = 10;
	[SyncVar][SerializeField] public Vector3 target = Vector3.zero;
    GhostBodyAdjustments ghostPosition;


	void Start () {
		agent = GetComponent<NavMeshAgent>();
        ghostPosition = GetComponentInChildren<GhostBodyAdjustments>();
        for(int i = 0; i < GameManager.instance.RadarHelper.Count; ++i)
            GameManager.instance.RadarHelper[i].RegisterEnemy(this);
	}

    void OnDestroy()
    {
        for(int i = 0; i < GameManager.instance.RadarHelper.Count; ++i)
            GameManager.instance.RadarHelper[i].DeregisterEnemy(this);
    }
	
	// Update is called once per frame on the server
	void Update () {
		if(!isServer)
			return;


        //Currently gets a random point on the navmesh
        if (!agent.hasPath)
        {
            Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
            Vector3 finalPosition = hit.position;
            float randY = Random.Range(0, maxFloatHeight);
            target = new Vector3(finalPosition.x, randY, finalPosition.z);
            RpcSetPosition(target);
        }
        
	}

	//Set the target of the ai agent across the network
	[ClientRpc]
	public void RpcSetPosition(Vector3 position)
	{
		target = position;
        if(agent.isOnNavMesh)
       // Vector3.Slerp(transform.position, position, );
		    agent.SetDestination(target);
        else
            GetComponent<GhostBehaviour>().Kill();
	}

	//Draw the agents path towards the target
	void OnDrawGizmos()
	{
        if (Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(target, 0.5f);
            if (!agent.hasPath)
                return;
            Gizmos.DrawLine(transform.position, agent.path.corners[0]);
            for (int i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
            }
        }
	}
}
