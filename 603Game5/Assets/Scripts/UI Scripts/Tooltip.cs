using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipToShow;
    public float timeToWait = 0.5f;
    public bool hovered;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        StopAllCoroutines();
        TooltipManager.OnMouseLoseFocus();
    }

    private void ShowMessage()
    {
        TooltipManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);

        ShowMessage();
    }
}
