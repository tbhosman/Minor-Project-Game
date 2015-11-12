using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class OpeningDoor : MonoBehaviour
{

    Animator animator;
    bool doorOpen;
    public GameObject player;
    private FirstPersonController firstpersoncontroller;
    void Start()
    {
        firstpersoncontroller = player.GetComponent<FirstPersonController>();
        doorOpen = false;
        animator = GetComponent<Animator>();
        Debug.Log("key1 opgepakt: " + firstpersoncontroller.getKey1());
    }


    void OnTriggerEnter(Collider colli)
    {
       
            if (colli.gameObject.tag == "Player" && firstpersoncontroller.getKey1())
            {
                doorOpen = true;
                Doors("Open");
                
        }
        
        }

    void OnTriggerExit(Collider coll)
    {
        if (doorOpen && firstpersoncontroller.getKey1())
        {
            doorOpen = false;
            Doors("Close");
        }
    }

    void Doors(string state)
    {
        animator.SetTrigger(state);
    }
    
}
