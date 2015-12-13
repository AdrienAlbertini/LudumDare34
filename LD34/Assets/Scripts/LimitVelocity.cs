using UnityEngine;
using System.Collections;

public class LimitVelocity : MonoBehaviour {

	public float maxVelocity = 10.0f;
	Rigidbody2D m_rigibody;
	// Use this for initialization
	void Start () {
	m_rigibody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		 if (m_rigibody.velocity.y < maxVelocity)
		 {
			 Vector2 tmp = m_rigibody.velocity;
			 tmp.y = maxVelocity;
			 m_rigibody.velocity = tmp;
			 
		 }
	}
}