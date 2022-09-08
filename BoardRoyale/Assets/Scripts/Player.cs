using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    List<int> cards;
    public bool isPushedChoiceWayButton;
    bool needToChoiceWay;//道を選ぶ必要があるときにtrue、選ぶフェーズに入ったらfalseになる
    List<int> havingCard;
    public List<GameObject> wayButtons;
    enum PlayerOperation
    {
        None,
        Dice,
        Card,
        ChoiceWay,
        Move
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
                        if (needToChoiceWay)
                        {
                            needToChoiceWay = false;
                            isPushedChoiceWayButton = false;
                            myOpe = PlayerOperation.ChoiceWay;
                            UIManager.Instance.SetChoiceWayButton(this);
                        }
                        else
                        {
                            myOpe = PlayerOperation.Move;
                        }
                    }
                    break;
                case PlayerOperation.ChoiceWay:
                    if (isPushedChoiceWayButton)
                    {
                        myOpe = PlayerOperation.Move;
                    }
                    break;
                case PlayerOperation.Move:
                    Move(DiceView.Instance.GetDiceNumber());
                    DiceView.Instance.EraceDice();
                    TurnEnd();
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
            case GameMap.SquareSpecies.Boss:
                GameManager.Instance.ArriveBossSquare();
                break;
        }
        //ここに敵やプレイヤーとぶつかったなど
    }

    public override void TurnStart()
    {
        base.TurnStart();
        myOpe = PlayerOperation.None;
        if (CheckNeedToChoiceWay())
        {
            needToChoiceWay = true;
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
    private bool CheckNeedToChoiceWay()
    {
        switch (GameMap.Instance.GetSquareById(nowSquareId).species)
        {
            case GameMap.SquareSpecies.Start:
                return true;
            case GameMap.SquareSpecies.Cross:
                return true;
            default:
                return false;
        }
    }
}
