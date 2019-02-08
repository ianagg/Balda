using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldLetter : MonoBehaviour
{
    [SerializeField] private List<Button> surroundings = new List<Button>();
    [SerializeField] private GameFlow game = null;

    private bool fistSelected = false;
    private bool doOnce = false;
    private bool clicked = false;


    ////////////The Field Button was clicked
    //There are two reasons for clicking the field button: 
    //- for placing the letter;
    //- for selecting letters to construct the word;
    //OnClick() checks the reasoning
    public void OnClick(Button b)
    {
        //if the player wants to place the letter here,
        //check if he can (the button must connect to other letters 
        //and has to be empty)


        if (game.IsPickState() && СheckIfValid() && b.name.StartsWith("Button"))
        {
            game.SelectedButtonText = b.GetComponentInChildren<Text>();
            game.SelectedButton = b;
            game.SwitchState();
            b.tag = "Placed";
            clicked = false;

            //change the tag
        }
        else
        {
            //WARNING
           
        }

        //if the player wants to select the letters 
        //and submit the word, need to check if this
        //the selection is valid.
        if (game.IsSelectState()&&!b.name.StartsWith("Button")&&!b.tag.Equals("Selected"))
        {
            if (fistSelected)
            {
                fistSelected = false;
                game.WordSelection(b);
                if (b.tag.Equals("Untagged"))     //switch the tag of the button if needed
                    b.tag = "Selected";
                
            } else
            {
                //check if surroundings contain button with tags.
                //yes - can continue; no - do nothing? error message?

                if (!b.tag.Equals("Places"))
                {
                    if (CheckForTags())
                    {
                        game.WordSelection(b);
                        //switch the tag of the button if needed
                        if (b.tag.Equals("Untagged"))
                            b.tag = "Selected";

                    }
                    else
                    {
                        ///WARNING
                    }
                } else
                {
                    if (!clicked)
                    {
                        game.WordSelection(b);
                        clicked = true;
                    }
                    else
                    {
                        ///WARNING
                    }


                }
                
               

            }
        }

    }

    //erases the tags if the state is correct
    private void Update()
    {
        if (!doOnce)
        {
            if (game.IsPickState())
            {
                foreach (Button b in surroundings)
                    b.tag = "Untagged";
                doOnce = true;
                fistSelected = true;

            }

        }

        if (game.IsSelectState()) {
            doOnce = false;
        }
    }


    //checks if the surroundings of the clicked button contain the letters,
    //if so - the player can put the new letter
    private bool СheckIfValid()
    {
        foreach (Button b in surroundings)
        {
            if (!b.name.StartsWith("Button"))
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckForTags() {

        foreach (Button b in surroundings)
        {
            Debug.Log(b.name + ": " + b.tag.ToString() + "; ");

            if (b.tag.Equals("Placed") || b.tag.Equals("Selected"))
            {
                return true;
            }

           
        }

        return false;


    }


}
