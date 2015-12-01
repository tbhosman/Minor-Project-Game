using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SecurityPanelController : MonoBehaviour {

	public GameObject codeDisplayPanel;
	public Text codeDisplayText;
	public string correctCode;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).name == "CodePanel"){
				codeDisplayPanel = transform.GetChild (i).gameObject;
				codeDisplayText = codeDisplayPanel.transform.GetChild(0).GetComponent<Text>();
			}
		}
		codeDisplayText.text = "";
	}

	public void GetDigitInput(Text input){
		if (codeDisplayText.text.Length < 4) {
			codeDisplayText.text += input.text;
			StartCoroutine (UpdateTextField ());
		}
	}

	public void UndoInput(){
		if (codeDisplayText.text.Length > 0 && codeDisplayText.text.Length != 4){
			codeDisplayText.text = codeDisplayText.text.Remove(codeDisplayText.text.Length - 1);
			StartCoroutine(UpdateTextField ());
		}
	}

	IEnumerator UpdateTextField(){
		if (codeDisplayText.text.Length > 3) {
			if (codeDisplayText.text == correctCode) {
				codeDisplayPanel.GetComponent<Image> ().color = Color.green;
				yield return StartCoroutine (WaitForRealSeconds (2.0f));
				codeDisplayText.text = "";
				codeDisplayPanel.GetComponent<Image> ().color = new Color (255, 255, 255, 100);
			}
			else {
				codeDisplayPanel.GetComponent<Image> ().color = Color.red;
				yield return StartCoroutine (WaitForRealSeconds (2.0f));
				codeDisplayText.text = "";
				codeDisplayPanel.GetComponent<Image> ().color = new Color (255, 255, 255, 100);
			}
		}
	}

	private IEnumerator WaitForRealSeconds(float waitTime)
	{
		float endTime = Time.realtimeSinceStartup + waitTime;
		
		while (Time.realtimeSinceStartup < endTime)
		{
			yield return null;
		}
	}

}
