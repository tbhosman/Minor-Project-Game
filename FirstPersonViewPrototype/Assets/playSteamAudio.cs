using UnityEngine;
using System.Collections;

public class playSteamAudio : MonoBehaviour {

	public float steamPlayTime;

	public void PlaySteamAudio(){
		StartCoroutine(PlayAudioSource());
	}

	IEnumerator PlayAudioSource()
	{
		GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(steamPlayTime);
		GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().time = 0;
	}
}
