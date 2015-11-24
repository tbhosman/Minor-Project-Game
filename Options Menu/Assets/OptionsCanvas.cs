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
    public Toggle fullscreenToggle;
    public Toggle vsyncToggle;
    private bool togglefullscreen;
    public RawImage Face;
    AudioSource scream;
    public AudioClip Screamsound;


	void Start () {
        scream = GetComponent<AudioSource>();
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

    public void Volume()
    {
        AudioListener.volume = VolumeSlider.value;
        Debug.Log(AudioListener.volume);
    }


    public void DropDown()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(640, 480, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(800, 600, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1024, 768, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(1152, 864, Screen.fullScreen);
                break;
            case 4:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 5:
                Screen.SetResolution(1280, 800, Screen.fullScreen);
                break;
            case 6:
                Screen.SetResolution(1280, 960, Screen.fullScreen);
                break;
            case 7:
                Screen.SetResolution(1280, 1024, Screen.fullScreen);
                break;
            case 8:
                Screen.SetResolution(1366, 768, Screen.fullScreen);
                break;
            case 9:
                Screen.SetResolution(1400, 1050, Screen.fullScreen);
                break;
            case 10:
                Screen.SetResolution(1440, 900, Screen.fullScreen);
                break;
            case 11:
                Screen.SetResolution(1600, 900, Screen.fullScreen);
                break;
            case 12:
                Screen.SetResolution(1600, 1024, Screen.fullScreen);
                break;
            case 13:
                Screen.SetResolution(1680, 1050, Screen.fullScreen);
                break;
            case 14:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
        }
    }

    public void setFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void vsync()
    {
        if (vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
            Debug.Log("vsynccount: 1");
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Debug.Log("vsynccount: 0");
        }
        
    }

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


    public void sensetivity()
    {
        PlayerPrefs.SetFloat("Volume", sensSlider.value);
        if (sensSlider.value == 1)
        {
            scream.PlayOneShot(Screamsound, 1f);
            StartCoroutine(popupFace());
            
            
        }
    }
    void Update () {
	
	}

    
}
