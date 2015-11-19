using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class usernameMenuButtons : MonoBehaviour {
	
	private InputField input;
	
	// Use this for initialization
	void Start () {

	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void start () {
		Application.LoadLevel ("intro");
		
	}
	
	
	public void getInput(string name){
		input = GameObject.Find("InputField").GetComponent<InputField>();
	}
	

}
