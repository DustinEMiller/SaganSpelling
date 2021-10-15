using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class SoundManager : MonoBehaviour
{
    
    [SerializeField] private string _audioLocation;
    [SerializeField] private AudioSource _audioSource;

    private List<string> _wordQueue = new List<string>();
    private List<AudioClip> _soundQueue = new List<AudioClip>();
    
    public UnityEvent onOverlapSoundFinished;

    private Dictionary<Sound, AudioClip> _audioClipDictionary;
    private bool _soundPlaying = false;
    
    public enum Sound
    {
        Intro,
        Correct,
        Incorrect,
        NewMonster
    }

    private void Awake()
    {
        _audioClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            _audioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound)
    {
        AudioClip audioClip = _audioClipDictionary[sound];
        _soundQueue.Add(audioClip);

        ProcessAudioQueue();
    }
    
    public void PlaySound(string sound)
    {
        string wordUrl = _audioLocation + sound + ".wav";
        _wordQueue.Add(wordUrl);

        ProcessAudioQueue();
    }

    public void ProcessAudioQueue()
    {
        if (_wordQueue.Count > 0 & !_soundPlaying)
        {
            StartCoroutine(GetAudioClip(_wordQueue[0]));
            _wordQueue.RemoveAt(0);
        }
        else if (_soundQueue.Count > 0 & !_soundPlaying)
        {
            _audioSource.PlayOneShot(_soundQueue[0]);
            _soundPlaying = true;
            StartCoroutine(WaitForSound(_soundQueue[0].length));
            _soundQueue.RemoveAt(0);
        }
        
    }

    private IEnumerator GetAudioClip(string wordUrl)
    {
        
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(wordUrl, AudioType.WAV))
        {
            yield return www.Send();

            if (www.error != null)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                _audioSource.clip = myClip;
                _audioSource.Play();
                _soundPlaying = true;
                StartCoroutine(WaitForSound(_audioSource.clip.length));
            }
        }
    }
    
    IEnumerator WaitForSound(float duration)
    {
        yield return new WaitForSeconds(duration + 0.5f);
        _soundPlaying = false;
        onOverlapSoundFinished.Invoke();
    }
}
