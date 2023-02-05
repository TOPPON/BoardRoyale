using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    static List<Card> cards = new List<Card>();
    public enum CardGenre
    {
        Move,
        Attack,
        PowerUP,
        Heal,
        Other,
        Auto
    }
    public CardGenre genre;
    public int moveSquareNumber;
    public int attackRange;
    public int attackDamage;
    public bool attackSingle;
    public int powerUpAtk;
    public int powerUpHp;
    public int powerUpTurn;
    public int healHp;
    public int healingTurn;
    public enum OtherCard
    {
        None,
        DirectionChange
    }
    public OtherCard otherCard;
    public enum AutoCard
    {
        None,
        Revive
    }
    public AutoCard autoCard;

    public int restTurn;
    public int counterDamage;
    public int id;

    Card(int id, CardGenre genre, int moveSquareNumber, int attackRange,
    int attackDamage, bool attackSingle, int powerUpAtk, int powerUpHp, int powerUpTurn,
    int healHp, int healingTurn, OtherCard otherCard, AutoCard autoCard,
    int restTurn, int counterDamage)
    {
        this.id = id;
        this.genre = genre;
        this.moveSquareNumber = moveSquareNumber;
        this.attackRange = attackRange;
        this.attackDamage = attackDamage;
        this.attackSingle = attackSingle;
        this.powerUpAtk = powerUpAtk;
        this.powerUpHp = powerUpHp;
        this.powerUpTurn = powerUpTurn;
        this.healHp = healHp;
        this.healingTurn = healingTurn;
        this.otherCard = otherCard;
        this.autoCard = autoCard;
        this.restTurn = restTurn;
        this.counterDamage = counterDamage;

    }
    public static void MakeCards()
    {
        //for (int i = 0; i < 80; i++)
        //    cards.Add(new Card(i, CardGenre.Attack, 0, 10, 3, false, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(1, CardGenre.Move, 1, 0, 0, false, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(2, CardGenre.Move, 2, 0, 0, false, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(3, CardGenre.Move, 3, 0, 0, false, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(4, CardGenre.Move, 4, 0, 0, false, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(5, CardGenre.Move, 5, 0, 0, false, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(6, CardGenre.Move, 6, 0, 0, false, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));

        cards.Add(new Card(7, CardGenre.Attack, 0, 2, 3, true, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(8, CardGenre.Attack, 0, 2, 1, false, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(9, CardGenre.Attack, 0, 1, 4, true, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(10, CardGenre.Attack, 0, 5, 1, true, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(11, CardGenre.Attack, 0, 1, 3, false, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(12, CardGenre.Attack, 0, 4, 2, true, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(13, CardGenre.Attack, 0, 4, 3, true, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.None, 1, 0));

        cards.Add(new Card(14, CardGenre.PowerUP, 0, 0, 0, false, 1, 0, 1, 0, 0, OtherCard.None, AutoCard.None, 1, 0));
        cards.Add(new Card(15, CardGenre.PowerUP, 0, 0, 0, false, 0, 1, 0, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(16, CardGenre.PowerUP, 0, 0, 0, false, 0, 3, 2, 0, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(17, CardGenre.PowerUP, 0, 0, 0, false, 0, 4, 2, 0, 0, OtherCard.None, AutoCard.None, 2, 0));

        cards.Add(new Card(18, CardGenre.Heal, 0, 0, 0, false, 0, 0, 0, 2, 2, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(19, CardGenre.Heal, 0, 0, 0, false, 0, 0, 0, 3, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(20, CardGenre.Heal, 0, 0, 0, false, 0, 0, 0, 1, 5, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(21, CardGenre.Heal, 0, 0, 0, false, 0, 0, 0, 5, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(22, CardGenre.Heal, 0, 0, 0, false, 0, 0, 0, 6, 0, OtherCard.None, AutoCard.None, 1, 0));
        cards.Add(new Card(23, CardGenre.Heal, 0, 0, 0, false, 0, 0, 0, 2, 0, OtherCard.None, AutoCard.None, 0, 0));
        cards.Add(new Card(24, CardGenre.Heal, 0, 0, 0, false, 0, 0, 0, 2, 4, OtherCard.None, AutoCard.None, 0, 0));

        cards.Add(new Card(25, CardGenre.Other, 0, 0, 0, false, 0, 0, 0, 0, 0, OtherCard.DirectionChange, AutoCard.None, 0, 0));
        
        cards.Add(new Card(26, CardGenre.Auto, 0, 0, 0, false, 0, 0, 0, 0, 0, OtherCard.None, AutoCard.Revive, 0, 0));
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
    public static Card GetCardById(int cardId)
    {
        return cards[cardId];
    }
    public void Use(Player player)
    {
        print("cardId:" + id);
        switch (genre)
        {
            case CardGenre.Move:
                player.MoveByCard(moveSquareNumber);
                break;
            case CardGenre.Attack:
                player.AttackRange(attackDamage, attackRange, attackSingle,restTurn);
                break;
            case CardGenre.PowerUP:
                player.PowerUp(powerUpAtk, powerUpHp, powerUpTurn, restTurn);
                break;
            case CardGenre.Heal:
                player.HealHp(healHp, healingTurn,restTurn);
                break;
            case CardGenre.Other:
                switch (otherCard)
                {
                    case OtherCard.DirectionChange:
                        player.movingDirection *= -1;
                        UIManager.Instance.SetMovingArrow(player);
                        break;
                }
                break;
            case CardGenre.Auto:
                break;
        }
        CardSelecter.Instance.ConsumeCard(id);

        /*
        this.id = id;
        this.genre = genre;
            
            Move,
            Attack,
            PowerUP,
            Heal,
            Other,
            Auto

        this.moveSquareNumber = moveSquareNumber;
        this.attackRange = attackRange;
        this.attackDamage = attackDamage;
        this.powerUpAtk = powerUpAtk;
        this.powerUpHp = powerUpHp;
        this.healHp = healHp;
        this.healingTurn = healingTurn;
        this.otherCard = otherCard;

            None,
            DirectionChange

        this.autoCard = autoCard;
        
            None,
            Revive
        
        this.restTurn = restTurn;
        this.counterDamage = counterDamage; 
        */
    }
}
