using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public List<string> scenes = new List<string>();
    private int _currentSceneId = 0;
    private bool _isNextSceneLoaded = false;
    private AsyncOperation _asyncOp;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    IEnumerator Preloader()
    {
        this._asyncOp.allowSceneActivation = false;
        this._asyncOp = SceneManager.LoadSceneAsync(this.scenes[_currentSceneId + 1], LoadSceneMode.Additive);
        yield return this._asyncOp;
    }

    public void PreloadNextScene()
    {
        StartCoroutine(this.Preloader());
    }

    public void LoadNextScene()
    {
        this._asyncOp.allowSceneActivation = true;
    }

    public void LoadScene(int sceneId)
    {
        if (sceneId >= 0 && sceneId < this.scenes.Count)
        {
            SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        }
    }

    public void LoadScene(string scene)
    {
        if (this.scenes.Contains(scene))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
