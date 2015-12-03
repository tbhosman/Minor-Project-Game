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

	void AntagWalk(){
		AntagAnimator.SetFloat ("Speed", 0.2f);
		AntagAnimator.speed = 0.65f;
	}

	void ToggleGasMaskOverlay(){
		GasMaskOverlay.enabled = !GasMaskOverlay.enabled;
	}

	IEnumerator FadeIn(){
		float elapsed = 0;
		while (elapsed <fadeinduration) {
			elapsed += Time.deltaTime;
			a = 1-elapsed/fadeinduration;
			fadeImage.color = new Color (0, 0, 0, a);
			yield return null;
		}
	}
	IEnumerator FadeToBlack(){
		float elapsed = 0;
		while (elapsed <fadeduration) {
			elapsed += Time.deltaTime;
			a = elapsed/fadeduration;
			fadeImage.color = new Color (0, 0, 0, a);
			yield return null;
		}
	}

}

