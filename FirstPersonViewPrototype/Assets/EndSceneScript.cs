﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndSceneScript : MonoBehaviour {

	public Image fadeImage;
	public float shakeduration;
	public float fadeduration;
	private float a;

	IEnumerator FadeToBlack(){
		float elapsed = 0;
		while (elapsed <fadeduration) {
			elapsed += Time.deltaTime;
			a = elapsed/fadeduration;
			fadeImage.color = new Color (0, 0, 0, a);
			yield return null;
		}
	}
	
	IEnumerator Shake(){

		float elapsed = 0.0f;
		Vector3 originalCamPos = Camera.main.transform.position;

		while (elapsed < shakeduration) {
			
			elapsed += Time.deltaTime;          

			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			float z = Random.value * 2.0f - 1.0f;
			
			Camera.main.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z + z);
			
			yield return null;
		}
		
		Camera.main.transform.position = originalCamPos;
	}

}

