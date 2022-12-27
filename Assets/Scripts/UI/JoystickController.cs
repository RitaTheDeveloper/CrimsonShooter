using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour
{
    [HideInInspector]
    public float speed = 0f;

    [HideInInspector]
    public Vector2 direction = Vector2.zero;

    [SerializeField]
    Sprite activeSprite;

    [SerializeField]
    Sprite idleSprite;

    Vector2 startPosition;
    Vector2 position;

    Image image;
    RectTransform rectTransform;
   
    float maxAllowedSize = 50f;

    private void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        position = Vector2.zero;
        startPosition = Vector2.zero;
    }

    public void OnPointerDown(BaseEventData data)
    {
        PointerEventData pntr = (PointerEventData)data;
        startPosition = pntr.position;
        image.sprite = activeSprite;
    }

    public void OnDrag(BaseEventData data)
    {
        PointerEventData pntr = (PointerEventData)data;
        position = pntr.position - startPosition;
        float size = position.magnitude;
        if (size > maxAllowedSize)
        {
            speed = 1f;
            position = position / size * maxAllowedSize;
        }
        else
            speed = size / maxAllowedSize;

        direction = position / size;
        rectTransform.localPosition = position;
    }

    public void OnPointerUp(BaseEventData data)
    {
        speed = 0f;
        direction = Vector2.zero;
        rectTransform.localPosition = Vector2.zero;
        image.sprite = idleSprite;
    }
}
