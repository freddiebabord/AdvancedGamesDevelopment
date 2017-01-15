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

[ExecuteInEditMode]
public class Frustum : NetworkBehaviour
{

    public enum GhostState { Peaceful = 0, Aggravated };

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
    GhostBehaviour ghostBehaviour;

    //float attentionSpanStart;
    //private GameObject root;

    #region Mesh Variables
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    //Mesh peacefulMesh;
    //Mesh //aggroMesh;
    #endregion

    void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshCollider = GetComponentInChildren<MeshCollider>();
        meshFilter.mesh = new Mesh();
        meshCollider.sharedMesh = meshFilter.sharedMesh;
    }

    public void PostStart()
    {
        ghostBehaviour = GetComponent<GhostBehaviour>();
        if (isServer)
        {
            //transform.parent.GetComponentInParent<EnemyBase>().ghostFrustum = this;
            Rpc_SetMeshes(peaceful);
            Rpc_ChangeFrustum((int)ghostState);
            //root = transform.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;
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
           // if ((Time.time - attentionSpanStart > attentionSpan || focus == null) && isLocalPlayer)
           // {
            //    ChoosePlayer();
            //}
        }

        if (isTriggered)
        {
            if (GetComponent<GhostCharge>() && ghostBehaviour.ghostTarget)
                GetComponent<GhostCharge>().Triggered(ghostBehaviour.ghostTarget.GetComponent<CapsuleCollider>().bounds.center);
            else if (GetComponent<GhostSwell>())
                GetComponent<GhostSwell>().Triggered();
			else if (GetComponent<GhostThrow>())
				GetComponent<GhostThrow>().Triggered();
        }

    }

    private Vector3 relativePlayerPos;
    private int netID;

    public void OnTriggerEnter(Collider other)
    {

        if (!isServer)
            return;

        //print("<color=green>" + other.tag + "</color>");
        if (other.tag == "Player")
        {
            relativePlayerPos = transform.position - other.transform.position;
            //otherNetID = other.GetComponent<NetworkIdentity>();
            Debug.DrawRay(other.transform.position, relativePlayerPos, Color.red);

            if (Physics.Raycast(other.transform.position, relativePlayerPos))
            {
                netID = other.GetComponent<NetworkedThirdPersonCharacter>().playerID;// otherNetID.isServer ? otherNetID.connectionToClient.connectionId : otherNetID.connectionToServer.connectionId;
                Rpc_PlayerEnterView(other.gameObject);
                //ChoosePlayer();
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

            Gizmos.DrawWireMesh(meshFilter.mesh, transform.position, transform.rotation);
        }
        //if (drawFrustum == DrawFrustum.Aggravated || drawFrustum == DrawFrustum.Both)
        //{
        //    if (ghostState == GhostState.Aggravated)
        //        Gizmos.color = Color.green;
        //    else
        //        Gizmos.color = Color.red;

        //    //Gizmos.DrawWireMesh(//aggroMesh, transform.position, transform.rotation);
        //}
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

        if (meshFilter.mesh == null)
            meshFilter.mesh = new Mesh();
        meshFilter.mesh.vertices = new Vector3[8]
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
        meshFilter.mesh.triangles = triangles;
        meshFilter.mesh.RecalculateNormals();

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

    [ClientRpc]
    public void Rpc_ChangeFrustum(int ghostStateInt)
    {
        GhostState curGhostState = (GhostState)ghostStateInt;
        // print("Hello");
        //        if (curGhostState == GhostState.Peaceful)
        //        {
        //            meshFilter.mesh = peacefulMesh;
        //            meshCollider.sharedMesh = peacefulMesh;
        //        }
        /* else if (curGhostState == GhostState.Aggravated)
         {
             meshFilter.mesh = //aggroMesh;
             meshCollider.sharedMesh = //aggroMesh;
         }*/

        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }

    [ClientRpc]
    public void Rpc_PlayerEnterView(GameObject player)
    {
        if (ghostBehaviour.ghostTarget == null)
        {
            ghostBehaviour.ghostTarget = player.transform;
            isTriggered = true;
        }
    }

    [ClientRpc]
    public void Rpc_ChoosePlayer(PlayerData player)
    {
        //focus = 
    }
}