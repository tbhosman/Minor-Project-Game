/// <summary>
/// Script that plays the movie in the game over scene
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class GameOverMovie : MonoBehaviour {
	
	public MovieTexture filmpje;
	private AudioSource geluid;

	void Start () {
		GetComponent<RawImage>().texture = filmpje as MovieTexture;
		geluid = GetComponent<AudioSource>();
		geluid.clip = filmpje.audioClip;
		filmpje.Play();
		geluid.Play();
	}
}