/// <summary>
/// Different Events used in EndAnimation
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndSceneScript : MonoBehaviour {

	public Image fadeImage;
	public float fadeduration;
	public float fadeinduration;
	private float a;
	public RawImage GasMaskOverlay;
	public Animator AntagAnimator;

	//Makes the Antag walk
	void AntagWalk(){
		AntagAnimator.SetFloat ("Speed", 0.2f);
		AntagAnimator.speed = 0.65f;
	}

	void ToggleGasMaskOverlay(){
		GasMaskOverlay.enabled = !GasMaskOverlay.enabled;
	}

	//Fade gradually from black to normal
	IEnumerator FadeIn(){
		float elapsed = 0;
		while (elapsed <fadeinduration) {
			elapsed += Time.deltaTime;
			a = 1-elapsed/fadeinduration;
			fadeImage.color = new Color (0, 0, 0, a);
			yield return null;
		}
	}

	//Fade gradually from normal to black
	IEnumerator FadeToBlack(){
		float elapsed = 0;
		while (elapsed <fadeduration) {
			elapsed += Time.deltaTime;
			a = elapsed/fadeduration;
			fadeImage.color = new Color (0, 0, 0, a);
			yield return null;
		}
	}

	//Loads Credits scene
    void LoadCredits() {
        Application.LoadLevel("creditscene");
    }
}

