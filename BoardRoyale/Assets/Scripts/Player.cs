using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Player : Character
{
    List<int> cards;
    public bool isPushedChoiceWayButton;
    bool needToChoiceWay;//道を選ぶ必要があるときにtrue、選ぶフェーズに入ったらfalseになる
    List<Card> havingCard = new List<Card>();
    public List<GameObject> wayButtons;
    int moveCount;
    float opeTimer;

    //状態異常系(プレイヤーにしか関わらないもの)
    int healingTurn;
    int healingAmount;

    int powerUpTurn;
    int powerUpAtk;
    int powerUpHp;

    //

    enum PlayerOperation
    {
        None,
        Dice,
        Card,
        UsingCard,
        ChoiceWay,
        Move,
        MovingAnimation,
        MoveFinish,
        CheckSquare,
        TrashCardSelect,
        Attack
    }
    PlayerOperation myOpe = PlayerOperation.None;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetCharacterPositionBynowSquareId", 0.05f);
    }

    public override void SetFirstStatus(int hp, int atk)
    {
        base.SetFirstStatus(hp, atk);
        movingDirection = -1;
        nowLoop = 2;
        nowSquareId = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isMyTurn)
            {
                UIManager.Instance.selectingCard = 1;
                UseCard();
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isMyTurn)
            {
                havingCard.Add(Card.GetCardById(CardSelecter.Instance.SelectCardByDeck()));
                UIManager.Instance.UpdateStatusCard(this, havingCard);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isMyTurn)
            {
                TurnStart();
                UIManager.Instance.TurnStart();
                UIManager.Instance.AttachOperationButton(this);
                UIManager.Instance.UpdateSituationText((GameManager.Instance.GetPlayerIdByInstance(this) + 1).ToString() + "Pのターン！");
            }
        }
        if (isMyTurn)
        {
            switch (myOpe)
            {
                case PlayerOperation.Card:
                    break;
                case PlayerOperation.UsingCard:
                    opeTimer += Time.deltaTime;
                    if (opeTimer > 0.5f)
                    {
                        TurnEnd();
                    }
                    break;
                case PlayerOperation.Dice:
                    if (DiceView.Instance.GetDiceNumber() > 0)
                    {
                        if (needToChoiceWay)
                        {
                            needToChoiceWay = false;
                            isPushedChoiceWayButton = false;
                            myOpe = PlayerOperation.ChoiceWay;
                            CalcMovableSquareAndShine(DiceView.Instance.GetDiceNumber());
                            UIManager.Instance.SetChoiceWayButton(this);
                        }
                        else
                        {
                            myOpe = PlayerOperation.Move;
                            moveCount = DiceView.Instance.GetDiceNumber();
                        }
                    }
                    break;
                case PlayerOperation.ChoiceWay:
                    if (isPushedChoiceWayButton)
                    {
                        myOpe = PlayerOperation.Move;
                        moveCount = DiceView.Instance.GetDiceNumber();
                        CalcMovableSquareAndStopShine(DiceView.Instance.GetDiceNumber());
                    }
                    break;
                case PlayerOperation.Move:
                    if (moveCount > 0)
                    {
                        Move(1);
                        myOpe = PlayerOperation.MovingAnimation;
                        opeTimer = 0;
                        moveCount--;
                    }
                    else
                    {
                        myOpe = PlayerOperation.MoveFinish;
                    }
                    break;
                case PlayerOperation.MovingAnimation:
                    opeTimer += Time.deltaTime;
                    if (opeTimer > 0.1f)
                    {
                        opeTimer = 0;
                        myOpe = PlayerOperation.Move;
                    }
                    break;
                case PlayerOperation.MoveFinish:
                    opeTimer += Time.deltaTime;
                    if (opeTimer > 0.3f)
                    {
                        DiceView.Instance.EraceDice();
                        myOpe = PlayerOperation.CheckSquare;
                        opeTimer = 0;
                    }
                    break;
                case PlayerOperation.CheckSquare:
                    CheckSquare();
                    if (havingCard.Count > 3)
                    {
                        myOpe = PlayerOperation.TrashCardSelect;
                        UIManager.Instance.AttachTrashCardSelectButton(this);
                        UIManager.Instance.TrashCardSelect();
                    }
                    else myOpe = PlayerOperation.Attack;
                    break;
                case PlayerOperation.TrashCardSelect:
                    break;
                case PlayerOperation.Attack:
                    SameSquareAttack();
                    TurnEnd();
                    break;
            }
        }
    }
    public void Move(int squares)
    {
        for (int i = 0; i < squares; i++)
        {
            nowSquareId = GameMap.Instance.GetnextSquare(nowLoop, nowSquareId, movingDirection);
        }
        SetCharacterPositionBynowSquareId();
        //ここに敵やプレイヤーとぶつかったなど
    }
    void CheckSquare()
    {
        switch (GameMap.Instance.GetSquareById(nowSquareId).species)
        {
            case GameMap.SquareSpecies.Start:
                if (hp + 2 > maxhp) ChangeHp(maxhp - hp);
                else ChangeHp(2);
                break;
            case GameMap.SquareSpecies.Card:
                havingCard.Add(Card.GetCardById(CardSelecter.Instance.SelectCardByDeck()));
                UIManager.Instance.UpdateStatusCard(this, havingCard);
                break;
            case GameMap.SquareSpecies.Boss:
                GameManager.Instance.ArriveBossSquare();
                break;
        }
    }
    public override void TurnStart()
    {
        myOpe = PlayerOperation.None;
        if (healingTurn > 0)
        {
            if (hp + healingAmount < maxhp) ChangeHp(healingAmount);
            else ChangeHp(maxhp - hp);
            healingTurn--;
        }
        if (powerUpTurn > 0)
        {
            powerUpTurn--;
            if (powerUpTurn == 0)
            {
                maxhp += powerUpHp;
                hp += powerUpHp;
                atk += powerUpAtk;
                UIManager.Instance.UpdatePlayerStatus(GameManager.Instance.GetPlayerIdByInstance((Player)this) + 1, hp, maxhp, atk, ((Player)this).havingCard.Count);
            }
            if (restTurn == 0) TurnEnd();
        }
        if (CheckNeedToChoiceWay())
        {
            needToChoiceWay = true;
        }
        base.TurnStart();
    }
    public void DiceRoll()
    {
        myOpe = PlayerOperation.Dice;
        DiceView.Instance.Roll();
    }
    public void SeeCard()
    {
        myOpe = PlayerOperation.Card;
    }
    public void TrashSeeCard()
    {
        int cardId = UIManager.Instance.selectingCard - 1;
        if (cardId != -1)
        {
            havingCard.RemoveAt(cardId);
            UIManager.Instance.UpdateStatusCard(this, havingCard);
        }
    }
    public void TrashExtraCard()
    {
        int cardId = UIManager.Instance.selectingCard - 1;
        if (cardId != -1)//何も起こらない
        {
            havingCard.RemoveAt(cardId);
            UIManager.Instance.UpdateStatusCard(this, havingCard);
            myOpe = PlayerOperation.Attack;
        }
    }
    public void BackCard()
    {
        myOpe = PlayerOperation.None;
    }
    public void UseCard()
    {
        int cardId = UIManager.Instance.selectingCard - 1;
        if (cardId != -1)//何も起こらない
        {
            myOpe = PlayerOperation.UsingCard;
            havingCard[cardId].Use(this);
            havingCard.RemoveAt(cardId);
            UIManager.Instance.UpdateStatusCard(this, havingCard);
        }
    }
    public void MoveByCard(int moveSquareNumber)
    {
        moveCount = moveSquareNumber;
        myOpe = PlayerOperation.Move;
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
    protected override void SetCharacterPositionBynowSquareId()
    {
        gameObject.transform.position = MapView.Instance.GetPlayerPositionById(nowSquareId) + gapCenter;

    }
    public bool CheckAndUseReviveCard()
    {
        foreach (Card card in havingCard)
        {
            if (card.genre == Card.CardGenre.Auto && card.autoCard == Card.AutoCard.Revive)
            {
                Predicate<Card> a = p => card.gameObject == p.gameObject;
                CardSelecter.Instance.ConsumeCard(havingCard.FindIndex(a));
                havingCard.Remove(card);
                UIManager.Instance.UpdateStatusCard(this, havingCard);
                hp = 1;
                return true;
            }
        }
        return false;
    }
    public void AttackRange(int damage, int range, bool single, int restTurn)
    {
        this.restTurn = restTurn;
        if (CheckNeedToChoiceWay())//crossかどうか判定
        {
            //GameMap.Instance.GetSquareById(nowSquareId).cross.cross1LoopId;
            int attack1loop = GameMap.Instance.GetSquareById(nowSquareId).cross.cross1LoopId;
            int attack1Square = GameMap.Instance.GetSquareById(nowSquareId).cross.cross1squareId;
            int attack2loop = GameMap.Instance.GetSquareById(nowSquareId).cross.cross1LoopId;
            int attack2Square = GameMap.Instance.GetSquareById(nowSquareId).cross.cross1squareId;
            int attack3loop = GameMap.Instance.GetSquareById(nowSquareId).cross.cross2LoopId;
            int attack3Square = GameMap.Instance.GetSquareById(nowSquareId).cross.cross2squareId;
            int attack4loop = GameMap.Instance.GetSquareById(nowSquareId).cross.cross2LoopId;
            int attack4Square = GameMap.Instance.GetSquareById(nowSquareId).cross.cross2squareId;
            List<Character> enemies = GameManager.Instance.GetCharactersBySquareId(attack1Square);
            enemies.Remove(this);
            for (int i = 0; i < range; i++)
            {
                attack1Square = GameMap.Instance.GetnextSquare(attack1loop, attack1Square, 1);
                enemies.AddRange(GameManager.Instance.GetCharactersBySquareId(attack1Square));
                attack2Square = GameMap.Instance.GetnextSquare(attack2loop, attack2Square, -1);
                enemies.AddRange(GameManager.Instance.GetCharactersBySquareId(attack2Square));
                attack3Square = GameMap.Instance.GetnextSquare(attack3loop, attack3Square, 1);
                enemies.AddRange(GameManager.Instance.GetCharactersBySquareId(attack3Square));
                attack4Square = GameMap.Instance.GetnextSquare(attack4loop, attack4Square, -1);
                enemies.AddRange(GameManager.Instance.GetCharactersBySquareId(attack4Square));
                if (single & enemies.Count > 0)
                {
                    Predicate<Character> a = p => gameObject == p.gameObject;
                    enemies.RemoveAll(a);
                    int index = UnityEngine.Random.Range(0, enemies.Count());
                    enemies[index].ReceiveAttack(this, damage);
                    return;
                }
                if (i == range - 1)
                {
                    Predicate<Character> a = p => gameObject == p.gameObject;
                    enemies.RemoveAll(a);
                    List<Character> attackEnemies = enemies.Distinct().ToList();
                    foreach (Character enemy in attackEnemies)
                    {
                        enemy.ReceiveAttack(this, damage);
                    }
                    return;
                }
            }
            print("Miss!");
        }
        else
        {
            int attack1loop = nowLoop;
            int attack1Square = nowSquareId;
            int attack2loop = nowLoop;
            int attack2Square = nowSquareId;
            List<Character> enemies = GameManager.Instance.GetCharactersBySquareId(attack1Square);
            enemies.Remove(this);
            for (int i = 0; i < range; i++)
            {
                attack1Square = GameMap.Instance.GetnextSquare(attack1loop, attack1Square, 1);
                enemies.AddRange(GameManager.Instance.GetCharactersBySquareId(attack1Square));
                attack2Square = GameMap.Instance.GetnextSquare(attack2loop, attack2Square, -1);
                enemies.AddRange(GameManager.Instance.GetCharactersBySquareId(attack2Square));
                if (single & enemies.Count > 0)
                {
                    Predicate<Character> a = p => gameObject == p.gameObject;
                    enemies.RemoveAll(a);
                    int index = UnityEngine.Random.Range(0, enemies.Count());
                    enemies[index].ReceiveAttack(this, damage);
                    return;
                }
                if (i == range - 1)
                {
                    Predicate<Character> a = p => gameObject == p.gameObject;
                    enemies.RemoveAll(a);
                    List<Character> attackEnemies = enemies.Distinct().ToList();
                    foreach (Character enemy in attackEnemies)
                    {
                        enemy.ReceiveAttack(this, damage);
                    }
                    return;
                }
            }
            print("Miss!");
        }
    }
    public void PowerUp(int powerUpAtk, int powerUpHp, int powerUpTurn, int restTurn)
    {
        if (restTurn == 0 && powerUpTurn == 0)
        {
            maxhp += powerUpHp;
            hp += powerUpHp;
            atk += powerUpAtk;
            UIManager.Instance.UpdatePlayerStatus(GameManager.Instance.GetPlayerIdByInstance((Player)this) + 1, hp, maxhp, atk, ((Player)this).havingCard.Count);
        }
        else
        {
            this.restTurn = restTurn;
            this.powerUpTurn = powerUpTurn;
            this.powerUpAtk = powerUpAtk;
            this.powerUpHp = powerUpHp;
        }
    }
    public void HealHp(int healingAmount, int healingTurn, int restTurn)
    {
        this.restTurn = restTurn;
        if (healingTurn == 0)
        {
            if (hp + healingAmount > maxhp) ChangeHp(maxhp - hp);
            else ChangeHp(healingAmount);
        }
        else
        {
            this.healingAmount = healingAmount;
            this.healingTurn = healingTurn;
        }

    }
    public override void ChangeHp(int variation)
    {
        base.ChangeHp(variation);
        if (hp < 0)
        {
            if (CheckAndUseReviveCard())
            {
                ChangeHp(1 - hp);
                UIManager.Instance.UpdateSituationText("プレイヤーは生き残った！");
            }
        }
    }

    public override int GetAtk()
    {
        if (restTurn > 0) return 0;
        return base.GetAtk();
    }
    public override void GetBonus(int hpBonus, int atkBonus)
    {
        base.GetBonus(hpBonus, atkBonus);
        print("Bonus:" + GameManager.Instance.GetPlayerIdByInstance(this) + 1);
        UIManager.Instance.UpdatePlayerStatus(GameManager.Instance.GetPlayerIdByInstance((Player)this) + 1, hp, maxhp, atk, ((Player)this).havingCard.Count);
    }
    public Card GetCard(int index)
    {
        return havingCard[index];
    }
    public int GetCardCount()
    {
        return havingCard.Count;
    }
    private List<int> CalcMovableSquare(int diceNum)
    {
        List<int> movableSquareId = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int nextDirection = i / 2 * 2 - 1;
            int nextWay = i % 2 + 1;
            int nextLoop = 0;
            int nextSquareId = 0;
            if (nextWay == 1)
            {
                nextLoop = GameMap.Instance.GetSquareById(nowSquareId).cross.cross1LoopId;
                nextSquareId = GameMap.Instance.GetSquareById(nowSquareId).cross.cross1squareId;
            }
            else
            {
                nextLoop = GameMap.Instance.GetSquareById(nowSquareId).cross.cross2LoopId;
                nextSquareId = GameMap.Instance.GetSquareById(nowSquareId).cross.cross2squareId;
            }
            for (int j = 0; j < diceNum; j++)
            {
                nextSquareId = GameMap.Instance.GetnextSquare(nextLoop, nextSquareId, nextDirection);
            }
            if (GameMap.Instance.GetSquareById(nextSquareId).species == GameMap.SquareSpecies.Cross || GameMap.Instance.GetSquareById(nextSquareId).species == GameMap.SquareSpecies.Start)
            {

                movableSquareId.Add(GameMap.Instance.GetSquareById(nextSquareId).cross.cross1squareId);
                movableSquareId.Add(GameMap.Instance.GetSquareById(nextSquareId).cross.cross2squareId);
            }
            else
            {
                movableSquareId.Add(nextSquareId);
            }
        }
        return movableSquareId;
    }
    private void CalcMovableSquareAndShine(int diceNum)
    {
        List<int> movableSquareId = CalcMovableSquare(diceNum);
        foreach (int id in movableSquareId)
        {
            MapView.Instance.ShineSquare(id);
        }
    }
    private void CalcMovableSquareAndStopShine(int diceNum)
    {
        List<int> movableSquareId = CalcMovableSquare(diceNum);
        foreach (int id in movableSquareId)
        {
            MapView.Instance.StopShineSquare(id);
        }
    }
}
