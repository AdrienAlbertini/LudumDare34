using UnityEngine;
using System.Collections;

public class ParallaxBG : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    public float tileSizeZ = 0.0f;

    private Vector3 _startPosition = Vector3.zero;

    void Start()
    {
        this._startPosition = transform.position;
    }

    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * this.scrollSpeed, this.tileSizeZ);
        transform.position = this._startPosition + Vector3.forward * newPos;
    }
}
