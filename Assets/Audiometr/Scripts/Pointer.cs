using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pointer : MonoBehaviour
{

    public List<GameObject> pathPoints;
    private int currentTableIterator;
    public AudioSource audioSourceOnScene;
    private int currentToneIterator;
    public List<AudioClip> pointersAudioClips;


    public int pointerID;
    private bool isBeeingTested;

    void Start()
    {
        currentTableIterator = 4;
        currentToneIterator = 7;
        isBeeingTested = false;
        CheckIfBeeingTested();

    }

    void OnEnable()
    {
        PointersManager.OnHearClicked += UserDidHear;
        PointersManager.OnNotHearClicked += UserDidntHear;
        PointersManager.OnPointerChange += PointerChange;
        PointersManager.OnPointerChange += CheckIfBeeingTested;

    }

    void CheckIfBeeingTested()
    {
        if (PointersManager.currentPointer == pointerID)
        {
            isBeeingTested = true;
            audioSourceOnScene.clip = pointersAudioClips[currentToneIterator];
            audioSourceOnScene.Play();
        }
    }



    void PointerChange()
    {
        
        if (PointersManager.currentPointer == pointerID)
        {
            gameObject.GetComponent<Image>().enabled = true;
            isBeeingTested = true;
        }
        else
        {
          //  gameObject.GetComponent<Image>().enabled = false;
            isBeeingTested = false;

        }
    }

    public void UserDidHear()
    {
        if (isBeeingTested)
        {
            if (currentTableIterator < pathPoints.Count)
            {
                currentTableIterator++;
            }
            if (currentTableIterator < pathPoints.Count)
            {
                gameObject.transform.position = pathPoints[currentTableIterator].transform.position;
            }

            if (currentToneIterator > 0)
            {
                currentToneIterator--;
            }
            if (currentToneIterator >= 0)
            {
                audioSourceOnScene.clip = pointersAudioClips[currentToneIterator];
                audioSourceOnScene.Play();
            }
        }
    }

    public void UserDidntHear()
    {
        if (isBeeingTested)
        {
            if (currentTableIterator > 0)
            {
                currentTableIterator--;
            }

            if (currentTableIterator >= 0)
            {
                gameObject.transform.position = pathPoints[currentTableIterator].transform.position;

            }

            if (currentToneIterator < pointersAudioClips.Count)
            {
                currentToneIterator++;
            }
            if (currentToneIterator < pointersAudioClips.Count)
            {
                audioSourceOnScene.clip = pointersAudioClips[currentToneIterator];
                audioSourceOnScene.Play();

            }
            Debug.Log("Currently played audio file: " + audioSourceOnScene.clip.name);

        }

    }


}
