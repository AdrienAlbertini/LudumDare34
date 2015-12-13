using UnityEngine;
using System.Collections;

public class ToggleDoor : MonoBehaviour, PressurePlateListener
{
    public enum dir { UP, DOWN, LEFT, RIGHT };

    [SerializeField]
    public float speed = 3.0f;
    [SerializeField]
    public float height = 3.0f;
    [SerializeField]
    public dir direction = dir.UP;

    private float _step;
    private Vector3 _initialPosition;

	// Use this for initialization
	void Start () {
        _initialPosition = transform.position;
        _step = speed * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPressurePlateTriggered(PressurePlate sender)
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
        StartCoroutine("MoveDoor", _initialPosition);
    }

    public void OnPressurePlateTriggered(PressurePlate sender)
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
        StartCoroutine("MoveDoor", _initialPosition);
    }

    IEnumerator MoveDoor(Vector3 endPos)
    {
        float t = 0f;
        Vector3 startPos = transform.position;
        while (t < 1f)
        {
            t += _step;
            transform.position = Vector3.Slerp(startPos, endPos, t);
            yield return null;
        }
    }
}
