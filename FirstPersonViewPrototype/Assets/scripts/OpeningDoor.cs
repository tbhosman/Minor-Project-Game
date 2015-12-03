using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class OpeningDoor : MonoBehaviour
{

    Animator animator;
    bool doorOpen;
    public GameObject player;
    private FirstPersonController firstpersoncontroller;
    public Text doorText;
    void Start()
    {
        firstpersoncontroller = player.GetComponent<FirstPersonController>();
        doorOpen = false;
        animator = GetComponent<Animator>();
        doorText.text = "";
    }
    
    void OnTriggerStay(Collider colli)
    {
        if (colli.gameObject.tag == "Player")
        {
            if (firstpersoncontroller.getKey1())
            {
                doorText.text = "Press E to open the door";
            }
            else
            {
                doorText.text = "Locked";
            }
        }

        if (colli.gameObject.tag == "Player" && firstpersoncontroller.getKey1() && !doorOpen && Input.GetKeyDown("e")) {
            {
                doorOpen = true;
                doorText.text = "";
                Doors("Open");
            }
        }
    }
        void OnTriggerExit(Collider coll)
        {
            if (doorOpen && firstpersoncontroller.getKey1())
            {
                doorOpen = false;
                Doors("Close");
            }
            doorText.text = "";
        }

        void Doors(string state)
    {
            animator.SetTrigger(state);
    }

}