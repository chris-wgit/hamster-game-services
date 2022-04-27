using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITapViewController : MonoBehaviour
{
    public TabGroup[] tabGroups;
    // Start is called before the first frame update
    void Start()
    {
        SelectTabGroup(0);
    }

    public void SelectTabGroup(int index)
    {
        for (int i = 0; i < tabGroups.Length; i++)
        {
            tabGroups[i].ToggleGroup(i == index);
        }
    }
}

[System.Serializable]
public class TabGroup
{
    public GameObject FocusButton;
    public GameObject Information;

    public void ToggleGroup(bool value)
    {
        FocusButton.SetActive(value);
        Information.SetActive(value);
    }
}
