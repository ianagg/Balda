using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetLetter : MonoBehaviour
{
    [SerializeField] private GameFlow game = null;


    public void OnClick() {

        game.LetterSelection(this.gameObject.GetComponent<Button>().name);  
    }

}
