using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class StartScreenScript : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    //private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    [SerializeField] private Animator anim;


    // Start is called before the first frame update
    void OnEnable()
    {
        //actions.Add("Start", DoSomething);
        string [] actions = {"Hathel"};
        keywordRecognizer = new KeywordRecognizer(actions, ConfidenceLevel.Low);

        keywordRecognizer.OnPhraseRecognized += RecognizeSpeech;
        keywordRecognizer.Start();
        

    }

    private void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        CleanRecognizer();
        // open main menu scene
        anim.Play("Fadeout Animation");
        Invoke("ChangeScene", 1);
        
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
    private void CleanRecognizer()
    {
        keywordRecognizer.Stop();
        keywordRecognizer.Dispose();
    }
}
