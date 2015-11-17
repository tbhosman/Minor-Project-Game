using UnityEngine;
using System.Collections;
using UnityEngine.UI;

    [RequireComponent (typeof(AudioSource))]
    
public class Movie : MonoBehaviour {

    public MovieTexture filmpje;
    private AudioSource geluid;
	void Start () {
        GetComponent<RawImage>().texture = filmpje as MovieTexture;
        geluid = GetComponent<AudioSource>();
        geluid.clip = filmpje.audioClip;
        filmpje.Play();
        geluid.Play();
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Space)&& filmpje.isPlaying)
        {
            filmpje.Pause();
        }else if (Input.GetKeyDown(KeyCode.Space) && !filmpje.isPlaying)
        {
            filmpje.Play();
        }
    }
}
