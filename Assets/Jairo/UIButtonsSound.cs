using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonsSound : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    private bool _enabled = true;

    private void Start()
    {
        if (!GetComponent<Button>().interactable)
        {
            _enabled = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_enabled)
        {
            AudioManager.Instance.PlayButtonClickSound();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_enabled)
        {
            AudioManager.Instance.PlayButtonHoverSound();
        }
    }
}
