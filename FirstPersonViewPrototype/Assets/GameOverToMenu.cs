using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		waitTillEndOfMovieCall ();
	}

	private void waitTillEndOfMovieCall(){
		StartCoroutine (waitTillEndOfMovie ());
	}

	IEnumerator waitTillEndOfMovie(){
		yield return new WaitForSeconds (11);
		Application.LoadLevel ("menu");
	}
}
