using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponLibarary : MonoBehaviour
{
    public WeaponLibrarySO library;

    public Transform contentTransform;
    public GameObject uiWeaponPrefab;

    public GridLayoutGroup gridLayout;
    public ContentSizeFitter sizeFilter;
    
    void Start()
    {
        InstantiateWeapon();
    }

    void InstantiateWeapon()
    {
        foreach (var item in library.WeaponList)
        {
            GameObject itemObject = Instantiate(uiWeaponPrefab, contentTransform);
            UIWeapon uiWeapon = itemObject.GetComponent<UIWeapon>();
            uiWeapon.InitData(item.Value);
        }
    }

    private void OnDisable()
    {
        sizeFilter.enabled = false;
        gridLayout.enabled = false;
    }
}
