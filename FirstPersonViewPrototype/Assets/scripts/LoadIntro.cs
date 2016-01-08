using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadIntro : MonoBehaviour {

	private bool loaded;
	private bool fadingOut;
	private bool loading;
	AsyncOperation async;

	void Start(){
		loaded = false;
		fadingOut = false;
		loading = false;
		Application.backgroundLoadingPriority = ThreadPriority.Low;
	}

	// Use this for initialization
	void Update() {
		//wait for loading screen to fade in, then execute once
		if (!GameObject.Find ("SceneFader").GetComponent<Image> ().enabled && !loaded && !loading) {
			loading = true;
			async = Application.LoadLevelAsync(mainMenuButtons.leveltoload);
			//async.allowSceneActivation = false;
			StartCoroutine (LoadLevel (async));
		}

		//if next scene is loaded, start fading out loading screen
		if (loaded) {
			GameObject.Find ("SceneFader").GetComponent<SceneFadeInOut> ().FadeToBlack();
			fadingOut = true;
		}

		//when faded out, switch to new scene
		if (GameObject.Find ("SceneFader").GetComponent<Image> ().color.a >= 0.95f && loaded) {
			async.allowSceneActivation = true;
		}
	}

	IEnumerator LoadLevel(AsyncOperation async){
		while (!async.isDone) {
			yield return async;
		}
		Debug.Log("Loading complete");
		loaded = true;
	}
}