using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class SceneFader : MonoBehaviour
{
    [SerializeField]
    private float FadeSpeed = 1.5f;

    private bool _sceneStarting = true;

    private GUITexture _guiTexture;

    void Awake()
    {
        this._guiTexture = this.GetComponent<GUITexture>();
        this._guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }


    void Update()
    {
        if (this._sceneStarting)
            this.StartScene();
    }


    void FadeToClear()
    {
        this._guiTexture.color = Color.Lerp(this._guiTexture.color, Color.clear, this.FadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        this._guiTexture.color = Color.Lerp(this._guiTexture.color, Color.black, this.FadeSpeed * Time.deltaTime);
    }


    void StartScene()
    {
        FadeToClear();

        if (this._guiTexture.color.a <= 0.05f)
        {
            this._guiTexture.color = Color.clear;
            this._guiTexture.enabled = false;
            this._sceneStarting = false;
        }
    }


    public void EndScene()
    {
        this._guiTexture.enabled = true;
        FadeToBlack();
        // TODO Do the load scene here!
    }
}