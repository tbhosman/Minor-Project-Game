/// <summary>
/// Manages the loading screen. Starts loading after fading in, loads asynchronously to keep the loading animation running,
/// then fades out to loaded scene
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadIntro : MonoBehaviour {

	private bool loaded;
	private bool fadingOut;
	private bool loading;
	AsyncOperation async;
	public GameObject MMCPrefab;
	private GameObject MainMusicController;

	void Start(){
		loaded = false;
		fadingOut = false;
		loading = false;
		Application.backgroundLoadingPriority = ThreadPriority.High;
		if (mainMenuButtons.leveltoload == "prototype1.0"){
			Destroy (GameObject.Find("MainMenuMusic(Clone)"));
			Destroy (GameObject.Find ("DataAquisitie"));
			Destroy (GameObject.Find ("DataAquisitie(Clone)"));
			Cursor.visible = false;
			MainMusicController = GameObject.Instantiate(MMCPrefab);
			GameObject.DontDestroyOnLoad(MainMusicController);
			MainMusicController.transform.FindChild("Alarm").gameObject.SetActive(false); //do not boot with alarm on
		}
	}

	// Use this for initialization
	void Update() {
		//wait for loading screen to fade in, then execute once
		if (!GameObject.Find ("SceneFader").GetComponent<Image> ().enabled && !loaded && !loading) {
			if (mainMenuButtons.leveltoload == "prototype1.0")
				MainMusicController.GetComponent<MainMusicController>().FadeIn("Office"); //NOT FINAL LOCATION, SEE BELOW
			loading = true;
			//async.allowSceneActivation = false;
			StartCoroutine (LoadLevel (async));
		}

		//if next scene is loaded, start fading out loading screen
		if (loaded) {
			Debug.Log(1);
			GameObject.Find ("SceneFader").GetComponent<SceneFadeInOut> ().FadeToBlack();
			fadingOut = true;
		}

		//when faded out, switch to new scene
		if (GameObject.Find ("SceneFader").GetComponent<Image> ().color.a >= 0.95f && loaded) {
			//MainMusicController.GetComponent<MainMusicController>().FadeIn("Office"); //TODO: INSERT AREA GAME LOADED IN
			async.allowSceneActivation = true;
		}
	}

	IEnumerator LoadLevel(AsyncOperation async){

		while (!async.isDone) {
			async = Application.LoadLevelAsync(mainMenuButtons.leveltoload);
			async.allowSceneActivation = false;
			yield return null;
		}
		Debug.Log("Loading complete");
		loaded = true;
	}
}