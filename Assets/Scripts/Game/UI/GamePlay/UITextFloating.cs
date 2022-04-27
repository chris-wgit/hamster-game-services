using TMPro;
using UnityEngine;
using DG.Tweening;

public class UITextFloating : MonoBehaviour
{
    public TextMeshProUGUI text;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void OnSpawn()
    {
        DoMovement();
    }

    public void SetText(int value)
    {
        text.text = value.ToString();
    }

    private void DoMovement()
    {
        rect.DOAnchorPosY(Random.Range(50,70), 1);
        rect.DOAnchorPosX(Random.Range(-30, 30), 1);
        rect.DOScale(1, 0.5f);
    }
    public void OnDespawn()
    {
        rect.localPosition = Vector3.zero;
        rect.localScale = Vector3.zero;
    }

}