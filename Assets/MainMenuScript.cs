using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class MainMenuScript : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    [SerializeField] private GameObject SelectPuzzleObject;

    // Start is called before the first frame update
    void OnEnable()
    {
        //gm = GameObject.FindObjectOfType<GameManager>();
        actions.Add("bhar puzzle", SelectPuzzle);
        actions.Add("ho ra ot", Instructions);
        actions.Add("sayem mishak", QuitGame);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizeSpeech;
        keywordRecognizer.Start();

    }

    private void QuitGame()
    {
        CleanRecognizer();
        // TBD: Add "BYE" sound
        Application.Quit();
    }

    private void Instructions()
    {
        throw new NotImplementedException();
    }

    private void SelectPuzzle()
    {
        CleanRecognizer();
        SelectPuzzleObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Main Menu").SetActive(false);
    }

    private void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void CleanRecognizer()
    {
        keywordRecognizer.Stop();
        keywordRecognizer.Dispose();
        actions.Clear();
    }
}