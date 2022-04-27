using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMenuAnim : MonoBehaviour
{
    public GameObject SocialPanel;

    public void ShowHideMenu()
    {
        if(SocialPanel != null)
        {
            Animator animator = SocialPanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }

    }
}
