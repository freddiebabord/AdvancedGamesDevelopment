using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public enum DrawFrustum { None, Peaceful, Aggravated, Both }
public enum PlayerState { None = 0, Visible, Shooting }

[Serializable]
public struct FrustumFramework
{
    public Vector2 near;
    public Vector2 far;
    public float distance;
}

public class PlayerPriority
{
    public int CalculatePriority()
    {
        priority = (int)state + distanceADJ;
        return priority;
    }

    public PlayerState state = PlayerState.None;
    public int distanceADJ = 0;
    public int priority = 0;
    public int firstIndex = 0;
}

[ExecuteInEditMode]
public class Frustum : NetworkBehaviour {
    
    public enum GhostState {Peaceful = 0, Aggravated};

    #region Frustum Inspector
    public bool ShowView
    {
        get { return showView; }
        set { showView = value; }
    }
    public bool IsPeaceful
    {
        get { return isPeaceful; }
        set { isPeaceful = value; }
    }
    public bool IsAggravated
    {
        get { return isAggravated; }
        set { isAggravated = value; }
    }
    [SerializeField]
    bool showView = false;
    [SerializeField]
    bool isPeaceful = false;
    [SerializeField]
    bool isAggravated = false;
    #endregion

    #region Frustum Variables
    public FrustumFramework peaceful = new FrustumFramework();
    //public FrustumFramework aggro = new FrustumFramework();
    #endregion

    public GhostState ghostState = GhostState.Peaceful;
    public DrawFrustum drawFrustum = DrawFrustum.None;
    public bool isTriggered;
    public Transform focus;
    public float attentionSpan = 5f;
    public NetworkInstanceId parentNetID;

    Dictionary<int,PlayerPriority> interactablePlayers = new Dictionary<int,PlayerPriority>();
    float attentionSpanStart;
    NetworkIdentity otherNetID;
    NetworkIdentity rootNetID;
    //private GameObject root;

    #region Mesh Variables
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    Mesh peacefulMesh;
    //Mesh //aggroMesh;
    #endregion


    public void PostStart()
    {
        rootNetID = GetComponent<NetworkIdentity>();
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshCollider = GetComponentInChildren<MeshCollider>();
        peacefulMesh = new Mesh();
		charge = GetComponent<Charge> ();
        //aggroMesh = new Mesh();
        if (!rootNetID.isServer)
        {
            print("Apparently I'm Local Player...");
            return;
        }
        if (rootNetID.isServer)
        {
            //transform.parent.GetComponentInParent<EnemyBase>().ghostFrustum = this;
            Rpc_SetMeshes(peaceful);
            Rpc_ChangeFrustum((int)ghostState);
			//root = transform.gameObject;
        }
    }

    // Update is called once per frame
	void Update ()
    {
        if (drawFrustum != DrawFrustum.None)
        {
            /*if (!Application.isPlaying)
            {
                meshFilter = GetComponent<MeshFilter>();
                meshCollider = GetComponent<MeshCollider>();
            }*/
            Rpc_SetMeshes(peaceful);
        }
        else
        {
            if ((Time.time - attentionSpanStart > attentionSpan || focus == null) && isLocalPlayer)
            {
                ChoosePlayer();
            }
        }

        if (isTriggered)
        {
            if (charge)
                charge.Triggered(focus.position);
            //if (root.GetComponent<Teleportation>())
            //root.GetComponent<Teleportation>().Triggered(target.position);
        }

    }

	private Charge charge;
	private Vector3 relativePlayerPos;
	private int netID;

