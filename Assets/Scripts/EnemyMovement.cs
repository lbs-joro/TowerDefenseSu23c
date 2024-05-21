using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<GameObject> waypoints;

    int wayPointIndex = 0;

    Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = waypoints[wayPointIndex].transform.position - transform.position;
        rb.velocity = direction.normalized * 2f;
        
        // om jag är tillräckligt nära, gå till nästa waypoint
        if (Vector2.Distance(waypoints[wayPointIndex].transform.position, transform.position) < 0.1f) {
            wayPointIndex += 1;

            //har jag kommit till slutet?

            if(wayPointIndex == waypoints.Count)
            {
                Destroy(gameObject);
                //TODO: ta bort liv från spelaren??
            }
        }

        //Rotera så att vi tittar åt det håll vi går

        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.rotation = Quaternion.Euler(0, 0, angle );
    }
}
