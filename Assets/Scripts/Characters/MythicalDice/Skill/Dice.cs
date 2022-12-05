using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField] 
    private Sprite[] _diceSprites = null;
    
    private int _diceNumber = 0;

    public int DiceNumber => _diceNumber;

    public void SetDice(int diceNumber, Transform diceParent)
    {
        transform.SetParent(diceParent);
        diceNumber = diceNumber;
        GetComponent<Image>().sprite = _diceSprites[diceNumber];
    }

    private void OnEnable()
    {
        
    }
    
    private void OnDisable()
    {
        
    }
}
