using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wordLabel;
    [SerializeField] private TextMeshProUGUI _hintLabel;
    [SerializeField] private TextMeshProUGUI _monsterKilledLabel;
    [SerializeField] private Button _submitWord;
    [SerializeField] private Button _repeatWord;
    [SerializeField] private SoundManager _soundManager;

    private string _answer;

    private void Awake()
    {
        _submitWord.onClick.AddListener(() =>
        {
            WordManager.Instance.CheckWord(_answer);
        });
        
        _repeatWord.onClick.AddListener(() =>
        {
            _soundManager.PlaySound(WordManager.Instance.CurrentWord);
        });
    }

    private void RebuildLabel()
    {
        _wordLabel.text = "";
        foreach (char character in _answer)
        {
            _wordLabel.text += "<color=#fff>" + character.ToString() + "</color>";
        }
    }

    private void ShowHintLabel(int numberOfLetters)
    {
        _hintLabel.enabled = true;
        _hintLabel.text = "HINT: " + numberOfLetters + " more letters.";
    }

    public void AddCharacter(char character, string color = "#fff")
    {
        _hintLabel.enabled = false;
        _answer += character.ToString();
        _wordLabel.text += "<color="+color+">" + character.ToString() + "</color>";
    }

    public void RemoveCharacterAtIndex(int index)
    {
        _hintLabel.enabled = false;
        _answer = _answer.Remove(index, 1);
        RebuildLabel();
    }

    public void ClearWord()
    {
        _answer = "";
        _wordLabel.text = "";
    }

    public void SetWordHighlights(List<int> highlights)
    {
        string tempAnswer = _answer;
        int additionalLetters = 0;
            
        ClearWord();
        
        for (int i = 0; i < highlights.Count; i++)
        {
            switch (highlights[i])
            {
                case 0:
                    AddCharacter(tempAnswer[i], "#f70000");
                    break;
                case 1:
                    AddCharacter(tempAnswer[i], "#00ff00");
                    break;
                case 2:
                    additionalLetters++;
                    break;
            }
        }

        if (additionalLetters > 0)
            ShowHintLabel(additionalLetters);
    }

    public void SetMonstersKilled()
    {
        string count = MonsterSpawner.Instance.GetMonstersKilled().ToString();
        _monsterKilledLabel.text = count;
    }
    
}
