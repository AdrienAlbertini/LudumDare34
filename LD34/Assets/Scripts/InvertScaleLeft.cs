using UnityEngine;
using System.Collections;

public class InvertScaleLeft : MonoBehaviour {
	Vector2 pos;
	// Use this for initialization
	void Start () {
		pos = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 scale;
		Vector2 poscalc;
		scale.x = 0.0645f  / this.transform.parent.gameObject.transform.localScale.x;
		scale.y = 0.0645f / this.transform.parent.gameObject.transform.localScale.x;
		poscalc = pos;
		poscalc.x += this.transform.parent.gameObject.transform.localScale.x * 0.08f;
		this.transform.localPosition = poscalc;
		this.transform.localScale = scale;
	}
}
