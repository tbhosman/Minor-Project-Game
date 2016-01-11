/// <summary>
/// Keep an image component in the object disabled on activation
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class disableOnActive : MonoBehaviour {

    private RawImage img;

	void Start () {
        img = gameObject.GetComponent<RawImage>();
        img.enabled = false;
	}
	
}
