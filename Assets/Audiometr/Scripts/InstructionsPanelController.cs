using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsPanelController : MonoBehaviour
{

    public delegate void InstructionAction();
    public static InstructionAction OnIntroductionEnd;
    [SerializeField]
    private AudioSource ufoAudioController;


    public void IntroductionEnd()
    {
        ufoAudioController.enabled = false;
        if (OnIntroductionEnd != null)
            OnIntroductionEnd.Invoke();
    }
}
