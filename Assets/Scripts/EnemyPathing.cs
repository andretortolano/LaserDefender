using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig = null;
    GameObject path = null; // REMOVE
    [SerializeField] float moveSpeed = 2f;

    List<Transform> pathWaypoints = new List<Transform>();

    int waypointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        pathWaypoints = waveConfig.getWayPoints();
        //foreach (Transform t in path.GetComponentInChildren<Transform>()) {
        //    pathWaypoints.Add(t);
        //}

        transform.position = pathWaypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= pathWaypoints.Count - 1)
        {
            var targetPosition = pathWaypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
