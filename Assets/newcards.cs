using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newcards : MonoBehaviour
{
    public Sprite Face;
    public Sprite Back;
    private SpriteRenderer spriteRenderer;
    private Select select;
    private Solitaire solitaire;
    private NewInput newInput;
    // Start is called before the first frame update
    void Start()
    {
        List<string> deck = Solitaire.GenerateDeck();
        solitaire = FindObjectOfType<Solitaire>();
        newInput = FindObjectOfType<NewInput>();

        int i = 0; 
        foreach (string card in deck)
        {
            if (this.name == card)
            {
                Face = solitaire.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        select = GetComponent<Select>();
    }

    // Update is called once per frame
    void Update()
    {
        if (select.faceup == true)
        {
            spriteRenderer.sprite = Face;

        }
        else
        {
            spriteRenderer.sprite = Back; 
        }

        if (newInput.slot1)
        {
            if (name == newInput.slot1.name)
            {
                spriteRenderer.color = Color.green;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
}
