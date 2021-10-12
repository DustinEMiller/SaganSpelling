using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class SoundManager : MonoBehaviour
{
    
    [SerializeField] private string _audioLocation;
    [SerializeField] private AudioSource _audioSource;
    public UnityEvent onOverlapSoundFinished;

    private Dictionary<Sound, AudioClip> _audioClipDictionary;
    
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
    public void PlayWord(string word)
    {
        string wordUrl = _audioLocation + word + ".wav";
        StartCoroutine(GetAudioClip(wordUrl));
    }
    
    public void PlaySound(Sound sound)
    {
        _audioSource.PlayOneShot(_audioClipDictionary[sound]);
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
                StartCoroutine(WaitForSound(_audioSource.clip.length));
            }
        }
    }
    
    IEnumerator WaitForSound(float duration)
    {
        yield return new WaitForSeconds(duration);
        onOverlapSoundFinished.Invoke();
    }

    
}
