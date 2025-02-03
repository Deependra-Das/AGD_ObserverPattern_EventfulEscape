using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjectView : MonoBehaviour
{
    public float maxHeight = 1.5f;
    public float floatSpeed = 0.5f;
    private float originalHeight;
    private bool isFloating = false;

    private void OnEnable()
    {
        EventService.Instance.OnObjectFloatingEvent.AddListener(OnStartObjectFloating);
    }

    private void OnDisable()
    {
        EventService.Instance.OnObjectFloatingEvent.RemoveListener(OnStartObjectFloating);
    }

    private void Start()
    {
        originalHeight = transform.position.y;
    }

    void FixedUpdate()
    {
        if (isFloating)
        {
            if (transform.position.y < originalHeight + maxHeight)
            {
                transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, originalHeight + maxHeight, transform.position.z);
            }
        }
    }

    public void OnStartObjectFloating()
    {
        if (!isFloating)
        {
            isFloating = true;
        }
    }

    public void OnStopObjectFloating()
    {
        if (isFloating)
        {
            isFloating = false;
            transform.position = new Vector3(transform.position.x, originalHeight, transform.position.z);
        }
    }

}
