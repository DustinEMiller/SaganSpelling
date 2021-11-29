using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _walkPoints;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] public List<GameObject> _camera;
    [SerializeField] public Canvas _wordPanel;
    [SerializeField] public Canvas _gameOverScreen;
    [SerializeField] private HealthSystem _gateHealthSystem;

    private bool _listIsLoaded = false;
    private bool _introComplete = false;
    private float _secondsToWait;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        _secondsToWait = 2.0f;

        if (PlayerPrefs.GetString("Tutorial") == "True")
        {
            _secondsToWait = 18.0f;
            _camera[1].SetActive(true);
        }
        else
        {
            _camera[0].SetActive(true);
        }

        _gateHealthSystem.OnDied += GameOver;
        StartCoroutine(GameIntro());
    }

    private void GameOver(object sender, EventArgs e)
    {
        KnightSpawner.Instance.DespawnKnights();
        MonsterSpawner.Instance.DespawnMonsters();
        MonsterSpawner.Instance.gameObject.SetActive(false);
        _gameOverScreen.gameObject.SetActive(true);
        _wordPanel.gameObject.SetActive(false);
    }

    private IEnumerator GameIntro()
    {
        if (PlayerPrefs.GetString("Tutorial") == "True")
        {
            _soundManager.PlaySound(SoundManager.Sound.Intro);
        }

        yield return new WaitForSeconds(_secondsToWait);
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