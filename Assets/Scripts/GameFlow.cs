using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{

    private enum GameState { PICK, PLACE, SELECT };
    private string selectedLetter;
    [SerializeField] private Text wordText = null;
    [SerializeField] private Text playerOneScore = null;
    [SerializeField] private Text playerTwoScore = null;
    [SerializeField] private GameObject turn = null;
    private bool isPlayerOne = true;

    private WordSearch search;
    private List<Button> selectedWord = new List<Button>();

    private Text selectedButtonText;
    public Text SelectedButtonText { get => selectedButtonText; set => selectedButtonText = value; }
    private Button selectedButton;
    public Button SelectedButton { get => selectedButton; set => selectedButton = value; }

    private GameState state = GameState.PICK;

    public bool IsPickState()
    {
        return state == GameState.PICK;
    }
    public bool IsSelectState()
    {
        return state == GameState.SELECT;
    }
    public bool IsPlaceState()
    {
        return state == GameState.PLACE;
    }


    public GameFlow() { }

    // Start is called before the first frame update
    void Start()
    {
        search = this.gameObject.GetComponent<WordSearch>();
    }

   
    //switches gamestate
    public void SwitchState()
    {
        switch (state)
        {
            case GameState.PICK:
                state = GameState.PLACE;
                break;
            case GameState.PLACE:
                state = GameState.SELECT;
                break;
            case GameState.SELECT:
                state = GameState.PICK;
                break;

        }

    }

    public void LetterSelection(string s)
    {
        selectedLetter = s;
        if (state == GameState.PLACE)
        {
            SelectedButtonText.GetComponent<Text>().text = s;
            SelectedButton.name = s;
            SwitchState();
        }
    }

    //если получать и сохранять в массив именно кнопки? 
    //тогда можно и забирать их имена и потом проверить,
    //есть ли среди кнопок в массиве кнопка с определенным тэгом
    public void WordSelection(Button b)
    {
        wordText.text += b.name.ToString();
        selectedWord.Add(b);
    }

    private bool Check() {

        foreach (Button b in selectedWord)
            if (b.tag.Equals("Placed"))
                return true;
  
        return false;
    }



    public void OnClickSubmit()
    {
        if (state == GameState.SELECT)
        {
            if (search.Search(wordText.text)&&Check())
            {
                if (isPlayerOne)
                {
                    int i = System.Int32.Parse(playerOneScore.text);

                    playerOneScore.text = "" + (i + wordText.text.Length);
                    isPlayerOne = false;
                    turn.transform.localPosition -= new Vector3(0,28f,0);

                } else
                {
                    int i = System.Int32.Parse(playerTwoScore.text);

                    playerTwoScore.text = "" + (i + wordText.text.Length);
                    isPlayerOne = true;
                    turn.transform.localPosition += new Vector3(0, 28f, 0);


                }
            }
            else
            {
                SelectedButton.name = "Button";
                selectedButtonText.GetComponent<Text>().text = "";                
            }

            selectedWord.Clear();
            wordText.text = "";
            SwitchState();

        }

    }

    public void OnClickPass() {
        
        if (isPlayerOne)
            turn.transform.localPosition -= new Vector3(0, 28f, 0);
        else
            turn.transform.localPosition += new Vector3(0, 28f, 0);

        isPlayerOne = !isPlayerOne;

        SelectedButton.name = "Button";
        selectedButtonText.GetComponent<Text>().text = "";
        selectedWord.Clear();
        wordText.text = "";
        state = GameState.PICK;

    }


}
