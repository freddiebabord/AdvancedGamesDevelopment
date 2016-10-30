using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public enum DrawFrustum { None, Peaceful, Aggravated, Both }
public enum PlayerState { None = 0, Visible, Shooting };

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
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]

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
    public FrustumFramework aggro = new FrustumFramework();
    #endregion

    public GhostState ghostState = GhostState.Peaceful;
    public DrawFrustum drawFrustum = DrawFrustum.None;
    public bool isTriggered;
    public GameObject focus;
    public float attentionSpan = 5f;
    [SyncVar] public NetworkInstanceId parentNetID;

    Dictionary<int,PlayerPriority> interactablePlayers = new Dictionary<int,PlayerPriority>();
    float attentionSpanStart;
    NetworkIdentity otherNetID;
    NetworkIdentity rootNetID;

    #region Mesh Variables
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    Mesh peacefulMesh;
    Mesh aggroMesh;
    #endregion

    public override void OnStartClient()
    {
        GameObject parentObj = ClientScene.FindLocalObject(parentNetID);
        transform.SetParent(parentObj.transform.GetChild(0));
        PostStart();
    }

    public void PostStart()
    {
        rootNetID = transform.parent.parent.GetComponent<NetworkIdentity>();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        if (!rootNetID.isServer)
        {
            print("Apparently I'm Local Player...");
            return;
        }
        if (rootNetID.isServer)
        {
            Rpc_SetMeshes(peaceful, aggro);
            Rpc_ChangeFrustum(ghostState);
        }
    }

    // Update is called once per frame
	void Update ()
    {
        if (drawFrustum != DrawFrustum.None)
        {
            if (!Application.isPlaying)
            {
                meshFilter = GetComponent<MeshFilter>();
                meshCollider = GetComponent<MeshCollider>();
            }
            Rpc_SetMeshes(peaceful,aggro);
        }
        else
        {
            if ((Time.time - attentionSpanStart > attentionSpan || focus == null) && isLocalPlayer)
            {
                ChoosePlayer();
            }

            if (isTriggered && focus != null)
            {
                transform.LookAt(focus.transform.position);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        print("<color=green>" + other.tag + "</color>");
        if (other.tag == "Player")
        {
            RaycastHit hit;
            Vector3 relativePlayerPos = transform.position - other.transform.position;

            Debug.DrawRay(other.transform.position + other.transform.TransformDirection(Vector3.back), relativePlayerPos, Color.green);

            if (Physics.Raycast(other.transform.position + other.transform.TransformDirection(Vector3.back), relativePlayerPos, out hit))
            {
                int netID = otherNetID.isServer ? otherNetID.connectionToClient.connectionId : otherNetID.connectionToServer.connectionId;
                Rpc_PlayerEnterView(netID, other.gameObject);
                ChoosePlayer();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;
            Vector3 relativePlayerPos = transform.position - other.transform.position;

            Debug.DrawRay(other.transform.position + other.transform.TransformDirection(Vector3.back), relativePlayerPos, Color.green);

            if (Physics.Raycast(other.transform.position + other.transform.TransformDirection(Vector3.back), relativePlayerPos, out hit))
            {
                int netID = otherNetID.isServer ? otherNetID.connectionToClient.connectionId : otherNetID.connectionToServer.connectionId;
                Rpc_PlayerLeftView(netID, other.gameObject);
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

            Gizmos.DrawWireMesh(aggroMesh, transform.position, transform.rotation);
        }
    }

    [ClientRpc]
    void Rpc_SetMeshes(FrustumFramework peaceful, FrustumFramework aggro)
    {
        print("<color=yellow>SetMeshes Triggered!</color>");
        peacefulMesh = new Mesh();
        aggroMesh = new Mesh();

        int[] triangles = new int[36]
        {
            2, 3, 1, 0, 2, 1, // Front
            6, 2, 0, 4, 6, 0, // Left
            7, 6, 4, 5, 7, 4, // Back
            3, 7, 5, 1, 3, 5, // Right
            3, 2, 6, 7, 3, 6, // Top
            0, 1, 5, 4, 0, 5  // Bottom
        };

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

        aggroMesh.vertices = new Vector3[8]
        {
            new Vector3(-(aggro.near.x / 2), -(aggro.near.y / 2), 0),
            new Vector3((aggro.near.x / 2), -(aggro.near.y / 2), 0),
            new Vector3(-(aggro.near.x / 2), (aggro.near.y / 2), 0),
            new Vector3((aggro.near.x / 2), (aggro.near.y / 2), 0),
            new Vector3(-(aggro.far.x / 2), -(aggro.far.y / 2), aggro.distance),
            new Vector3((aggro.far.x / 2), -(aggro.far.y / 2), aggro.distance),
            new Vector3(-(aggro.far.x / 2), (aggro.far.y / 2), aggro.distance),
            new Vector3((aggro.far.x / 2), (aggro.far.y / 2), aggro.distance)
        };
        aggroMesh.triangles = triangles;
        aggroMesh.RecalculateNormals();
    }

    public void ChoosePlayer()
    {
        int rangeMax = 0;
        // Sort the list based on the result of CalculatePriority
        List<PlayerPriority> playerList = interactablePlayers.Values.OrderBy(x=>x.CalculatePriority()).ToList();
        print(playerList);
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
    public void Rpc_ChangeFrustum(GhostState curGhostState)
    {
        if (curGhostState == GhostState.Peaceful)
        {
            meshFilter.mesh = peacefulMesh;
            meshCollider.sharedMesh = peacefulMesh;
        }
        else if (curGhostState == GhostState.Aggravated)
        {
            meshFilter.mesh = aggroMesh;
            meshCollider.sharedMesh = aggroMesh;
        }

        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }

    [ClientRpc]
    public void Rpc_PlayerEnterView(int networkID, GameObject player)
    {
        while (interactablePlayers.Count - 1 <= networkID)
            interactablePlayers.Add(networkID,new PlayerPriority());
        if (interactablePlayers[networkID].state == PlayerState.None) 
            interactablePlayers[networkID].state = PlayerState.Visible;
        ghostState = GhostState.Aggravated;

        if (focus == null)
        {
            focus = player;
            attentionSpanStart = Time.time;
        }
    }

    [ClientRpc]
    public void Rpc_PlayerLeftView(int networkID, GameObject player)
    {
        if (interactablePlayers[networkID].state == PlayerState.Visible)
            interactablePlayers[networkID].state = PlayerState.None;
        if (player == focus)
            focus = null;
    }

    [ClientRpc]
    public void Rpc_ChoosePlayer(PlayerPriority player)
    {
        //focus = 
    }
}
