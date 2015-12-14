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
    private Vector3 _currentEndPos;

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
        Vector3 vecDir = Vector3.zero;

        switch (this.direction)
        {
            case dir.UP:
                {
                    vecDir = new Vector3(0f, height, 0f);
                    break;
                }
            case dir.DOWN:
                {
                    vecDir = new Vector3(0f, -height, 0f);
                    break;
                }
            case dir.LEFT:
                {
                    vecDir = new Vector3(-height, 0f, 0f);
                    break;
                }
            case dir.RIGHT:
                {
                    vecDir = new Vector3(height, 0f, 0f);
                    break;
                }
        }

        Vector3 endPos = transform.position + vecDir;
        StartCoroutine(this.MoveDoor(endPos));
    }

    private IEnumerator MoveDoor(Vector3 endPos)
    {
        float t = 0f;
        Vector3 startPos = transform.position;
        this._currentEndPos = endPos;
        while (Vector3.Distance(startPos, this._currentEndPos) > 0.0f)
        {
            if (!this._isPlayerBlocking)
            {
                t += speed * Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, this._currentEndPos, t);
            }
            yield return new WaitForEndOfFrame();

        }
    }

    private void _OnPlayerBlock(object sender, EventArgs e)
    {
        PlayerTrigger playerTrigger = (PlayerTrigger)sender;

        if (playerTrigger.transform.forward.normalized == (this._currentEndPos - this.transform.position).normalized)
        {
            Debug.Log("PLAYER BLOCK");
            this._isPlayerBlocking = true;
        }
    }
    private void _OnPlayerUnBlock(object sender, EventArgs e)
    {
        this._isPlayerBlocking = false;
    }
}
