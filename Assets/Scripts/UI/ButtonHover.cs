using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleIncrease = 1.2f;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
