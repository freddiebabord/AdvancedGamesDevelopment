using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class LocalFrustum : MonoBehaviour {

    public enum GhostState { Peaceful = 0, Aggravated }

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
    public Transform target;
    public float attentionSpan = 5f;
    public GhostBehaviourLocal behaviour;

    float attentionSpanStart;
    PlayerPriority playerPriority;
    GameObject root;

    #region Mesh Variables
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    Mesh peacefulMesh;
    //Mesh aggroMesh;
    #endregion

    // Use this for initialization
    void Start ()
    {
        root = transform.parent.parent.gameObject;
        behaviour = root.GetComponent<GhostBehaviourLocal>();
        playerPriority = new PlayerPriority();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        peacefulMesh = new Mesh();
        SetMeshes();
        ChangeFrustum();
    }

    void Update()
    {
        if (isTriggered)
        {
            if (root.GetComponent<Charge>())
                root.GetComponent<Charge>().Triggered(target.position);
            //if (root.GetComponent<Teleportation>())
                //root.GetComponent<Teleportation>().Triggered(target.position);
        }
    }

    void SetMeshes()
    {
        print("<color=yellow>SetMeshes Triggered!</color>");

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

        //aggroMesh.vertices = new Vector3[8]
        //{
        //    new Vector3(-(aggro.near.x / 2), -(aggro.near.y / 2), 0),
        //    new Vector3((aggro.near.x / 2), -(aggro.near.y / 2), 0),
        //    new Vector3(-(aggro.near.x / 2), (aggro.near.y / 2), 0),
        //    new Vector3((aggro.near.x / 2), (aggro.near.y / 2), 0),
        //    new Vector3(-(aggro.far.x / 2), -(aggro.far.y / 2), aggro.distance),
        //    new Vector3((aggro.far.x / 2), -(aggro.far.y / 2), aggro.distance),
        //    new Vector3(-(aggro.far.x / 2), (aggro.far.y / 2), aggro.distance),
        //    new Vector3((aggro.far.x / 2), (aggro.far.y / 2), aggro.distance)
        //};
        //aggroMesh.triangles = triangles;
        //aggroMesh.RecalculateNormals();
    }

    public void ChangeFrustum()
    {
        meshFilter.mesh = peacefulMesh;
        meshCollider.sharedMesh = peacefulMesh;
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        print("<color=green>" + other.tag + "</color>");
        if (other.tag == "Player")
        {
            RaycastHit hit;
            Vector3 relativePlayerPos = transform.position - other.transform.position;

            Debug.DrawRay(other.transform.position, relativePlayerPos, Color.red);

            if (Physics.Raycast(other.transform.position, relativePlayerPos, out hit))
                PlayerEnterView(other.transform);
        }
    }

    void PlayerEnterView(Transform player)
    {
        if (playerPriority.state == PlayerState.None)
            playerPriority.state = PlayerState.Visible;
        ghostState = GhostState.Aggravated;

        if (target == null && !isTriggered && playerPriority.state == behaviour.ghostTrigger)
        {
            target = player;
            isTriggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;
            Vector3 relativePlayerPos = transform.position - other.transform.position;

            Debug.DrawRay(other.transform.position + other.transform.TransformDirection(Vector3.forward), relativePlayerPos, Color.green);

            if (Physics.Raycast(other.transform.position, relativePlayerPos, out hit))
                PlayerLeftView(other.transform);
        }
    }

    void PlayerLeftView(Transform player)
    {
        if (playerPriority.state == PlayerState.Visible)
            playerPriority.state = PlayerState.None;
        ghostState = GhostState.Peaceful;
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

            //Gizmos.DrawWireMesh(aggroMesh, transform.position, transform.rotation);
        }
    }
}
