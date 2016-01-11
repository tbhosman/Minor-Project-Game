/// <summary>
/// Makes the object this is attached to glow (used on pickups)
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
public class GlowScript : MonoBehaviour {

	private Material mat;
	private Renderer rend;
	public float duration;
	public Color fullEmissionColor;

	void Start(){
		rend = gameObject.GetComponent<Renderer> ();
		mat = rend.material;
		mat.EnableKeyword ("_EmissionColor");
	}
	void Update () {
		float emission = Mathf.PingPong (Time.time, duration)*0.1f;
		Color finalColor = fullEmissionColor * Mathf.LinearToGammaSpace (emission);
		mat.SetColor ("_EmissionColor", finalColor);
	}
}
