/// <summary>
/// Plays the frames and sound of a movie file
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

    [RequireComponent (typeof(AudioSource))]
    
public class Movie : MonoBehaviour {

    public MovieTexture filmpje;
    private AudioSource geluid;
	private GameObject player;
	public float maxVolume = 0.1f;
	public float maxDistance = 10.0f;

	void Start () {
		player = GameObject.Find ("FPSController");
        GetComponent<RawImage>().texture = filmpje as MovieTexture;
        geluid = GetComponent<AudioSource>();
        geluid.clip = filmpje.audioClip;
        filmpje.Play();
        geluid.Play();
    }
	
	// Update is called once per frame
	void Update () {

		float distance = Vector3.Distance (player.transform.position, transform.position);

		// manual volume rolloff, because this is bugged for movietextures: 
		///http://issuetracker.unity3d.com/issues/movietexture-rolloff-not-working-on-movie-textures-audio
		if (distance > maxDistance) {
			geluid.volume = 0.0f;
		} else {
			geluid.volume = maxVolume - maxVolume / maxDistance * distance;
		}

		// loop when finished
		if (!geluid.isPlaying) {
			filmpje.Play();
			geluid.Play();
		}
		//	    if(Input.GetKeyDown(KeyCode.Space)&& filmpje.isPlaying)
//        {
//            filmpje.Pause();
//        }else if (Input.GetKeyDown(KeyCode.Space) && !filmpje.isPlaying)
//        {
//            filmpje.Play();
//        }
    }
}
