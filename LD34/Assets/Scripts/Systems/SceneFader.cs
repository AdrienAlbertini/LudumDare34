using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class SceneFader : SingletonBehaviour<SceneFader>
{
    [SerializeField]
    public float FadeSpeed = 1.5f;

    private bool _fadeToClear = true;

    private GUITexture _guiTexture;

    void Awake()
    {
        this.enabled = false;
        this._guiTexture = this.GetComponent<GUITexture>();
        this._guiTexture.pixelInset = new Rect(0f, -Screen.height / 2.0f, Screen.width, Screen.height);
        this._guiTexture.color = Color.clear;
    }


    void FixedUpdate()
    {
        this._guiTexture.color = Color.Lerp(this._guiTexture.color, (this._fadeToClear ? Color.clear : Color.black), this.FadeSpeed * Time.deltaTime);

        if (this._fadeToClear && this._guiTexture.color.a <= 0.05f)
        {
            this._guiTexture.color = Color.clear;
            this.enabled = false;
        }
        else if (this._guiTexture.color.a >= 0.95f)
        {
            this._guiTexture.color = Color.black;
            this.enabled = false;
        }
    }

    public void StartScene()
    {
        this._fadeToClear = true;
        this.enabled = true;
    }

    public void EndScene()
    {
        this._fadeToClear = false;
        this.enabled = true;
    }
}