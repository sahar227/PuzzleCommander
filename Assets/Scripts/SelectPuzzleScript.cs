using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SelectPuzzleScript : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    [SerializeField] public Dictionary<string, Action> actions = new Dictionary<string, Action>();
    [SerializeField] public string Selected;
    [SerializeField] private GameObject MainMenuObject;
    [SerializeField] private GameObject SelectDifficultyObject;
    [SerializeField] private GameObject Puzzles;

    // Start is called before the first frame update
    void OnEnable()
    {
        //gm = GameObject.FindObjectOfType<GameManager>();
        actions.Add("hazor", Back);
        /*foreach (var puzzle in Puzzles.GetComponentsInChildren<Image>())
        {
            var t = puzzle.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
            actions.Add(t, DifficultySelect);
        }*/
        actions.Add("Puzzle ehad", DifficultySelect);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizeSpeech;
        keywordRecognizer.Start();

    }

    private void DifficultySelect()
    {
        CleanRecognizer();
        SelectDifficultyObject.SetActive(true);
        SelectDifficultyObject.GetComponentInChildren<SelectDifficultyScript>().Selected = Selected;
        GameObject.FindGameObjectWithTag("Select Puzzle").SetActive(false);
    }

    private void Back()
    {
        CleanRecognizer();
        MainMenuObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Select Puzzle").SetActive(false);
    }

    private void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        if (speech.text != "Back")
            Selected = speech.text;
        actions[speech.text].Invoke();
    }

    private void CleanRecognizer()
    {
        keywordRecognizer.Stop();
        keywordRecognizer.Dispose();
        actions.Clear();
    }
}