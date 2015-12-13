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

    private bool? _fadeToClear = null;

    private Image _fadeImg;

    void Awake()
    {
        this._fadeImg = this.GetComponent<Image>();
        this._fadeImg.color = Color.clear;
        this._fadeImg.enabled = false;
    }

    void FixedUpdate()
    {
        if (this._fadeToClear != null)
        {
            this._fadeImg.color = Color.Lerp(this._fadeImg.color, (this._fadeToClear.Value ? Color.clear : Color.black), this.fadeSpeed * Time.fixedDeltaTime);

            if (this._fadeToClear.Value && this._fadeImg.color.a <= 0.05f)
            {
                this._fadeImg.color = Color.clear;
                if (this.fadeOver != null)
                    this.fadeOver(this, EventArgs.Empty);
                this._fadeToClear = null;
                this._fadeImg.enabled = false;
            }
            else if (!this._fadeToClear.Value && this._fadeImg.color.a >= 0.95f)
            {
                this._fadeImg.color = Color.black;
                if (this.fadeOver != null)
                    this.fadeOver(this, EventArgs.Empty);
                this._fadeToClear = null;
            }
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