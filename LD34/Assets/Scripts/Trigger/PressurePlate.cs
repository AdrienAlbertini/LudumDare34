using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface PressurePlateListener
{
    void OnPressurePlateTriggerIn(PressurePlate sender);
    void OnPressurePlateTriggerOut(PressurePlate sender);
}

[RequireComponent(typeof(Collider2D))]
public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> Targets = null;

    private Collider2D _collider;
    private Animation _animation;

    [SerializeField]
    private float RequiredSize = 7f;

    [SerializeField]
    private bool UseOnce = true;

    private bool _triggered = false;

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

    void OnTriggerStay2D(Collider2D other)
    {
        if (!this._triggered)
            this.CheckTrigger(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (this._triggered)
            this.Untrigger(other);
    }

    private void Untrigger(Collider2D other)
    {
        this._triggered = false;

        Debug.Log("Pressure plate untriggered");
        if (this._animation == null || this._animation.GetClip("PressurePlateMoveUp") == null)
            Debug.LogWarning("No animation on pressure plate");
        else
            this._animation.Play("PressurePlateMoveUp");

        foreach (GameObject Target in this.Targets)
        {
            PressurePlateListener listener = Target.GetComponent<PressurePlateListener>();

            if (listener != null)
                listener.OnPressurePlateTriggerOut(this);
        }
    }

    private void CheckTrigger(Collider2D other)
    {
        if (other.gameObject.transform.localScale.x >= this.RequiredSize)
        {
            this._triggered = true;

            Debug.Log("Pressure plate triggered");
            if (this._animation == null || this._animation.GetClip("PressurePlateMoveDown") == null)
                Debug.LogWarning("No animation on pressure plate");
            else
                this._animation.Play("PressurePlateMoveDown");

            //foreach (PressurePlateListener Target in this.Targets)
            //{
            //    Target.OnPressurePlateTriggerIn(this);
            //}
            foreach (GameObject Target in this.Targets)
            {
                PressurePlateListener listener = Target.GetComponent<PressurePlateListener>();

                if (listener != null)
                    listener.OnPressurePlateTriggerIn(this);
            }

            if (this.UseOnce)
                this.enabled = false;
        }
    }
}
