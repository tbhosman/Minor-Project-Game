using UnityEngine;
using System.Collections;

public class EnemyAnimationScript : MonoBehaviour {
	
	private Animator animator;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
	}

	public void Update() {
		if (GameObject.Find ("Enemy").GetComponent<EnemyRouting> ().wantIdle) {
			animator.SetFloat("Speed", 0); //sets animation to idle
		}
		else if (GameObject.Find ("Enemy").GetComponent<EnemyRouting> ().wantWalk) {
			animator.SetFloat("Speed", GameObject.Find ("Enemy").GetComponent<EnemyRouting> ().speed); //sets animation to walking
		}

		if (animator.GetCurrentAnimatorStateInfo (0).IsName("Idle")) {
			GameObject.Find ("Enemy").GetComponent<EnemyRouting> ().isAnimIdle = true;
			GameObject.Find ("Enemy").GetComponent<EnemyRouting> ().isAnimWalk = false;

		} else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walking")) {
			GameObject.Find ("Enemy").GetComponent<EnemyRouting> ().isAnimIdle = false;
			GameObject.Find ("Enemy").GetComponent<EnemyRouting> ().isAnimWalk = true;
		}
		//Debug.Log(animator.GetFloat ("Speed"));
		//animator.SetFloat("Speed", Mathf.Abs (GameObject.Find ("Enemy").GetComponent<EnemyRouting> ().rb.velocity.z));
		}
}

//Voor later: http://answers.unity3d.com/questions/475477/animation-parameters-1.html
