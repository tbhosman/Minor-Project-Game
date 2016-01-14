/// <summary>
/// Manages the options in the options menu
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class OptionsCanvas : MonoBehaviour {
    public Slider aaSlider;
    public Slider VolumeSlider;
    public Slider sensSlider;
    public Text aaText;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public Toggle vsyncToggle;
    private bool togglefullscreen;
    public RawImage Face;
    AudioSource scream;
    public AudioClip Screamsound;
 

	void Start () {
        //displays the resolutions that are available for your pc
        resolutionDropdown.options.Clear();
        for (int i = 0; i < Screen.resolutions.Length; i++) { resolutionDropdown.options.Add(new Dropdown.OptionData() { text = Screen.resolutions[i].width + "x" + Screen.resolutions[i].height }); }
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution", Screen.resolutions.Length - 1);
        
        //displays the qualitylevels available for your pc
        qualityDropdown.options.Clear();
        for (int i = 0; i < QualitySettings.names.Length; i++) { qualityDropdown.options.Add(new Dropdown.OptionData() { text = QualitySettings.names[i] }); }
        qualityDropdown.value = PlayerPrefs.GetInt("quality", 2);
        scream = GetComponent<AudioSource>();
        
        //changes the antialiasing
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
        AudioListener.volume = VolumeSlider.value;
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

    //changes the volume
    public void Volume()
    {
        AudioListener.volume = VolumeSlider.value;
        Debug.Log(AudioListener.volume);
    }

    //changes the resolution
    public void DropDown()
    {
        Screen.SetResolution(Screen.resolutions[resolutionDropdown.value].width, Screen.resolutions[resolutionDropdown.value].height, true);
        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);
    }

    //changes the quality
    public void DropDownQuality() {
        QualitySettings.SetQualityLevel(qualityDropdown.value, true);
        PlayerPrefs.SetInt("quality", qualityDropdown.value);
    }

    //set the screen fullscreen
    public void setFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    //changes the vsync
    public void vsync()
    {
        if (vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        
    }

    //the jumpscare in the optionsmenu
    IEnumerator popupFace()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.03f);
            Face.transform.SetAsLastSibling();
            yield return new WaitForSeconds(0.09f);
            Face.transform.SetAsFirstSibling();
        }
    }

    //changes the sensetivity
    public void sensetivity()
    {
        PlayerPrefs.SetFloat("Volume", sensSlider.value);
        if (sensSlider.value == 1)
        {
            scream.PlayOneShot(Screamsound, 1f);
            StartCoroutine(popupFace());
        }
        PlayerPrefs.SetFloat("sensetivity", sensSlider.value);
    }

    //menu options
    public void back() {
		GameObject.Find ("SceneFader").GetComponent<SceneFadeInOut> ().scene = "menu";
		GameObject.Find ("SceneFader").GetComponent<SceneFadeInOut> ().sceneEnding = true;
    } 
}
