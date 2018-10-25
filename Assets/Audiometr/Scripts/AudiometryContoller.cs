using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiometryContoller : MonoBehaviour
{

    [SerializeField]
    private GameObject ufo;
    [SerializeField]
    private List<AudioClip> leftEarClips;
    [SerializeField]
    private List<AudioClip> rightEarClips;
    [SerializeField]
    private AudioClip brawoClip;
    [SerializeField]
    private AudioClip pomylkaClip;

    [SerializeField]
    private AudioSource leftAudioSource;
    [SerializeField]
    private AudioSource rightAudioSource;
    [SerializeField]
    private Animator instructionsAnimator;

    AudioSource ufoAudioSource;
    [SerializeField]
    private AudioSource narrationAudioSource;
    [SerializeField]
    private Animator ufoAnimator;
    private EAR_SIDE currentSide;
    public static EAR_SIDE currentEarTested;

    private void Awake()
    {
        ufoAudioSource = ufo.GetComponent<AudioSource>();
        // ufoAnimator = ufo.GetComponent<Animator>();

    }

    private void OnEnable()
    {
        InstructionsPanelController.OnIntroductionEnd += TestIteration;
    }


    void TestIteration()
    {
        ufoAudioSource.enabled = false;
        ufo.SetActive(false);
        AudioClip clipToAssign;
        currentSide = EAR_SIDE.NONE;
        if (leftEarClips.Count > 0 && rightEarClips.Count > 0)
        {
            currentSide = Random.Range(0, 100) > 50 ? EAR_SIDE.LEFT : EAR_SIDE.RIGHT;
        }
        else if (leftEarClips.Count > 0)
        {
            currentSide = EAR_SIDE.LEFT;
        }
        else if (rightEarClips.Count > 0)
        {
            currentSide = EAR_SIDE.RIGHT;
        }

        switch (currentSide)
        {
            case EAR_SIDE.LEFT:
                clipToAssign = leftEarClips[Random.Range(0, leftEarClips.Count)];
                leftEarClips.Remove(clipToAssign);
                leftAudioSource.clip = clipToAssign;
                leftAudioSource.Play();
                break;
            case EAR_SIDE.RIGHT:
                clipToAssign = rightEarClips[Random.Range(0, leftEarClips.Count)];
                rightEarClips.Remove(clipToAssign);
                rightAudioSource.clip = clipToAssign;
                rightAudioSource.Play();
                break;
            case EAR_SIDE.NONE:
                break;
        }
    }


    public void LeftAsteroidAnswer()
    {
        rightAudioSource.clip = null;
        leftAudioSource.clip = null;
        ufo.SetActive(true);
        if (currentSide == EAR_SIDE.LEFT)
        {
            ufoAnimator.Play("UFO_COMING_OUT_RIGHT");

            GoodAnswer();
        }
        else
        {
            ufoAnimator.Play("UFO_COMING_OUT_LEFT");
            BadAnswer();
        }
    }

    public void RightAsteroidAnswer()
    {
        leftAudioSource.clip = null;
        rightAudioSource.clip = null;
        ufo.SetActive(true);
        if (currentSide == EAR_SIDE.RIGHT)
        {
            ufoAnimator.Play("UFO_COMING_OUT_LEFT");

            GoodAnswer();
        }
        else
        {
            ufoAnimator.Play("UFO_COMING_OUT_RIGHT");
            BadAnswer();
        }
    }

    void GoodAnswer()
    {
        //  ufoAudioSource.enabled = true;
        narrationAudioSource.clip = brawoClip;
        narrationAudioSource.Play();
        StartCoroutine(NextTestIteration());

    }

    void BadAnswer()
    {
        //  ufoAudioSource.enabled = true;
        narrationAudioSource.clip = pomylkaClip;
        narrationAudioSource.Play();
        StartCoroutine(NextTestIteration());

    }

    IEnumerator NextTestIteration()
    {
        yield return new WaitForSeconds(2.5f);
        instructionsAnimator.Play("FADE_OUT");
        yield return new WaitForSeconds(1);

        TestIteration();
    }
}

public enum EAR_SIDE { LEFT, RIGHT, NONE }