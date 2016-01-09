/// <summary>
/// Script that manages fading in and fading out of menus on startup and leaving, respectively
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

	private bool sceneStarting = true;      // Whether or not the scene is still fading in.

	public bool sceneEnding = false;

	public string scene;

	private Image fadeTexture;
	
	
	void Awake ()
	{
		fadeTexture = GetComponent<Image>();
	}
	
	
	void Update ()
	{
		// If the scene is starting...
		if(sceneStarting)
			// ... call the StartScene function.
			StartScene();

		if (sceneEnding)
			EndScene();
	}
	
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		fadeTexture.color = Color.Lerp(fadeTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	public void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		fadeTexture.color = Color.Lerp(fadeTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(fadeTexture.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			fadeTexture.color = Color.clear;
			fadeTexture.enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		fadeTexture.enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if (fadeTexture.color.a >= 0.95f) {
			// ... reload the level.
			if (scene == "") Application.Quit();
			else Application.LoadLevel (scene);
		}
	}
}

