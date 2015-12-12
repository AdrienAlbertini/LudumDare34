using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelsManager : SingletonBehaviour<LevelsManager>
{
    public List<string> scenes = new List<string>();
    private int _currentSceneId = 0;
    private bool _isNextSceneLoaded = false;
    private AsyncOperation _asyncOp;
    private bool _isLoading = false;
   
    IEnumerator Preloader()
    {
        this._asyncOp.allowSceneActivation = false;
        this._asyncOp = SceneManager.LoadSceneAsync(this.scenes[_currentSceneId + 1], LoadSceneMode.Additive);
        yield return this._asyncOp;
    }

    void FixedUpdate()
    {
        if (this._asyncOp != null && this._asyncOp.isDone && this._isLoading)
        {
            this._isLoading = false;
            this.PreloadNextScene();
        }
    }

    public void PreloadNextScene()
    {
        if ((this._currentSceneId + 1) < this.scenes.Count)
        {
            StartCoroutine(this.Preloader());
        }
    }

    public void ReloadScene()
    {
        this.LoadScene(this._currentSceneId);
    }

    public void SwitchToNextScene()
    {
        if (this._currentSceneId >= 0 && this._currentSceneId < this.scenes.Count)
        {
            this._asyncOp.allowSceneActivation = true;
            this._isLoading = true;
            ++this._currentSceneId;
        }
    }

    public void LoadScene(int sceneId)
    {
        if (sceneId >= 0 && sceneId < this.scenes.Count)
        {
            this._currentSceneId = sceneId;
            SceneManager.LoadScene(this.scenes[sceneId], LoadSceneMode.Single);
        }
    }

    public void LoadScene(string scene)
    {
        if (this.scenes.Contains(scene))
        {
            this._currentSceneId = this.scenes.IndexOf(scene);
            SceneManager.LoadScene(scene);
        }
    }
}
