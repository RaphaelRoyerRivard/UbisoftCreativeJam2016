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

    public void setDestination(Vector3 destination, bool rotateTowardsPlayer, DirectPathOrderListener listener)
    {
        this.destination = destination;
        destinationSet = true;
        this.rotateTowardsPlayer = rotateTowardsPlayer;
        this.listener = listener;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!destinationSet && !rotateTowardsPlayer)
            return;
        if (!destinationSet && rotateTowardsPlayer)
        {
            Vector3 dir = transform.position - Camera.main.transform.position;
            if (transform.rotation != Quaternion.LookRotation(dir))
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(dir),
                    Time.deltaTime * rotationSpeed
                );
            }
            else
            {
                rotateTowardsPlayer = false;
                listener.destinationReached(this.gameObject, true);
            }
        }
        else {
            Vector3 dir = transform.position - destination;

            //To smooth the rotation of the object when it is on a new trajectory
            if (dir != Vector3.zero)
            {
                if (transform.rotation != Quaternion.LookRotation(dir))
                {
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        Quaternion.LookRotation(dir),
                        Time.deltaTime * rotationSpeed
                    );
                }
            }

            if (destinationSet)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
                float dist = Vector3.Distance(destination, transform.position);
                if (dist <= reachDist)
                {
                    destinationSet = false;
                    if (!rotateTowardsPlayer)
                    {
                        listener.destinationReached(this.gameObject, false);
                    }
                    else
                    {
                        Debug.Log("Angle still need to be set");
                    }
                }
            }
        }
    }

}
