using UnityEngine;
using System.Collections;

public class OpeningDoor : MonoBehaviour
{

    Animator animator;
    bool doorOpen;

    // Use this for initialization
    void Start()
    {
        doorOpen = false;
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider colli)
    {
        if (colli.gameObject.tag == "Player")
        {
            doorOpen = true;
            Doors("Open");
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (doorOpen)
        {
            doorOpen = false;
            Doors("Close");
        }
    }

    void Doors(string state)
    {
        animator.SetTrigger(state);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
