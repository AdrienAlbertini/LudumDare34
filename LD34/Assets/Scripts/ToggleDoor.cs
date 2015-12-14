using UnityEngine;
using System.Collections;
using System;

public class ToggleDoor : MonoBehaviour, PressurePlateListener
{
    public enum dir { UP, DOWN, LEFT, RIGHT };

    public float speed = 1.0f;
    public float height = 3.0f;
    public dir direction = dir.UP;
    public bool isBackAndForth = false;

    private float _step;
    private bool _isPlayerBlocking = false;
    private Vector3 _initialPosition;
    private Vector3 _vecDir = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        _initialPosition = transform.position;
        PlayerTrigger[] triggers = this.GetComponentsInChildren<PlayerTrigger>();

        foreach (PlayerTrigger trigger in triggers)
        {
            trigger.onPlayerTrigger += this._OnPlayerBlock;
            trigger.onPlayerUnTrigger += this._OnPlayerUnBlock;
        }
        Debug.Log("TriggersNb: " + triggers.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPressurePlateTriggerOut(PressurePlate sender)
    {
        if (this.isBackAndForth)
            StartCoroutine(this.MoveDoor(_initialPosition));
    }

    public void OnPressurePlateTriggerIn(PressurePlate sender)
    {
        this._vecDir = Vector3.zero;

        switch (this.direction)
        {
            case dir.UP:
                {
                    this._vecDir = new Vector3(0f, height, 0f);
                    break;
                }
            case dir.DOWN:
                {
                    this._vecDir = new Vector3(0f, -height, 0f);
                    break;
                }
            case dir.LEFT:
                {
                    this._vecDir = new Vector3(-height, 0f, 0f);
                    break;
                }
            case dir.RIGHT:
                {
                    this._vecDir = new Vector3(height, 0f, 0f);
                    break;
                }
        }

        Vector3 endPos = transform.position + this._vecDir;
        Debug.Log("TRIGGER");
        StartCoroutine(this.MoveDoor(endPos));
    }

    private IEnumerator MoveDoor(Vector3 endPos)
    {
        float t = 0f;
        Vector3 startPos = transform.position;
        while (Vector3.Distance(transform.position, endPos) > 0.0f)
        {
            if (!this._isPlayerBlocking)
            {
                t += speed * Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, t);
            }
            else
            {
                Debug.Log("DeltaTime: " + Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void _OnPlayerBlock(object sender, EventArgs e)
    {
        PlayerTrigger playerTrigger = (PlayerTrigger)sender;

        Debug.Log("Vec1: " + ((playerTrigger.transform.position - this.transform.position).normalized)
            + " | Vec2: " + this._vecDir.normalized);
        if ((playerTrigger.transform.position - this.transform.position).normalized == this._vecDir.normalized)
        {
            Debug.Log("PlayerBlock");
            this._isPlayerBlocking = true;
        }
    }
    private void _OnPlayerUnBlock(object sender, EventArgs e)
    {
        Debug.Log("PlayerUnBlock");
        this._isPlayerBlocking = false;
    }
}
