using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wordLabel;
    [SerializeField] private Button _submitWord;
    [SerializeField] private Button _repeatWord;
    [SerializeField] private SoundManager soundManager;

    public void Awake()
    {
        _submitWord.onClick.AddListener(() =>
        {
            WordManager.Instance.CheckWord(_wordLabel.text);
        });
        
        _repeatWord.onClick.AddListener(() =>
        {
            soundManager.PlaySound(WordManager.Instance.CurrentWord);
        });
    }

    public void AddCharacter(char character)
    {
        _wordLabel.text += character.ToString();
    }

    public void RemoveCharacterAtIndex(int index)
    {
        string word = _wordLabel.text;
        _wordLabel.text = word.Remove(index, 1);
    }

    public void ClearWord()
    {
        _wordLabel.text = "";
    }
    
}
