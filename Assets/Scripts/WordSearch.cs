using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSearch : MonoBehaviour
{

    private List<List<string>> vocabulary = new List<List<string>> ();
    private char[] letters = { 'а', 'б', 'в', 'г', 'д',
        'е', 'ж', 'з', 'и', 'й', 'к',
        'л', 'м', 'н', 'о', 'п', 'р',
        'с','т', 'у', 'ф', 'х', 'ц',
        'ч', 'ш', 'щ', 'э', 'ю', 'я' };

    /// <summary>
    /// create the vocabulary in the memory
    /// </summary>
    private void Awake()
    {
        foreach(char ch in letters)
        {
            string[] lines = System.IO.File.ReadAllLines("Assets\\Docs\\" + ch + ".txt");
            List<string> words = new List<string>();

            foreach (string line in lines)
            {
               string[] w = line.Split(' ');
                foreach (string word in w) {
                    words.Add(word);
                }

            }

            vocabulary.Add(words);
        }

    }

    public bool Search(string word)
    {
        //Debug.Log(word.ToLower());

        return vocabulary[System.Array.IndexOf(letters, word.Substring(0, 1).ToLower().ToCharArray()[0])].Contains(word.ToLower());
    }


}
