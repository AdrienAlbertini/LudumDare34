using System;
using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
    public EventHandler onPlayerTrigger;
    public EventHandler onPlayerUnTrigger;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") &&  this.onPlayerTrigger != null)
            this.onPlayerTrigger(this, EventArgs.Empty);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") &&  this.onPlayerUnTrigger != null)
            this.onPlayerUnTrigger(this, EventArgs.Empty);
    }
}
