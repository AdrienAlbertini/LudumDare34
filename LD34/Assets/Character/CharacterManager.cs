﻿using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour
{

    [HideInInspector]
	public float SizePlayerA;
    [HideInInspector]
    public float SizePLayerB;
	public float MaxSize = 10.0f;
    public float MinSize = 1.0f;
	public Platformer2DUserControl PlayerA;
	public Platformer2DUserControl PlayerB;
	// Use this for initialization
	void Start ()
    {
        this.SizePlayerA = ((MaxSize - MinSize) / 2.0f) + MinSize;
        this.SizePLayerB = this.SizePlayerA;
	}
	
	// Update is called once per frame
	void Update () {
	
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
    if (SizePlayerA < MinSize || SizePLayerB < MinSize)
        {
            Debug.LogWarning("Size is " + SizePlayerA + " " + SizePLayerB);
        }   
		if (_IsGrowing == true)
		{
			if (_IsPlayerA)
			{
                if (SizePlayerA - 0.1f <= MinSize)
                {
                    SizePLayerB = MaxSize;
                    SizePlayerA = MinSize;
                }
                else
                {
                    SizePlayerA -= 0.1f;
                    SizePLayerB += 0.1f;
                }
			}
			else
			{
                if (SizePLayerB - 0.1 <= MinSize)
                {
                    SizePlayerA = MaxSize;
                    SizePLayerB = MinSize;
                }
                else
                {
                    SizePlayerA += 0.1f;
                    SizePLayerB -= 0.1f;
                }
			}
			
			//if (SizePlayerA > MaxSize)
			//{
			//	SizePlayerA = MaxSize;
			//	SizePLayerB = 1.0f;
			//}
			//if (SizePLayerB > MaxSize)
			//{
			//	SizePlayerA = 1.0f;
			//	SizePLayerB = MaxSize;
			//}
			
		}
	}
}
