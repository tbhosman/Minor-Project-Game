using UnityEngine;
using System.Collections;

public class ReaktorGlow : MonoBehaviour {

	public GameObject[] light;
	public float glowRange;
	public float glowSpeed;
	private float intensity;

	void Start(){
		intensity = light[0].GetComponent<Light>().intensity;
	}
	// Update is called once per frame
	void FixedUpdate () {
		for (int i = 0; i < light.Length; i++) {
			light[i].GetComponent<Light>().intensity =  intensity + glowRange*Mathf.Sin(glowSpeed * Time.timeSinceLevelLoad);
		}
	}
}
