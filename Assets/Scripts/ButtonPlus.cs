using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonPlus : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    public UnityEvent OnPress;
    public UnityEvent OnClick;
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) OnPress.Invoke();
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) OnClick.Invoke();
    }
}
