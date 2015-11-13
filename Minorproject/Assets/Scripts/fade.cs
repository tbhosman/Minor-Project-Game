using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class fade : MonoBehaviour {
	
	public float duration = 5;
	//int level = 1;
	//Time time;

	public List<Text> texts;
	float timer;

	void Update () {
		timer += Time.deltaTime;
		float ratio = Time.time / duration;
		float ind = Mathf.Floor (ratio);
		Text text = texts [(int)ind];
		Color myColor = text.color;
		myColor.a = Mathf.Lerp (0, 1, ratio);
		text.color = myColor;
		if (ratio > (ind + 1)*0.5 && ratio < (ind+1)*1.2) {
			myColor.a = Mathf.Lerp (1, 0, ratio);
			text.color = myColor;
		} 

		//ratio -= ind;
		Debug.Log (ratio);




		//foreach (var text in texts) {
			//Color myColor = text.color;

			//myColor.a = Mathf.Lerp (0, 1, ratio);
			//text.color = myColor;
			//if (ratio > 0.5 && ratio < 1.2) {
			//	myColor.a = Mathf.Lerp (1, 0, ratio);
			//	text.color = myColor;
			//} 
			//else if (ratio > 1.2) {
			//Application.LoadLevel (level);
			//level++;
			//}
		}

}
