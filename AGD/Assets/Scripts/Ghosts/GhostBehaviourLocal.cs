using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class GhostBehaviourLocal : MonoBehaviour
{
    public float maxHealth = 100f;
    public float movementSpeed = 5f;
    public float fearPresence = 10f;
    public float score = 100f;
    public Transform ghostTarget;
    [Tooltip("None: Ghost is always Aggravated.\nVisible: Ghost will attack on sight or when attacked.\nAttacking: Ghost will retaliate when attacked.")]
    public PlayerState ghostTrigger;
    [Tooltip("True: The Ghost will never attack.\nFalse: The Ghost can attack.")]
    public bool isPeaceful = false;
    EnemyBase enmBase;
}

