using UnityEngine;
using System.Collections;

public class Parallax_BGScroller : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    public bool isVerticalScroll = false;

    private float _tileSize = 0.0f;
    private Vector3 _startPosition = Vector3.zero;
    private SpriteRenderer _renderer;

    void Start()
    {
        GameObject child = new GameObject();
        SpriteRenderer childSPR = child.AddComponent<SpriteRenderer>();
        Vector3 tmp = this.transform.position;
        this._startPosition = transform.position;
        this._renderer = this.GetComponent<SpriteRenderer>();


        // Set tile size
        if (this.isVerticalScroll)
            this._tileSize = this._renderer.sprite.bounds.size.y * this.transform.localScale.y;
        else
            this._tileSize = this._renderer.sprite.bounds.size.x * this.transform.localScale.x;
        
        // set child
        childSPR.sprite = this._renderer.sprite;
        child.transform.localScale = this.transform.localScale;

        if (this.isVerticalScroll)
            tmp.y += this._renderer.sprite.bounds.size.y * this.transform.localScale.y;
        else
            tmp.x -= this._renderer.sprite.bounds.size.x * this.transform.localScale.x;
        child.transform.position = tmp;
        child.transform.SetParent(this.transform);
    }

    void FixedUpdate()
    {
        float newPos = Mathf.Repeat(Time.time * this.scrollSpeed, this._tileSize);

        if (this.isVerticalScroll)
            transform.position = this._startPosition + (Vector3.down * newPos);
        else
            transform.position = this._startPosition + (Vector3.right * newPos);
    }
}
