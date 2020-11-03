using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class SelectDifficultyScript : MonoBehaviour
{
    public string Selected;
    public string Difficulty;
    [SerializeField] private GameObject SelectPuzzleObject;
    private KeywordRecognizer keywordRecognizer;
    [SerializeField] public Dictionary<string, Action> actions = new Dictionary<string, Action>();
    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void OnEnable()
    {
        //gm = GameObject.FindObjectOfType<GameManager>();
        actions.Add("hazor", Back);
        actions.Add("kal", StartGame);
        actions.Add("ragil", StartGame);
        actions.Add("kashe", StartGame);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizeSpeech;
        keywordRecognizer.Start();

    }

    private void StartGame()
    {
        // start game scene and pass selected and difficulty params
        //PuzzleParams.PuzzleName = Selected;
        //PuzzleParams.PuzzleDifficulty = "Easy";
        CleanRecognizer();
        anim.Play("Fadeout Animation");
        Invoke("ChangeScene", 1);

    }
    private void ChangeScene()
    {
        SceneManager.LoadScene("Game screen");
    }


    private void Back()
    {
        CleanRecognizer();
        SelectPuzzleObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Select Difficulty").SetActive(false);
    }

    private void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        if (speech.text != "Back")
            Difficulty = speech.text;
        actions[speech.text].Invoke();
    }

    private void CleanRecognizer()
    {
        keywordRecognizer.Stop();
        keywordRecognizer.Dispose();
        actions.Clear();
    }

}