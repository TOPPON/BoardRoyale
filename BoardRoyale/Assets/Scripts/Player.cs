using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    List<int> cards;
    int nowSquareId;
    int nowLoop;
    int movingDirection;
    int hp;
    int maxhp;
    int atk;
    int restTurn;//休み
    bool isMyTurn;
    List<int> havingCard;
    enum PlayerOperation
    {
        None,
        Dice,
        Card
    }
    PlayerOperation myOpe=PlayerOperation.None;
    // Start is called before the first frame update
    void Start()
    {
        movingDirection = -1;
        nowLoop = 2;
        nowSquareId = 0;
        gameObject.transform.position = MapView.Instance.GetPlayerPositionById(nowSquareId);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(1);
        }
        if (isMyTurn)
        {
            switch (myOpe)
            {
                case PlayerOperation.Card:
                    break;
                case PlayerOperation.Dice:
                    if (DiceView.Instance.GetDiceNumber() > 0)
                    {
                        Move(DiceView.Instance.GetDiceNumber());
                        print(DiceView.Instance.GetDiceNumber());
                        isMyTurn = false;
                        GameManager.Instance.NextPlayerTrun(); 
                    }
                    break;
            }
        }
    }
    public void Move(int squares)
    {
        for (int i = 0; i < squares; i++)
        {
            nowSquareId=GameMap.Instance.GetnextSquare(nowLoop, nowSquareId, movingDirection);
        }
        gameObject.transform.position = MapView.Instance.GetPlayerPositionById(nowSquareId);
        switch (GameMap.Instance.GetSquareById(nowSquareId).species)
        {
            case GameMap.SquareSpecies.Start:
                if (hp < maxhp) hp++;
                break;
            case GameMap.SquareSpecies.Card:
                CardSelecter.Instance.SelectCardByDeck();
                break;
        }
    }
    public void SetFirstStatus(int hp,int atk)
    {
        maxhp = hp;
        this.hp = hp;
        this.atk = atk;
    }
    public void MyTurn()
    {
        isMyTurn = true;
        myOpe = PlayerOperation.None;
        if(restTurn>0)
        {
            restTurn--;
            GameManager.Instance.NextPlayerTrun();
        }
    }
    public void DiceRoll()
    {
        myOpe = PlayerOperation.Dice;
        DiceView.Instance.Roll();
    }
    public void SeeCard()
    {

    }
    public void DecideMovingWay()
    {

    }
}
