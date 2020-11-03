using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class GameInput : MonoBehaviour
{
    private GrammarRecognizer grMove;
    private GrammarRecognizer grTake;

    private GameScript gm;
    private Dictionary<string, char> LettersTranslation;
    private Dictionary<string, string> NumbersTranslation;

    private KeywordRecognizer keywordRecognizer;
    private KeywordRecognizer keywordRecognizerHelp;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    [SerializeField] private GameObject HelpScreen;
    [SerializeField] private GameObject VictoryUI;

    [SerializeField] private TextMeshProUGUI HelpText;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameScript>();

        LettersTranslation = new Dictionary<string, char>
        {
            { "aigul adom", 'A' },
            { "ribua adom", 'B' },
            { "meshulash adom", 'C' },
            { "aigul yarok", 'D' },
            { "ribua yarok", 'E' },
            { "meshulash yarok", 'F' },
            { "aigul kahol", 'G' },
            { "ribua kahol", 'H' },
            { "meshulash kahol", 'I' }
        };

        NumbersTranslation = new Dictionary<string, string>
        {
            {"zero", "0" },
            { "Ehad", "1" },
            { "Shtaim", "2" },
            { "Shalosh", "3" },
            { "Arba", "4" },
            { "Hamesh", "5" },
            { "Shesh", "6" },
            { "Sheva", "7" },
            { "Shmone", "8" },
            { "Tesha", "9" },
        };

        actions.Add("Ezra", HelpCommand);
        actions.Add("hathel mehadash", RestartCommand);
        actions.Add("tafrit rashi", MainMenu);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        string[] rg = { "hazor lamishak" };
        keywordRecognizerHelp = new KeywordRecognizer(rg, ConfidenceLevel.Low);

        keywordRecognizer.OnPhraseRecognized += RecognizeSpeechPhrase;
        keywordRecognizerHelp.OnPhraseRecognized += ResumeGame;


        grMove = new GrammarRecognizer(Application.streamingAssetsPath + "/Grammars/GrammarMoveHebrew.xml", ConfidenceLevel.Low);
        grTake = new GrammarRecognizer(Application.streamingAssetsPath + "/Grammars/GrammarTakeHeb.xml", ConfidenceLevel.Low);

        grMove.OnPhraseRecognized += RecognizeSpeech;
        grTake.OnPhraseRecognized += RecognizeSpeech;

        StartRecognizers();
    }

    private void ResumeGame(PhraseRecognizedEventArgs args)
    {
        StartRecognizers();
        HelpText.gameObject.SetActive(true);
        HelpScreen.SetActive(false);
        keywordRecognizerHelp.Stop();
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void RestartCommand()
    {
        SceneManager.LoadScene("Game screen");

    }

    private void HelpCommand()
    {
        StopRecognizers();
        HelpText.gameObject.SetActive(false);
        HelpScreen.SetActive(true);
        keywordRecognizerHelp.Start();
        //throw new NotImplementedException();
    }

    private void RecognizeSpeechPhrase(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        try
        {
            Debug.Log(speech.text);
            var tokens = speech.text.Split();
            switch (tokens[0].ToLower())
            {
                case "hazez":
                    {
                        gm.MovePieceToBoard(NumbersTranslation[tokens[2]], LettersTranslation[tokens[4] + " " + tokens[5]]);
                        if(gm.CheckVictory())
                        {
                            VictoryUI.SetActive(true);
                            grMove.Stop();
                            grTake.Stop();
                        }
                        break;
                    }
                case "kah":
                    {
                        gm.ReovePieceFromBoard(NumbersTranslation[tokens[2]]);
                        break;
                    }
            }
        }
        catch
        {

        }
    }

    private void StopRecognizers()
    {
        grMove.Stop();
        grTake.Stop();
        keywordRecognizer.Stop();
    }

    private void StartRecognizers()
    {
        grMove.Start();
        grTake.Start();
        keywordRecognizer.Start();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
