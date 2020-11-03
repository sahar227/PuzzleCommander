using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceInput : MonoBehaviour
{
    private GameManager gm;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        actions.Add("Test", DoSomething);
        actions.Add("One", DoSomething);
        actions.Add("Two", DoSomething);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizeSpeech;
        keywordRecognizer.Start();
        
    }

    private void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    void DoSomething()
    {
        gm.MovePieceToBoard(0, 'A');
        //gm.MovePieceToBoard(1, 'B');
       // gm.MovePieceToBoard(2, 'C');
       // gm.MovePieceToBoard(3, 'D');

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
