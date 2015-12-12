using UnityEngine;
using System.Collections;

public interface PressurePlateListener
{
    void OnPressurePlateTriggered(PressurePlate sender);
}

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private GameObject Target = null;

    private Collider2D _collider;
    private Animation _animation;

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
        Debug.Log("Pressure plate triggered");
        this._collider.enabled = false;
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
