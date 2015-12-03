using UnityEngine;
using System.Collections;

public class ShakeCameraScript : MonoBehaviour {

	public float shakeduration;
	public float shakeIntensity;

	IEnumerator Shake(){
		
		float elapsed = 0.0f;
		Vector3 originalCamPos = Camera.main.transform.position;
		
		while (elapsed < shakeduration) {
			
			elapsed += Time.deltaTime;          
			
			float x = (Random.value * 2.0f - 1.0f)*shakeIntensity;
			float y = (Random.value * 2.0f - 1.0f)*shakeIntensity;
			float z = (Random.value * 2.0f - 1.0f)*shakeIntensity;

			Camera.main.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z + z);
			
			yield return null;
		}
		
		Camera.main.transform.position = originalCamPos;
	}
}
