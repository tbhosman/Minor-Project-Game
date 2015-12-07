using UnityEngine;
using System.Collections;

public class changeShader : MonoBehaviour {
public Shader firstShader;
public Shader secondShader;
public Renderer render;


	void Start () {
        firstShader = Shader.Find("Diffuse");
        render = GetComponent<Renderer>();
    }
	
    public void changeTheShader() {
        if (render.material.shader == firstShader)
        {
            render.material.shader = secondShader;
        }
        else { render.material.shader = firstShader; }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            changeTheShader();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            changeTheShader();
        }
    }
}
