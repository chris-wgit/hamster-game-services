using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UICharacterInteractive : MonoBehaviour, IDragHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject targetObject;

    public float rotationSpeed;
    public float rotationDamping;

    private float _rotationVelocity;

    private bool isDrag= false;

    [Serializable]
    public class OnClickEvent : UnityEvent { }

    public OnClickEvent OnClick;

    public void OnBeginDrag(PointerEventData eventData)
    {

    }


    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        _rotationVelocity = eventData.delta.x * rotationSpeed;

        if(targetObject!=null)
            targetObject.transform.Rotate(Vector3.up, -_rotationVelocity, Space.World);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDrag)
        {
            OnClick.Invoke();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
    }

    private void Update()
    {
        if (!isDrag && !Mathf.Approximately(_rotationVelocity, 0))
        {
            float deltaVelocity = Mathf.Min(
                Mathf.Sign(_rotationVelocity) * Time.deltaTime * rotationDamping,
                Mathf.Sign(_rotationVelocity) * _rotationVelocity
            );
            _rotationVelocity -= deltaVelocity;

            if(targetObject != null)
                targetObject.transform.Rotate(Vector3.up, -_rotationVelocity, Space.Self);

        }
    }
}
