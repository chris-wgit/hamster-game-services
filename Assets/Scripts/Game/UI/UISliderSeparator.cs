using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderSeparator : MonoBehaviour
{
    private Vector2 barSize;
    private int numberOfSeparator;
    public GameObject separatorPrefab;
    private float separatorSpace;
    public int unitPerSpace = 100;

    private void Awake()
    {
        barSize = GetComponent<RectTransform>().rect.size;
    }

    public void SetSeparatorBySpace(int health)
    {
        numberOfSeparator = (health / unitPerSpace);
        separatorSpace = barSize.x*unitPerSpace / health;
        for (int i = 1; i < numberOfSeparator+1; i++)
        {
            GameObject separator = Instantiate(separatorPrefab, transform);
            RectTransform rect = separator.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(i * separatorSpace, 0, 0);
        }
    }

    public void SetSeperatorByNumber(int number)
    {
        separatorSpace = barSize.x / number;
        for (int i = 1; i < number; i++)
        {
            GameObject separator = Instantiate(separatorPrefab, transform);
            RectTransform rectTransform = separator.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, barSize.y);
            rectTransform.anchoredPosition = new Vector3(i * separatorSpace, 0, 0);
        }
    }

}
