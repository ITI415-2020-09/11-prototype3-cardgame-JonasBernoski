using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInput : MonoBehaviour
{
    public GameObject slot1;
    private Solitaire solitaire;

    void Start()
    {
        solitaire = FindObjectOfType<Solitaire>();
        slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
    }
    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out hit))
            {
                if (hit.collider.CompareTag("Deck"))
                {
                    Deck();
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    Card(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Top"))
                {
                    Top();
                }
                else if (hit.collider.CompareTag("Bottom"))
                {
                    Bottom();
                }
            }
        }
    }
    void Deck()
    {
        print("clicked");
        solitaire.DealFromDeck();
    }
    void Card(GameObject selected)
    {
        print("clicked");
        if (slot1 == this.gameObject)
        {
            slot1 = selected;
        }
        else if (slot1 != selected)
        {
            if (Stackable(selected))
            {

            }
            else
            {
                slot1 = selected;
            }
        }
    }
    void Top()
    {
        print("clicked");
    }
    void Bottom()
    {
        print("clicked");
    }
    bool Stackable(GameObject selected)
    {
        Select s1 = slot1.GetComponent<Select>();
        Select s2 = selected.GetComponent<Select>();
        
        if (s2.top)
        {
            if (s1.suit == s2.suit || (s1.value == 1 && s2.suit == null))
            {
                if (s1.value == s2.value + 1)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (s1.value == s2.value -1)
            {
                bool card1Red = true;
                bool card2Red = true; 

                if (s1.suit == "C" || s1.suit == "S")
                {
                    card1Red = false;
                }
                if (s2. suit == "C" || s2.suit == "S")
                {
                    card2Red = false; 
                }
                if (card1Red == card2Red)
                {
                    print("Can't Stack");
                    return false;
                }
                else
                {
                    print("Stacks");
                    return true; 
                }
            }
        }
        return false;
    }
}

