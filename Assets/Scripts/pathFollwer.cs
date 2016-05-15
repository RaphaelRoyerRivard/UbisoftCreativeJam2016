using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour
{
    public Transform[] path;
    public float speed = 5.0f;
    public float reachDist = 1.0f;
    public int currentPoint = 1;
    public float rotationSpeed = 5.0f;
    public Transform TargetPlayer;
    //public float speed = 10f;
    public float turnSpeed = 50f;

    // Use this for initialization
    void Start()
    {
        speed = 5.0f;
        rotationSpeed = 5f;
        TargetPlayer = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = path[currentPoint].position - transform.position;

        transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, Time.deltaTime * speed);


        //To smooth the rotation of the object when it is on a new trajectory
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                Time.deltaTime * rotationSpeed
            );
        }

        //transform.position += dir * Time.deltaTime * speed;
        float dist = Vector3.Distance(path[currentPoint].position, transform.position);
        if (dist <= reachDist)
        {
            currentPoint++;
        }

        if (currentPoint >= path.Length)
        {
            currentPoint = 0;
        }
    }

    void OnDrawGizmos()
    {
        if (path.Length > 0)
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null)
                {
                    Gizmos.DrawSphere(path[i].position, reachDist);
                }
            }
    }


}
