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

    private IEnumerator _StartLoad(string scene, bool fade)
    {
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
            SaveManager.data.levelID = this._currentSceneId;
            SaveManager.instance.save();
        }
    }
    
    public void LoadScene(int sceneId, bool fade = true)
    {
        Debug.Log("SceneId to load: " + sceneId);
        if (sceneId >= 0 && sceneId < this.scenes.Count)
        {
            Debug.Log("Scene contained");
            this._currentSceneId = sceneId;
            StartCoroutine(this._StartLoad(this.scenes[sceneId], fade));
        }
    }

    public void LoadScene(string scene, bool fade = true)
    {
        Debug.Log("Scene to load: " + scene);
        if (this.scenes.Contains(scene))
        {
            Debug.Log("Scene contained");
            this._currentSceneId = this.scenes.IndexOf(scene);
            StartCoroutine(this._StartLoad(scene, fade));
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
            ++this._currentSceneId;
            this.LoadScene(this._currentSceneId);
        }
    }
}
