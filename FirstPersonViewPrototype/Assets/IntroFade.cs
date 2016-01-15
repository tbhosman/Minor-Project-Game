/// <summary>
/// Fades text for intro in and out.
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroFade : MonoBehaviour {

	public float fadeSpeed;
	private Text date;
	private Text year;
	private Text time;
	private Text situation;
	private Material title;
	private bool fadedIn;
	private bool fadedOut;
	private bool titleDisplaying;
	private float timeWaited;


	// Use this for initialization
	void Start () {
		date = GameObject.Find ("Datum").GetComponent<Text> ();
		year = GameObject.Find ("Jaar").GetComponent<Text> ();
		time = GameObject.Find ("Tijd").GetComponent<Text> ();
		situation = GameObject.Find ("Situatie").GetComponent<Text> ();
		title = GameObject.Find ("titel").GetComponent<Renderer> ().material;

		date.color = new Color (1, 1, 1, 0);
		year.color = new Color (1, 1, 1, 0);
		time.color = new Color (1, 1, 1, 0);
		situation.color = new Color (1, 1, 1, 0);
		title.color = new Color (1, 1, 1, 0);

		fadedIn = false;
		fadedOut = false;
		titleDisplaying = false;

		timeWaited = 0.0f;

		Destroy (GameObject.Find("MainMenuMusic(Clone)"));
		Destroy (GameObject.Find ("DataAquisitie"));
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!fadedIn) {
			if (date.color.a < 0.95f) {
				fadeIn (date);
				return;
			} else if (year.color.a < 0.95f) {
				fadeIn (year);
				return;
			} else if (time.color.a < 0.95f) {
				fadeIn (time);
				return;
			} else if (situation.color.a < 0.95f) {
				fadeIn (situation);
				return;
			}
			fadedIn = true;
		} else {
			fadeOut (date);
			fadeOut (year);
			fadeOut (time);
			fadeOut (situation);
			fadedOut = true;
		}

		if (timeWaited < 2.0f) {
			timeWaited += Time.unscaledDeltaTime;
			return;
		}

		if (fadedOut == true) {
			fadeInTitle(title);
		}

		if (titleDisplaying) {
			DontDestroyOnLoad(GameObject.Find ("MainMusicController"));
			Application.LoadLevel(4);
		}
	}

	void fadeIn(Text text){
		text.color = Color.Lerp(text.color, Color.white, fadeSpeed * Time.deltaTime);
		if (text.color.a > 0.95f) {
			Color tmp = text.color;
			tmp.a = 1.0f;
			text.color = tmp;
		}
	}

	void fadeOut(Text text){
		text.color = Color.Lerp(text.color, new Color (1,1,1,0), fadeSpeed * Time.deltaTime);
		if (text.color.a < 0.05f) {
			Color tmp = text.color;
			tmp.a = 0.0f;
			text.color = tmp;
		}
	}

	void fadeInTitle(Material text){
		text.color = Color.Lerp(text.color, Color.white, fadeSpeed * Time.deltaTime);
		if (text.color.a > 0.95f) {
			Color tmp = text.color;
			tmp.a = 1.0f;
			text.color = tmp;
			titleDisplaying = true;
		}
	}

}