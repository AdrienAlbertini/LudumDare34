using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour
{

    [SerializeField]
    private float Force;

    private Collider2D _collider;

    // Use this for initialization
    void Start()
    {
        this._collider = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Rigidbody2D body = coll.gameObject.GetComponent<Rigidbody2D>();

        if (body == null)
            Debug.Log("Target no rigid body");
        else
        {
            Vector2 direction = new Vector2(this.transform.position.x, this.transform.position.y) - body.position;
            Vector2 force = direction.normalized * -1 * this.Force;

            body.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
