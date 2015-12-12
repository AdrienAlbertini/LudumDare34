using UnityEngine;
using System.Collections;

public class ParallaxBG : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    public float tileSizeX = 0.0f;

    private Vector3 _startPosition = Vector3.zero;

    void Start()
    {
        this._startPosition = transform.position;
    }

    void FixedUpdate()
    {
        float newPos = Mathf.Repeat(Time.time * this.scrollSpeed, this.tileSizeX);
        transform.position = this._startPosition + Vector3.right * newPos;
    }
}
