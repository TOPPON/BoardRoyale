using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelecter : MonoBehaviour
{
    public static CardSelecter Instance;
    List<int> Cards = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < 26; i++)
        {
            Cards.Add(i);
        }
    }
    public int SelectCardByDeck()
    {
        int i = Random.Range(0,Cards.Count);
        int cardId = Cards[i];
        Cards.RemoveAt(i);
        return cardId;
    }
    public void ConsumeCard(int cardId)
    {
        Cards.Add(cardId-1);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
