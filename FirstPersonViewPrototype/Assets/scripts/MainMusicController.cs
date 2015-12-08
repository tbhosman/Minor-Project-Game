using UnityEngine;
using System.Collections;

public class MainMusicController : MonoBehaviour {

	public GameObject[] audioTracks;
	public float fadeTime = 2;
	public float maxVolume = 1;
	private int toFadeOut;

	// Use this for initialization
	void Start () {
		audioTracks = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			audioTracks[i] = transform.GetChild(i).gameObject;
			if (!(audioTracks[i].name == "Alarm")){
				audioTracks[i].gameObject.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!(audioTracks[0].GetComponent<AudioSource>().isPlaying) && audioTracks[0].gameObject.activeSelf) {
			FadeIn("Office");
		}
	}

	public void FadeIn(string track){
		for (int i = 1; i < transform.childCount; i++) {
			if (audioTracks[i].name == track){
				audioTracks[i].gameObject.SetActive(true);
				StartCoroutine(FadeMusic (audioTracks[i].GetComponent<AudioSource>(), maxVolume, fadeTime));
			} else {
				if (audioTracks[i].gameObject.activeSelf == true){
					toFadeOut = i;
				}
				StartCoroutine(FadeMusic (audioTracks[i].GetComponent<AudioSource>(), 0, fadeTime));
			}
		}
	}

	private IEnumerator FadeMusic(AudioSource audio, float targetVolume, float duration)
	{
		float startVolume = audio.volume;
		float inverseDuration = 1.0f / duration;
		float lerpFactor = 0.0f;
		while (lerpFactor <= 1.0f) {
			audio.volume = Mathf.Lerp(startVolume, targetVolume, lerpFactor);
			lerpFactor = lerpFactor + Time.deltaTime * inverseDuration;
			yield return null;
		}
		audio.volume = targetVolume;
		audioTracks [toFadeOut].SetActive (false);
	}

}
