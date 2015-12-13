using UnityEngine;
using System.Collections;

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
    public AudioSource audioSource;
    public AudioClip PlayerAExplosion;
    public AudioClip PlayerBExplosion;
    public AudioClip PlayerADeath;
    public AudioClip PlayerBDeath;

    // Use this for initialization
    void Start()
    {
        this.SizePlayerA = ((MaxSize - MinSize) / 2.0f) + MinSize;
        this.SizePlayerB = this.SizePlayerA;
    }

    private float _explosion = 0;
    private bool _hasGrown = false;

    // Update is called once per frame
    void Update()
    {
        if (this._hasGrown && (this.SizePlayerA == MaxSize || this.SizePlayerB == MaxSize))
        {
            this._explosion += Time.deltaTime;
            if (this._explosion > 1.0f)
            {
                GameObject particles = null;
                if (this.SizePlayerA == MaxSize)
                {
                    particles = GameObject.Instantiate(this.misterFeathersPrefab);
                    particles.transform.position = this.PlayerA.transform.position;
                    this.audioSource.PlayOneShot(this.PlayerAExplosion);
                    this.audioSource.PlayOneShot(this.PlayerADeath);
                    //   this.PlayerAExplosion.Play();
                    GameObject.Destroy(this.PlayerA.transform.parent.gameObject);
                }
                else
                {
                    particles = GameObject.Instantiate(this.ladyFeathersPrefab);
                    particles.transform.position = this.PlayerB.transform.position;
                    this.audioSource.PlayOneShot(this.PlayerBExplosion);
                    this.audioSource.PlayOneShot(this.PlayerBDeath);
                    //  this.PlayerBExplosion.Play();
                    GameObject.Destroy(this.PlayerB.transform.parent.gameObject);
                }
                Debug.Log("Explosion!");
            }
        }
        else
            this._explosion = 0;
        this._hasGrown = false;
    }

    public void Grow(bool _IsGrowing, bool _IsPlayerA)
    {
        /*if (_IsGrowing == true)
		{*/
        if (!_IsPlayerA && !PlayerA.calculateIfIcanGrow())
            return;
        else if (!PlayerB.calculateIfIcanGrow() && _IsPlayerA)
            return;
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
        }
    }
}
