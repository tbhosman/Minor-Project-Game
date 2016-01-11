/// <summary>
/// Fetches the global and local highscores
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class HighscoreManager : MonoBehaviour {

	public Text localHighscore;
	public Text globalHighscore;
	public string url = "http://drproject.twi.tudelft.nl:8086";


	// Use this for initialization
	void Start () {
		int localBest = PlayerPrefs.GetInt ("highscore"); // fetch local highscore
		if (localBest != 0){
			localHighscore.text = localBest.ToString() + " minutes";
		} else {
			localHighscore.text = "No highscore yet";
		}

		globalHighscore.text = "Retrieving highscore.."; // placeholder while loading global highscore

		StartCoroutine(getGlobalHighscore());
	}

	IEnumerator getGlobalHighscore()
	{
		WWW www = new WWW(url + "/bestScore");
		float elapsedTime = 0.0f;
		
		while (!www.isDone)
		{
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= 10.0f) { // server timed out
				globalHighscore.text = "Server is offline";
				break;
			}
			yield return null;
		}
		
		if (!www.isDone || !string.IsNullOrEmpty(www.error)) // if an error was received
		{
			//Debug.LogError("Load Failed");
			globalHighscore.text = "Load failed";
			yield break;
		}

		// answer from server has been received, no error returned
		string globalText =  www.text; // Pass retrieved result.
		char[] delimiters = {':', '}'};
		globalText = globalText.Split (delimiters)[1];

		if (globalText == "null") {
			globalHighscore.text = "No highscore yet";
		} else {
			globalHighscore.text = globalText + " minutes";
		}
	}

	public void BackToMenu(){
		GameObject.Find ("SceneFader").GetComponent<SceneFadeInOut> ().scene = "menu";
		GameObject.Find ("SceneFader").GetComponent<SceneFadeInOut> ().sceneEnding = true;
		//Application.LoadLevel ("menu");
	}
}