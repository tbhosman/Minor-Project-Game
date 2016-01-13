/// <summary>
/// Randomly starts a smoke effect through the ventilation
/// </summary>

using UnityEngine;
using System.Collections;

public class ventilationSmoke : MonoBehaviour {
    public ParticleSystem ventilatie;
    public AudioClip audioclip;
    public float playtime;
    public AudioSource audiosource;
    public float playfromseconds;
    public bool hasaudio;

    // Use this for initialization
    void Start () {
        Rook();
        InvokeRepeating("Rook", 3, 3);
    }
	
	// Update is called once per frame
	void Rook () {
        if (Random.Range(1, 5) <= 1)
        {
            ventilatie.Play();
            
                audiosource.clip = audioclip;
                audiosource.time = playfromseconds;
                audiosource.Play();
                StartCoroutine(PlayAudioSource());
        }
    }

    IEnumerator PlayAudioSource()
    {
        audiosource.Play();
        yield return new WaitForSeconds(playtime);
        audiosource.Stop();
        audiosource.time = 0;
    }
}




