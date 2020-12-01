using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetMouseClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Deck"))
                {
                    Deck();
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    Card();
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
    }
    void Card()
    {
        print("clicked");
    }
    void Top()
    {
        print("clicked");
    }
    void Bottom()
    {
        print("clicked");
    }
}

