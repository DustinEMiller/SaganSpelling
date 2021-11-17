using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField] private WordPanel WordPanel;
    [SerializeField] private SoundManager SoundManager;
    
    private List<Knight> _selectedKnights = new List<Knight>();
    private string _currentWord;
    private WordList _wordList;

    public static WordManager Instance { get; private set; }
    public string CurrentWord {
        get
        {
            return _currentWord;
        }
        private set { }
    }
    private void Awake()
    {
        Instance = this;
    }

    public void GetWordList()
    {
        _wordList = NetworkManager.Instance.GetWeeklyWords();
    }
    
    public void SelectKnight(Knight knight)
    {
        _selectedKnights.Add(knight);
        WordPanel.AddCharacter(knight.GetComponentInChildren<KnightLetter>().GetLetter());
    }

    public void RemoveKnight(Knight knight)
    {
        var item = _selectedKnights.FindIndex(x => x.GetInstanceID() == knight.GetInstanceID());
        if (item != null)
        {
            WordPanel.RemoveCharacterAtIndex(item);
            _selectedKnights.RemoveAt(item);
        }
    }
    
    public string GetLetters()
    {
        string allLetters = _currentWord;
        
        for (var i = 0; i < 2; i++)
        {
            if (allLetters.Length >= 10)
                return allLetters;
            
            allLetters += GetVowel();
        }
        
        for (var i = allLetters.Length; i < 10; i++)
            allLetters += GetConsonant();

        return allLetters;
    }

    public void CheckWord(string submittedWord)
    {
        if (submittedWord == _currentWord)
        {
            SoundManager.PlaySound(SoundManager.Sound.Correct);
            KnightSpawner.Instance.SetDamage(_currentWord.Length);
            GameManager.Instance.RetrieveNewWord();
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.Incorrect);
            HighlightCharacters(submittedWord);
        }
    }

    public void GetWord()
    {
        _selectedKnights.Clear();
        WordPanel.ClearWord();
        int number = Random.Range(1, 8);

        if (number <= 5)
            _currentWord = _wordList.words[0].highFrequencyWords[Random.Range(0, _wordList.words[0].highFrequencyWords.Count)].word;
        else
            _currentWord = _wordList.words[0].otherWords[Random.Range(0, _wordList.words[0].otherWords.Count)].word;

        SoundManager.PlaySound(_currentWord);
    }
    
    private char GetVowel()
    {
        string vowels = "aeiou";
        int num = Random.Range(0, vowels .Length);
        return vowels [num];
    }
    
    private char GetConsonant()
    {
        string chars = "bcdfghjklmnpqrstvwxyz";
        int num = Random.Range(0, chars.Length);
        return chars[num];
    }

    private void HighlightCharacters(string submittedWord)
    {
        List<int> highlightedCharacters = new List<int>();

        for (var i = 0; i <= _currentWord.Length - 1; i++ )
        {
            if (i + 1 <= submittedWord.Length)
            {
                if (_currentWord[i] == submittedWord[i])
                    highlightedCharacters.Add(1);
                else
                    highlightedCharacters.Add(0);
            }
        }
        
        if(submittedWord.Length > _currentWord.Length)
            highlightedCharacters.AddRange(Enumerable.Repeat(0, submittedWord.Length - _currentWord.Length));
        else if(submittedWord.Length < _currentWord.Length)
            highlightedCharacters.AddRange(Enumerable.Repeat(2, _currentWord.Length - submittedWord.Length));
        
        WordPanel.SetWordHighlights(highlightedCharacters);
    }
    
}
