using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKillPopupBehaviour : MonoBehaviour
{
    public IntEventChannelSO OnUpdateEvent;
    [SerializeField] Animator animator;
    [SerializeField] int PopupDelay;

    private void OnEnable()
    {
        OnUpdateEvent.OnEventRaised += ShowPopup;
    }
    private void OnDisable()
    {
        OnUpdateEvent.OnEventRaised -= ShowPopup;
    }

    private void ShowPopup(int value)
    {
        Debug.Log(value);
        StartCoroutine(ShowKillPopup());
    }

    IEnumerator ShowKillPopup()
    {
        animator.Play("Show");
        yield return new WaitForSeconds(PopupDelay);
        animator.Play("Hide");
    }
}
