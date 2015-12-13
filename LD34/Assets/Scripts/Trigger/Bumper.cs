using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Bumper : MonoBehaviour
{

    [SerializeField]
    private float Force = 1;
    public bool bumperOn;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (!bumperOn)
            return;
        Rigidbody2D body = coll.gameObject.GetComponent<Rigidbody2D>();

        if (body == null)
            Debug.Log("Target no rigid body");
        else
        {
            //Vector2 direction = new Vector2(this.transform.position.x, this.transform.position.y) - body.position;
            //Vector2 force = direction.normalized * -1 * this.Force;

            Vector2 force = new Vector2(0, this.Force);

            body.velocity = force;

            //body.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
