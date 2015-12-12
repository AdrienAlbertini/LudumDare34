using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent(typeof(GUITexture))]
public class SceneFader : SingletonBehaviour<SceneFader>
{
    public event EventHandler fadeOver;

    [SerializeField]
    public float fadeSpeed = 1.5f;

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
        this._fadeImg.color = Color.Lerp(this._fadeImg.color, (this._fadeToClear ? Color.clear : Color.black), this.fadeSpeed * Time.fixedDeltaTime);
        
        if (this._fadeToClear && this._fadeImg.color.a <= 0.05f)
        {
            this._fadeImg.color = Color.clear;
            this._fadeImg.enabled = false;
            if (this.fadeOver != null)
                this.fadeOver(this, EventArgs.Empty);
        }
        else if (!this._fadeToClear && this._fadeImg.color.a >= 0.95f)
        {
            this._fadeImg.color = Color.black;
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