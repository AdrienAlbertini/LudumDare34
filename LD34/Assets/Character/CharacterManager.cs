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
