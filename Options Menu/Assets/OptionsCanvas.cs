using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsCanvas : MonoBehaviour {
    public Slider aaSlider;
    public Text aaText;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    private bool togglefullscreen;

	void Start () {
        PlayerPrefs.SetInt("AA", 4);
        if (PlayerPrefs.HasKey("AA"))
        {
            aaSlider.value = Mathf.Log(PlayerPrefs.GetInt("AA"),2);
            aaText.text = "Anti aliasing: " + PlayerPrefs.GetInt("AA") + "x";
            if (aaSlider.value == 0)
            {
                aaText.text = "disabled";
            }
       }
        Screen.fullScreen = true;
    }
	
    public void AAchange()
    {   
        PlayerPrefs.SetInt("AA",(int) Mathf.Pow(2, (int)aaSlider.value));
        aaText.text = "Anti aliasing: " + PlayerPrefs.GetInt("AA") + "x";
        QualitySettings.antiAliasing = PlayerPrefs.GetInt("AA");
        if (aaSlider.value == 0)
        {
            aaText.text = "disabled";
        }
    }

    public void DropDown()
    {
         switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 1:
                Screen.SetResolution(1280, 1024, true);
                break;
            case 2:
                Screen.SetResolution(800, 600, true);
                break;
        }
    }

    public void setFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    void Update () {
	
	}

    
}
