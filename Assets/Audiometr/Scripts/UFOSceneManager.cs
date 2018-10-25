using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UFOSceneManager : MonoBehaviour
{
    AudioSource audioSource;
    private List<float> textChangeSeconds = new List<float> { 0, 1.2f, 2.5f, 3f, 3.8f, 2.5f };
    [SerializeField]
    private Text instructionsText;
    private int currentInstruction = 0;
    [Range(1, 10)]
    [SerializeField]
    private float sceneTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.5f);
        audioSource.Play();
        StartCoroutine(ChangeInstructionText());

    }

    IEnumerator ChangeInstructionText()
    {
        if (currentInstruction < textChangeSeconds.Count)
        {
            yield return new WaitForSeconds(textChangeSeconds[currentInstruction]);
            instructionsText.text = UFOInstructions.TEXT_INSTRUCTIONS[currentInstruction];
            currentInstruction++;
            StartCoroutine(ChangeInstructionText());
        }
    }

    private void Update()
    {
        Time.timeScale = sceneTime;
        audioSource.pitch = sceneTime;
    }
}

public class UFOInstructions : ScriptableObject
{
    public static List<string> TEXT_INSTRUCTIONS = new List<string>{ " HEJ! ",
    "ZARAZ SCHOWAM SIĘ ZA JEDNĄ Z TYCH ASTEROID",
    "TWOIM ZADANIEM BĘDZIE NASŁUCHIWAĆ MOICH SYGNAŁÓW, ",
   "A NASTĘPNIE WSKAZAĆ ZA KTÓRĄ Z NICH SIĘ UKRYWAM.", "UWAGA...", "NASŁUCHUJĄC MOICH SYGNAŁÓW WSKAŻ, ZA KTÓRĄ ASTEROIDĄ SIĘ UKRYWAM." };

}