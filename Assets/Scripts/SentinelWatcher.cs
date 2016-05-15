using UnityEngine;
using UnityEngine.SceneManagement;

class SentinelWatcher : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        
    }

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.name == "chair_man")
        {
            SceneManager.LoadScene(2);
        }
    }
}
