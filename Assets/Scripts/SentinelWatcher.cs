using UnityEngine;
using UnityEngine.SceneManagement;

class SentinelWatcher : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        //TODO call GameLoop
    }

    void OnCollisionEnter (Collision col)
    {
        Debug.Log("OnCollisionEnter: "+col.gameObject.name);
        if (col.gameObject.name == "chair_man")
        {
            SceneManager.LoadScene(2);
        }
    }
}
