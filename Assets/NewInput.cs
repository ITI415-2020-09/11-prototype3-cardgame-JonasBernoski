using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(Input.mousePosition, direction);
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
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
                        Top(hit.collider.gameObject);
                    }
                    else if (hit.collider.CompareTag("Bottom"))
                    {
                        Bottom(hit.collider.gameObject);
                    }
                }
            }
        }
        void Deck()
        {
            print("clickedDeck");
            solitaire.DealFromDeck();
        }
        void Card(GameObject selected)
        {
            print("clicked");

            if (!selected.GetComponent<Select>().faceup)
            {
                if (!Block(selected))
                {
                    selected.GetComponent<Select>().faceup = true;
                    slot1 = this.gameObject;
                }
            }
            else if (selected.GetComponent<Select>().inDeckPile)
            {
                if (!Block(selected))
                {
                    slot1 = selected;
                }
            }

            if (slot1 == this.gameObject)
            {
                slot1 = selected;
            }
            else if (slot1 != selected)
            {
                if (Stackable(selected))
                {
                    Stack(selected);
                }
                else
                {
                    slot1 = selected;
                }
            }
        }
        void Top(GameObject selected)
        {
            print("clicked top");
            if (slot1.CompareTag("Card"))
            {
                if (slot1.GetComponent<Select>().value == 1)
                {
                    Stack(selected);
                }
            }
        }
        void Bottom(GameObject selected)
        {
            print("clicked");
            if (slot1.CompareTag("Card"))
            {
                if (slot1.GetComponent<Select>().value == 13)
                {
                    Stack(selected);
                }
            }
        }
        bool Stackable(GameObject selected)
        {
            Select s1 = slot1.GetComponent<Select>();
            Select s2 = selected.GetComponent<Select>();

            if (!s2.inDeckPile)
            {
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
                    if (s1.value == s2.value - 1)
                    {
                        bool card1Red = true;
                        bool card2Red = true;

                        if (s1.suit == "C" || s1.suit == "S")
                        {
                            card1Red = false;
                        }
                        if (s2.suit == "C" || s2.suit == "S")
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
            }
            return false;
        }
    }
    void Stack(GameObject selected)
    {
        Select s1 = slot1.GetComponent<Select>();
        Select s2 = selected.GetComponent<Select>();
        float yOffset = 0.3f;

        if (s2.top || (!s2.top && s1.value == 13))
        {
            yOffset = 0;
        }
        slot1.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - yOffset, selected.transform.position.z - 0.01f);
        slot1.transform.parent = selected.transform;

        if (s1.inDeckPile)
        {
            solitaire.tripsOnDisplay.Remove(slot1.name);
        }
        else if (s1.top && s2.top && s1.value == 1)
        {
            solitaire.topPos[s1.row].GetComponent<Select>().value = 0;
            solitaire.topPos[s1.row].GetComponent<Select>().suit = null;
        }
        else if (s1.top)
        {
            solitaire.topPos[s1.row].GetComponent<Select>().value = s1.value - 1;
        }
        else
        {
            solitaire.bottoms[s1.row].Remove(slot1.name);
        }
        s1.inDeckPile = false;
        s1.row = s2.row;

        if (s2.top)
        {
            solitaire.topPos[s1.row].GetComponent<Select>().value = s1.value;
            solitaire.topPos[s1.row].GetComponent<Select>().suit = s1.suit;
            s1.top = true;
        }
        else
        {
            s1.top = false;
        }
        slot1 = this.gameObject;
    }
    bool Block(GameObject selected)
    {
        Select s2 = selected.GetComponent<Select>();
        if (s2.inDeckPile == true)
        {
            if (s2.name == solitaire.tripsOnDisplay.Last())
            {
                return false;
            }
            else
            {
                print(s2.name + "blocked by" + solitaire.tripsOnDisplay.Last());
                return true;
            }
        }
        else
        {
            if (s2.name == solitaire.bottoms[s2.row].Last())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

