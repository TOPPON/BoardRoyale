using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    enum CardGenre
    {
        Move,
        Attack,
        PowerUP,
        Heal,
        Other,
        Auto
    }
    CardGenre genre;
    int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Use(Player user)
    { 

    }
}
