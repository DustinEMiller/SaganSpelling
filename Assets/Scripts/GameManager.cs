using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _walkPoints;
    [SerializeField] private SoundManager _soundManager;

    private bool _listIsLoaded = false;
    private bool _introComplete = false;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        StartCoroutine(GameIntro());
    }

    private IEnumerator GameIntro()
    {
        //_soundManager.PlaySound(SoundManager.Sound.Intro);
        yield return new WaitForSeconds(2.0f);
        _introComplete = true;

        if (_listIsLoaded)
            RetrieveNewWord();

    }

    public Transform GetWalkPoint(int point)
    {
        return _walkPoints[point];
    }

    public void RetrieveNewWord()
    {
        WordManager.Instance.GetWord();
        KnightSpawner.Instance.SpawnKnights();
    }

    public void ListIsLoaded()
    {
        _listIsLoaded = true;

        if (_introComplete)
            RetrieveNewWord();
    }
}