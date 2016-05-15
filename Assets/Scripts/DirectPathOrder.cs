using UnityEngine;
using System.Collections;

public class DirectPathOrder : MonoBehaviour
{
    public float speed = 5.0f;
    public float reachDist = 1.0f;
    public float rotationSpeed = 5.0f;
    private bool destinationSet = false;
    private Vector3 destination;
    private bool rotateTowardsPlayer = false;
    public DirectPathOrderListener listener;

    // Use this for initialization
    void Start()
    {
    }

    public void interrupt()
    {
        destinationSet = false;
        rotateTowardsPlayer = false;
        listener = null;
    }

    public void setDestination(Vector3 destination, float speed, bool rotateTowardsPlayer, DirectPathOrderListener listener)
    {
        this.destination = destination;
        destinationSet = true;
        this.speed = speed;
        this.rotateTowardsPlayer = rotateTowardsPlayer;
        this.listener = listener;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!destinationSet && !rotateTowardsPlayer)
            return;
        Vector3 dest = destinationSet ? destination : Camera.main.transform.position;
        Vector3 dir = dest - transform.position;

        //To smooth the rotation of the object when it is on a new trajectory
        if (dir != Vector3.zero)
        {
            if (transform.rotation != Quaternion.LookRotation(dir))
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(dir),
                    Time.deltaTime * rotationSpeed
                );  //TODO rotate at the right angle, not the opposite
            }
            else
            {
                rotateTowardsPlayer = false;
                listener.destinationReached();
            }
        }

        if (destinationSet)
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime * speed);
            float dist = Vector3.Distance(destination, transform.position);
            if (dist <= reachDist)
            {
                destinationSet = false;
                if(!rotateTowardsPlayer)
                {
                    listener.destinationReached();
                }
            }
        }
    }

}