    public void OnTriggerEnter(Collider other)
    {
        //print("<color=green>" + other.tag + "</color>");
        if (other.tag == "Player")
        {
            relativePlayerPos = transform.position - other.transform.position;
            //otherNetID = other.GetComponent<NetworkIdentity>();
            Debug.DrawRay(other.transform.position, relativePlayerPos, Color.red);

            if (Physics.Raycast(other.transform.position, relativePlayerPos))
            {
				netID = other.GetComponent<NetworkedThirdPersonCharacter> ().playerID;// otherNetID.isServer ? otherNetID.connectionToClient.connectionId : otherNetID.connectionToServer.connectionId;
                Rpc_PlayerEnterView(netID, other.gameObject);
                ChoosePlayer();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            relativePlayerPos = transform.position - other.transform.position;
            //otherNetID = other.GetComponent<NetworkIdentity>();
            Debug.DrawRay(other.transform.position, relativePlayerPos, Color.red);

            if (Physics.Raycast(other.transform.position, relativePlayerPos))
            {
				netID = other.GetComponent<NetworkedThirdPersonCharacter> ().playerID; //otherNetID.isServer ? otherNetID.connectionToClient.connectionId : otherNetID.connectionToServer.connectionId;
                Rpc_PlayerLeftView(netID, other.gameObject);
                ChoosePlayer();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (drawFrustum == DrawFrustum.Peaceful || drawFrustum == DrawFrustum.Both)
        {
            if (ghostState == GhostState.Peaceful)
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.cyan;

            Gizmos.DrawWireMesh(peacefulMesh, transform.position, transform.rotation);
        }
        if (drawFrustum == DrawFrustum.Aggravated || drawFrustum == DrawFrustum.Both)
        {
            if (ghostState == GhostState.Aggravated)
                Gizmos.color = Color.green;
            else
                Gizmos.color = Color.red;

            //Gizmos.DrawWireMesh(//aggroMesh, transform.position, transform.rotation);
        }
    }

	int[] triangles = new int[36]
	{
		2, 3, 1, 0, 2, 1, // Front
		6, 2, 0, 4, 6, 0, // Left
		7, 6, 4, 5, 7, 4, // Back
		3, 7, 5, 1, 3, 5, // Right
		3, 2, 6, 7, 3, 6, // Top
		0, 1, 5, 4, 0, 5  // Bottom
	};

    [ClientRpc]
    void Rpc_SetMeshes(FrustumFramework peaceful/*, FrustumFramework aggro*/)
    {
       // print("<color=yellow>SetMeshes Triggered!</color>");

        
        peacefulMesh.vertices = new Vector3[8]
        {
            new Vector3(-(peaceful.near.x / 2), -(peaceful.near.y / 2), 0),
            new Vector3((peaceful.near.x / 2), -(peaceful.near.y / 2), 0),
            new Vector3(-(peaceful.near.x / 2), (peaceful.near.y / 2), 0),
            new Vector3((peaceful.near.x / 2), (peaceful.near.y / 2), 0),
            new Vector3(-(peaceful.far.x / 2), -(peaceful.far.y / 2), peaceful.distance),
            new Vector3((peaceful.far.x / 2), -(peaceful.far.y / 2), peaceful.distance),
            new Vector3(-(peaceful.far.x / 2), (peaceful.far.y / 2), peaceful.distance),
            new Vector3((peaceful.far.x / 2), (peaceful.far.y / 2), peaceful.distance)
        };
        peacefulMesh.triangles = triangles;
        peacefulMesh.RecalculateNormals();

        //aggroMesh.vertices = new Vector3[8]
       /* {
            new Vector3(-(aggro.near.x / 2), -(aggro.near.y / 2), 0),
            new Vector3((aggro.near.x / 2), -(aggro.near.y / 2), 0),
            new Vector3(-(aggro.near.x / 2), (aggro.near.y / 2), 0),
            new Vector3((aggro.near.x / 2), (aggro.near.y / 2), 0),
            new Vector3(-(aggro.far.x / 2), -(aggro.far.y / 2), aggro.distance),
            new Vector3((aggro.far.x / 2), -(aggro.far.y / 2), aggro.distance),
            new Vector3(-(aggro.far.x / 2), (aggro.far.y / 2), aggro.distance),
            new Vector3((aggro.far.x / 2), (aggro.far.y / 2), aggro.distance)
        };*/
        //aggroMesh.triangles = triangles;
        //aggroMesh.RecalculateNormals();
    }

    public void ChoosePlayer()
    {
        int rangeMax = 0;
        // Sort the list based on the result of CalculatePriority
        //List<PlayerPriority> playerList = interactablePlayers.Values.OrderBy(x=>x.CalculatePriority()).ToList();
        //print(playerList);
        for (int i = 0; i < interactablePlayers.Count; i++)
        {
            rangeMax += interactablePlayers[i].priority;
            interactablePlayers[i].firstIndex = i > 0 ? interactablePlayers[i - 1].firstIndex + (interactablePlayers[i - 1].priority - 1) : 0;
        }
        int chosenPlayer = UnityEngine.Random.Range(0, rangeMax - 1);
        for (int i = 0; i < interactablePlayers.Count; i++)
        {
            if (i < interactablePlayers.Count - 1)
            {
                if (chosenPlayer >= interactablePlayers[i].firstIndex &&
                    chosenPlayer < interactablePlayers[i + 1].firstIndex)
                {
                    Rpc_ChoosePlayer(interactablePlayers[i]);
                }
            }
            else if (i == interactablePlayers.Count - 1)
            {
                Rpc_ChoosePlayer(interactablePlayers[i]);
            }
        }
    }

    [ClientRpc]
    public void Rpc_ChangeFrustum(int ghostStateInt)
    {
        GhostState curGhostState = (GhostState)ghostStateInt;
       // print("Hello");
        if (curGhostState == GhostState.Peaceful)
        {
            meshFilter.mesh = peacefulMesh;
            meshCollider.sharedMesh = peacefulMesh;
        }
       /* else if (curGhostState == GhostState.Aggravated)
        {
            meshFilter.mesh = //aggroMesh;
            meshCollider.sharedMesh = //aggroMesh;
        }*/

        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }

    [ClientRpc]
    public void Rpc_PlayerEnterView(int networkID, GameObject player)
    {
        if (!interactablePlayers.ContainsKey(networkID))
            interactablePlayers.Add(networkID, new PlayerPriority());   
        if (interactablePlayers[networkID].state == PlayerState.None) 
            interactablePlayers[networkID].state = PlayerState.Visible;
        ghostState = GhostState.Aggravated;

        if (focus == null)
        {
            focus = player.transform;
            isTriggered = true;
            attentionSpanStart = Time.time;
        }
    }

    [ClientRpc]
    public void Rpc_PlayerLeftView(int networkID, GameObject player)
    {
        if (interactablePlayers[networkID].state == PlayerState.Visible)
            interactablePlayers[networkID].state = PlayerState.None;
        if (player == focus)
            isTriggered = false;
            focus = null;
    }

    [ClientRpc]
    public void Rpc_ChoosePlayer(PlayerPriority player)
    {
        //focus = 
    }
}
