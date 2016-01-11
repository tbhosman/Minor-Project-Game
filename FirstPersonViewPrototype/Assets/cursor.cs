using UnityEngine;
using System.Collections;


public class cursor : MonoBehaviour
{

    public Texture2D texturecursor;
    public CursorMode cursormode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;

    void Start(){
        Cursor.SetCursor(texturecursor, hotspot, cursormode);
    }

    void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, cursormode);
    }
}