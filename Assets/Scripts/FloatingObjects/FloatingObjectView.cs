using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjectView : MonoBehaviour
{
    public float floatHeight = 1.5f;
    public float floatSpeed = 1f;
    public float dangleSpeed = 1f;
    private Vector3 originalPosition;
    private bool isFloating = false;
    private bool isDangle = false;

    private void OnEnable()
    {
        EventService.Instance.OnObjectFloatingEvent.AddListener(OnStartObjectFloating);
        EventService.Instance.OnObjectFallingEvent.AddListener(OnStopObjectFloating);
    }

    private void OnDisable()
    {
        EventService.Instance.OnObjectFloatingEvent.RemoveListener(OnStartObjectFloating);
        EventService.Instance.OnObjectFallingEvent.RemoveListener(OnStopObjectFloating);
    }

    private void Start()
    {
        originalPosition = transform.position;

    }

    private void Update()
    {
        if (isFloating)
        {
            if (transform.position.y < originalPosition.y + floatHeight)
            {
                transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
            }
            else
            {
                isDangle = true;
                isFloating = false;
            }
        }

        if (isDangle)
        {
            float yPosition = Mathf.Sin(Time.time * dangleSpeed) * 0.75f + (originalPosition.y + floatHeight);
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        }
    }

    public void OnStartObjectFloating()
    {
        if (!isFloating && !isDangle)
        {
            isFloating = true;
        }
    }

    public void OnStopObjectFloating()
    {
        if (isDangle)
        {
            isDangle = false;
            StartCoroutine(FallBackToOriginalPosition());
        }
    }
    private IEnumerator FallBackToOriginalPosition()
    {
        float elapsedTime = 0f;
        float duration = 0.5f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, originalPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.ObjectFalling);
    }

}
