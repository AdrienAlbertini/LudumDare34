using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

	public float SizePlayerA;
	public float SizePLayerB;
	public float MaxSize = 10.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Grow(bool _IsGrowing, bool _IsPlayerA)
	{
		if (_IsGrowing == true)
		{
			if (_IsPlayerA)
			{
					SizePlayerA += 0.5f;
					SizePLayerB -=0.5f;
			}
			else
			{
					SizePlayerA -= 0.5f;
					SizePLayerB +=0.5f;
			}
			
			if (SizePlayerA > MaxSize)
			{
				SizePlayerA = MaxSize;
				SizePLayerB = 1.0f;
			}
			if (SizePLayerB > MaxSize)
			{
				SizePlayerA = 1.0f;
				SizePLayerB = MaxSize;
			}
			
		}
	}
}
