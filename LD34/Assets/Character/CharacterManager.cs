using UnityEngine;
using System.Collections;
using System;

public class CharacterManager : MonoBehaviour
{

    [HideInInspector]
    public float SizePlayerA;
    [HideInInspector]
    public float SizePlayerB;
    public float MaxSize = 10.0f;
    public float MinSize = 1.0f;
    public Platformer2DUserControl PlayerA;
    public Platformer2DUserControl PlayerB;
    public GameObject misterFeathersPrefab;
    public GameObject ladyFeathersPrefab;
    public AudioClip growSound;
    public AudioClip growLimitSound;
    private AudioSource _audioSource;

    // Use this for initialization
    void Start()
    {
        this.SizePlayerA = ((MaxSize - MinSize) / 2.0f) + MinSize;
        this.SizePlayerB = this.SizePlayerA;
        this._audioSource = this.GetComponent<AudioSource>();
        this.PlayerA.growStart += this._OnGrow;
        this.PlayerA.growOver += this._OnGrowEnd;
        this.PlayerB.growStart += this._OnGrow;
        this.PlayerB.growOver += this._OnGrowEnd;

        Debug.Log("PLAY");
    }

    private float _explosion = 0;
    private bool _hasGrown = false;

    // Update is called once per frame
    void Update()
    {
        if (this._hasGrown && (this.SizePlayerA == MaxSize || this.SizePlayerB == MaxSize))
        {
            if (this._explosion <= 0.0f)
            {
                this._audioSource.clip = this.growLimitSound;
                this._audioSource.Play();
            }
            this._explosion += Time.deltaTime;
            if (this._explosion > 1.0f)
            {
                if (this.SizePlayerA == MaxSize && PlayerA != null)
                {
                    this.KillPlayer(this.PlayerA);
                }
                else if (this.SizePlayerB == MaxSize && PlayerB != null)
                {
                    this.KillPlayer(this.PlayerB);
                }
                Debug.Log("Explosion!");
            }
        }
        else
        {
            this._explosion = 0;
        }
        this._hasGrown = false;
    }

    public void KillPlayer(Platformer2DUserControl player)
    {
        if (player != null)
        {
            GameObject particles = null;
            if (player.IsPLayerA)
            {
                particles = GameObject.Instantiate(this.misterFeathersPrefab);
                particles.transform.position = this.PlayerA.transform.position;
                AudioManager.Instance.PlaySound("MisterExplosion");
                AudioManager.Instance.PlaySound("MisterDeath");
                this._audioSource.Stop();
                GameObject.Destroy(this.PlayerA.transform.parent.gameObject);
                LevelsManager.Instance.ReloadScene(true, 1.0f);
            }
            else
            {
                particles = GameObject.Instantiate(this.ladyFeathersPrefab);
                particles.transform.position = this.PlayerB.transform.position;
                AudioManager.Instance.PlaySound("LadyExplosion");
                AudioManager.Instance.PlaySound("LadyDeath");
                this._audioSource.Stop();
                GameObject.Destroy(this.PlayerB.transform.parent.gameObject);
                LevelsManager.Instance.ReloadScene(true, 1.0f);
            }
        }
    }

    private void _OnGrow(object sender, EventArgs e)
    {
        Debug.Log("OnGrow");
        this._audioSource.Stop();
        this._audioSource.clip = this.growSound;
        this._audioSource.Play();
    }
    private void _OnGrowEnd(object sender, EventArgs e)
    {
        Debug.Log("OnGrowEnd");
        if (PlayerA != null && PlayerB != null && PlayerA.Grow == false && PlayerB.Grow == false)
            this._audioSource.Stop();
    }

    public bool Grow(bool _IsGrowing, bool _IsPlayerA)
    {
        /*if (_IsGrowing == true)
		{*/
        if (!_IsPlayerA && !PlayerA.calculateIfIcanGrow())
            return false;
        else if (!PlayerB.calculateIfIcanGrow() && _IsPlayerA)
            return false;
        //	}
        if (SizePlayerA < MinSize || SizePlayerB < MinSize)
        {
            Debug.LogWarning("Size is " + SizePlayerA + " " + SizePlayerB);
        }
        if (_IsGrowing == true)
        {
            this._hasGrown = true;
            if (_IsPlayerA)
            {
                if (SizePlayerA - 0.1f <= MinSize)
                {
                    SizePlayerB = MaxSize;
                    SizePlayerA = MinSize;
                }
                else
                {
                    SizePlayerA -= 0.1f;
                    SizePlayerB += 0.1f;
                }
            }
            else
            {
                if (SizePlayerB - 0.1 <= MinSize)
                {
                    SizePlayerA = MaxSize;
                    SizePlayerB = MinSize;
                }
                else
                {
                    SizePlayerA += 0.1f;
                    SizePlayerB -= 0.1f;
                }
            }
            return true;
        }
        return false;
    }
}
