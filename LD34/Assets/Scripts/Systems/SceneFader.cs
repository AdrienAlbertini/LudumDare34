using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class SceneFader : SingletonBehaviour<SceneFader>
{
    [SerializeField]
    public float FadeSpeed = 1.5f;

    private bool _fadeToClear = true;

    private Image _fadeImg;

    void Awake()
    {
        this._fadeImg = this.GetComponent<Image>();
        this._fadeImg.color = Color.clear;
        this._fadeImg.enabled = false;
    }

    void FixedUpdate()
    {
        this._fadeImg.color = Color.Lerp(this._fadeImg.color, (this._fadeToClear ? Color.clear : Color.black), this.FadeSpeed * Time.deltaTime);

        if (this._fadeToClear && this._fadeImg.color.a <= 0.05f)
        {
            this._fadeImg.color = Color.clear;
            this._fadeImg.enabled = false;
        }
        else if (this._fadeImg.color.a >= 0.95f)
        {
            this._fadeImg.color = Color.black;
            this._fadeImg.enabled = false;
        }
    }

    public void StartScene()
    {
        this._fadeToClear = true;
        this._fadeImg.enabled = true;
    }

    public void EndScene()
    {
        this._fadeToClear = false;
        this._fadeImg.enabled = true;
    }
}