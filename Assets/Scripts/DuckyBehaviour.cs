using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.XR.CoreUtils;

public class DuckyBehaviour : MonoBehaviour
{
    [Header("Refs")]
    GameManager gameManager;
    Transform player;
    public GameObject positions;
    [Tooltip("Collider used to check if next jump position is in camera frustum.")]
    public Collider testCollider;

    [Header("Variables")] 
    Transform[] movePositions;
    Dictionary<Transform, Transform[]> neighboringPositions;
    Transform currentTransform;
    public float AI_decisionInterval;
    public float AI_moveOdds;
    public float AI_followPlayerOdds;
    Plane[] cameraPlanes;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        player = GameObject.FindObjectOfType<XROrigin>().transform;

        // pre-process move Positions
        neighboringPositions = new Dictionary<Transform, Transform[]>();
        movePositions = positions.GetComponentsInChildren<Transform>();
        // Remove first element of array, since it's the transform of the gameobject holding the children transforms.
        movePositions = movePositions.Skip(1).ToArray();
        SetNewTransform(movePositions[0], true);

        foreach (Transform referenceMovePosition in movePositions)
        {
            List<Transform> neighbors = new List<Transform>();
            foreach (Transform movePosition in movePositions)
            {
                if (!movePosition.Equals(referenceMovePosition) && (movePosition.position - referenceMovePosition.position).sqrMagnitude < 16f)
                    neighbors.Add(movePosition);
            }
            neighboringPositions.Add(referenceMovePosition, neighbors.ToArray());
        }

        // === DEBUG ===
        /*
        foreach (KeyValuePair<Transform, Transform[]> kvp in neighboringPositions)
        {
            Debug.Log($"{kvp.Key.position} has {kvp.Value.Length} neighbors.");
            foreach (Transform neighbor in kvp.Value)
                Debug.DrawLine(kvp.Key.position, neighbor.position, Color.yellow);
        }
        */
        // === END DEBUG ===
    }

    float timer;
    // Update is called once per frame
    void Update()
    {
        if (gameManager.level1States.HasFlag(GameManager.Level1States.ServerRoomDoorUnlocked))
        {
            // Move
            timer += Time.deltaTime;
            if (timer > AI_decisionInterval) // Decision interval
            {
                if (Random.value < AI_moveOdds && !CheckIfInCameraFrustum(currentTransform))
                {
                    Transform newMove = ChooseNewValidPosition();
                    if (!CheckIfInCameraFrustum(newMove))
                        SetNewTransform(newMove, true);
                }
                timer = 0f;
            }
        }
    }

    Transform bestCandidate;
    Transform ChooseNewValidPosition()
    {
        bestCandidate = currentTransform;

        Transform[] possiblePositions = neighboringPositions[currentTransform];

        if (Random.value < AI_followPlayerOdds)
        {
            float bestDistanceToPlayer = 999f;

            foreach (Transform possiblePosition in possiblePositions)
            {
                float distanceToPlayer = (possiblePosition.position - player.position).sqrMagnitude;
                if (distanceToPlayer < bestDistanceToPlayer)
                {
                    bestCandidate = possiblePosition;
                    bestDistanceToPlayer = distanceToPlayer;
                }
            }
            return bestCandidate;
        } else { // Choose at random
            return possiblePositions[Random.Range(0, possiblePositions.Length)];
        }
    }

    void SetNewTransform(Transform newTransform, bool alsoSetRotation = false)
    {
        currentTransform = newTransform;
        transform.position = newTransform.position;
        if (alsoSetRotation)
            transform.rotation = newTransform.rotation;
    }

    bool CheckIfInCameraFrustum(Transform testPoint)
    {
        testCollider.transform.position = testPoint.position;
        cameraPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        // Debug.Log($"Position {testPoint.position} {(GeometryUtility.TestPlanesAABB(cameraPlanes, testCollider.bounds)?"is":"is not")} in camera view frustum.");
        return GeometryUtility.TestPlanesAABB(cameraPlanes, testCollider.bounds);
    }
}
