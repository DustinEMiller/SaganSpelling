using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnightLetter : MonoBehaviour
{
    private bool _selected = false;

    [SerializeField] private Knight _knight;
    [SerializeField] private TextMeshPro _letterLabel;
    [SerializeField] private CapsuleCollider _letterCollider;
    
    private char _letter;
    
    void Update()
    {
        _letterLabel.transform.LookAt(Camera.main.transform);
        _letterLabel.transform.localScale = new Vector3(-1, 1,1);
    }
    
    private void OnMouseDown()
    {
        if (_selected)
        {
            _selected = false;
            WordManager.Instance.RemoveKnight(_knight);
            ChangeTextColor(Color.white);
        }
        else
        {
            _selected = true;
            WordManager.Instance.SelectKnight(_knight);
            ChangeTextColor(Color.green);
        }
    }
    
    private void ChangeTextColor(Color color)
    {
        _letterLabel.color = color;
    }
    
    public void AssignLetter(char letter)
    {
        _letterLabel.text = letter.ToString();
        _letter = letter;
    }
    
    public char GetLetter()
    {
        return _letter;
    }
    
    public void RaiseTextLabel()
    {
        Vector3 position = new Vector3(0,1 , 0); 
        _letterLabel.transform.position += position;
    }
}
