﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class fade2 : MonoBehaviour {

	public List<Text> texts;
	float timer;
	public float duration = 0.5f;
	public Light dimLight;
	public float zoomSpeed1 = 0.05f;
	public float zoomSpeed2 = 0.5f;


	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//float ratio1 = Time.time / duration;

		Text text1 = texts [1];
		text1.color = Color.black;
		Text text2 = texts [2];
		text2.color = Color.black;
		Text text3 = texts [3];
		text3.color = Color.black;

		if (timer < 5.0f) {
			float ratio = timer / duration;
			Text text = texts [0];
			Color myColor = text.color;
			myColor.a = Mathf.Lerp (0, 1, ratio);
			text.color = myColor;
			if (ratio > 0.6 && ratio < 1.6) {
				myColor.a = Mathf.Lerp (1, 0, ratio);
				text.color = myColor;

			}
			//Debug.Log (timer);
			//Debug.Log (timer);

		} else if (timer > 5.0f && timer < 10.0f) {
		
			float ratio = (Time.time - 5.0f) / duration; //- ratio1;
			Text text = texts [1];
			text.color = Color.white;
			Color myColor = text.color;
			myColor.a = Mathf.Lerp (0, 1, ratio);
			text.color = myColor;
			if (ratio > 0.6 && ratio < 1.6) {
				myColor.a = Mathf.Lerp (1, 0, ratio);
				text.color = myColor;
			}
			Debug.Log (ratio);
			//Debug.Log (timer);

		} else if (timer > 10.0f && timer < 15.0f) {
			
			float ratio = (Time.time - 10.0f) / duration; //- ratio1;
			Text text = texts [2];
			text.color = Color.white;
			Color myColor = text.color;
			myColor.a = Mathf.Lerp (0, 1, ratio);
			text.color = myColor;
			if (ratio > 0.6 && ratio < 1.6) {
				myColor.a = Mathf.Lerp (1, 0, ratio);
				text.color = myColor;
			}
			Debug.Log (ratio);
			//Debug.Log (timer);
		} else if (timer > 15.0f && timer < 20.0f) {
			
			float ratio = (Time.time - 15.0f) / duration; //- ratio1;
			Text text = texts [3];
			text.color = Color.white;
			Color myColor = text.color;
			myColor.a = Mathf.Lerp (0, 1, ratio);
			text.color = myColor;
			if (ratio > 0.6 && ratio < 1.6) {
				myColor.a = Mathf.Lerp (1, 0, ratio);
				text.color = myColor;
			}
			//Debug.Log (ratio);
			//Debug.Log (timer);
		} else if (timer > 20.0f) {
			dimLight.intensity += zoomSpeed1;
			} if (timer > 25.0f) {
				dimLight.intensity -= zoomSpeed1;
			}
		}
}

