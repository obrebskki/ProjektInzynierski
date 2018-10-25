using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PointersManager : MonoBehaviour
{

    public delegate void PointerAction();
    public static event PointerAction OnPointerChange;
    public delegate void ClickAction();
    public static event ClickAction OnHearClicked;
    public static event ClickAction OnNotHearClicked;
    public AudioSource audioSourceOnSceneLeft;
    public AudioSource audioSourceOnSceneRight;

    public List<GameObject> allPointersOnScene;
    public GameObject colorsAnimation;
    public static int currentPointer;
    void Start()
    {
        currentPointer = 0;

    }



    public void LeftButton()
    {
        audioSourceOnSceneLeft.Stop();
        audioSourceOnSceneRight.Stop();
        if (currentPointer > 0)
        {
            currentPointer -= 1;

            if (OnPointerChange != null)
                OnPointerChange.Invoke();
        }

    }

    public void RightButton()
    {
        audioSourceOnSceneLeft.Stop();
        audioSourceOnSceneRight.Stop();

        if (currentPointer < (2 * allPointersOnScene.Count - 1))
        {
            currentPointer++;

            if (OnPointerChange != null)
                OnPointerChange.Invoke();
        }
        if (currentPointer == 2 * allPointersOnScene.Count - 1)
        {
            colorsAnimation.SetActive(true);
        }
    }

    public void OnHearButtonClick()
    {
        if (OnHearClicked != null)
        {
            OnHearClicked();
        }
    }
    public void OnNotHearButtonClick()
    {

        if (OnNotHearClicked != null)
        {
            OnNotHearClicked();
        }
    }

}
