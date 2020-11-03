using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class GrammarInput : MonoBehaviour
{
    private GrammarRecognizer gr;
    private GameManager gm;
    private Dictionary<string, char> LettersTranslation;
    private Dictionary<string, int> NumbersTranslation;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();

        LettersTranslation = new Dictionary<string, char>
        {
            { "aye", 'A' },
            { "bee", 'B' },
            { "sea", 'C' },
            { "dee", 'D' }

        };

        NumbersTranslation = new Dictionary<string, int>
        {
            {"zero", 0 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 }
        };
        string path = Application.streamingAssetsPath + "/Grammars/GrammarInput.xml";
        Debug.Log(path);
        gr = new GrammarRecognizer(path);
        //gr = new GrammarRecognizer(@"C:\Users\Sahar\Documents\Projects\Unity\2D projects\Puzzle Voice Commands\Assets\Grammars\GrammarInput.xml");
        gr.OnPhraseRecognized += RecognizeSpeech;
        gr.Start();
    }

    private void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        var tokens = speech.text.Split();
        switch (tokens[0].ToLower())
        {
            case "move":
                {
                    gm.MovePieceToBoard(NumbersTranslation[tokens[1]], LettersTranslation[tokens[3]]);
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
