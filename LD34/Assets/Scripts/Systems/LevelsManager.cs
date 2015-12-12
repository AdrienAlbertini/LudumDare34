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
    
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ReloadScene()
    {
        this.StartCoroutine(this.LoadScene(this._currentSceneId));
    }

    public void SwitchToNextScene()
    {
        if (this._currentSceneId >= 0 && this._currentSceneId < this.scenes.Count)
        {
            ++this._currentSceneId;
            this.StartCoroutine(this.LoadScene(this._currentSceneId));
        }
    }

    public IEnumerator LoadScene(int sceneId, bool fade = true)
    {
        if (sceneId >= 0 && sceneId < this.scenes.Count)
        {
            this._currentSceneId = sceneId;
            if (fade)
            {
                SceneFader.Instance.EndScene();
                yield return new WaitForSeconds(SceneFader.Instance.FadeSpeed);
            }
            SceneManager.LoadScene(this.scenes[sceneId], LoadSceneMode.Single);
            if (fade)
            {
                SceneFader.Instance.StartScene();
                yield return new WaitForSeconds(SceneFader.Instance.FadeSpeed);

            }
        }
    }

    public IEnumerator LoadScene(string scene, bool fade = true)
    {
        if (this.scenes.Contains(scene))
        {
            this._currentSceneId = this.scenes.IndexOf(scene);
            if (fade)
            {
                SceneFader.Instance.EndScene();
                yield return new WaitForSeconds(SceneFader.Instance.FadeSpeed);
            }
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
            if (fade)
            {
                SceneFader.Instance.StartScene();
                yield return new WaitForSeconds(SceneFader.Instance.FadeSpeed);

            }
        } 
    }
}
