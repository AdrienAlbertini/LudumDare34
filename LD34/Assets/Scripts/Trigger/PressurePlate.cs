using UnityEngine;
using System.Collections;

public interface PressurePlateListener
{
    void OnPressurePlateTriggered(PressurePlate sender);
}

[RequireComponent(typeof(Collider2D))]
public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private GameObject Target = null;

    private Collider2D _collider;
    private Animation _animation;

    [SerializeField]
    private float RequiredSize = 7f;

    // Use this for initialization
    void Start()
    {
        this._collider = this.GetComponent<Collider2D>();
        this._animation = this.GetComponentInChildren<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.localScale.x >= this.RequiredSize)
        {
            Debug.Log("Pressure plate triggered");
            this._collider.enabled = false;
            if (this._animation == null)
                Debug.LogWarning("No animation on pressure plate");
            else
                this._animation.Play();

            PressurePlateListener[] targets;

            if (this.Target == null)
                Debug.Log("PressurePlate null target");
            else if ((targets = this.Target.GetComponents<PressurePlateListener>()) == null)
                Debug.Log("No PressurePlateListener on target");
            else
                foreach (PressurePlateListener listener in targets)
                {
                    listener.OnPressurePlateTriggered(this);
                }
        }
    }
}
