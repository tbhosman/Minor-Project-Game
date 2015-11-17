using UnityEngine;
using System.Collections;

public class EnemyAnimationScript : MonoBehaviour {
	
	private Animator animator;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
	}

	public void Update() {
		Debug.Log(animator.GetFloat ("Speed"));
		animator.SetFloat("Speed", Mathf.Abs (GameObject.Find ("Enemy").GetComponent<EnemyRouting> ().rb.velocity.z));
		}
}

//Voor later: http://answers.unity3d.com/questions/475477/animation-parameters-1.html
