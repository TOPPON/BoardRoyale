using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    int hpBonus;
    int atkBonus;
    int moveCount;
    float opeTimer;
    bool firstTurn;
    enum BossOperation
    {
        None,
        BeforeMoveCheck,
        Move,
        MovingAnimation,
        MoveFinish,
        Act,
        Attack
    }
    BossOperation bossOpe;
    // Start is called before the first frame update
    void Start()
    {
        movingDirection = Random.Range(0, 2) * 2 - 1;
        nowLoop = 2;
        nowSquareId = 67;
        Invoke("SetCharacterPositionBynowSquareId", 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMyTurn)
        {
            switch (bossOpe)
            {
                case BossOperation.None:
                    //同じマスに誰かいたときにダメージを与えて動かす
                    if(!firstTurn) BossWakeAttack(atk);
                    bossOpe = BossOperation.BeforeMoveCheck;
                    break;
                case BossOperation.BeforeMoveCheck:
                    //動く方向決め
                    if (GameMap.Instance.GetSquareById(nowSquareId).species == GameMap.SquareSpecies.Cross || GameMap.Instance.GetSquareById(nowSquareId).species == GameMap.SquareSpecies.Start)
                    {
                        movingDirection = Random.Range(0, 2) * 2 - 1;
                        int nextLoop = Random.Range(1, 3);
                        if (nextLoop == 1)
                        {
                            nowLoop = GameMap.Instance.GetSquareById(nowSquareId).cross.cross1LoopId;
                            nowSquareId = GameMap.Instance.GetSquareById(nowSquareId).cross.cross1squareId;
                        }
                        else
                        {
                            nowLoop = GameMap.Instance.GetSquareById(nowSquareId).cross.cross2LoopId;
                            nowSquareId = GameMap.Instance.GetSquareById(nowSquareId).cross.cross2squareId;
                        }
                    }
                    //消すときのマップ影響の配慮
                    else if (GameMap.Instance.GetSquareById(nowSquareId).species != GameMap.SquareSpecies.Boss)
                    {
                        int tempSquareId = nowSquareId;
                        nowSquareId = GameMap.Instance.GetnextSquare(nowLoop, nowSquareId, movingDirection * -1);
                        GameMap.Instance.LoseSquare(nowLoop, tempSquareId);
                    }
                    moveCount = Random.Range(1, 4);
                    bossOpe = BossOperation.Move;
                    break;
                case BossOperation.Move:
                    if (firstTurn) firstTurn = false;
                    if (moveCount > 0)
                    {
                        nowSquareId = GameMap.Instance.GetnextSquare(nowLoop, nowSquareId, movingDirection);
                        bossOpe = BossOperation.MovingAnimation;
                        opeTimer = 0;
                        moveCount--;
                        SetCharacterPositionBynowSquareId();
                    }
                    else
                    {
                        bossOpe = BossOperation.MoveFinish;
                    }
                    break;
                case BossOperation.MovingAnimation:
                    opeTimer += Time.deltaTime;
                    if (opeTimer > 0.2f)
                    {
                        opeTimer = 0;
                        bossOpe = BossOperation.Move;
                    }
                    break;
                case BossOperation.MoveFinish:
                    opeTimer += Time.deltaTime;
                    if (opeTimer > 0.3f)
                    {
                        DiceView.Instance.EraceDice();
                        bossOpe = BossOperation.Attack;
                        opeTimer = 0;
                    }
                    break;
                case BossOperation.Act:
                    //ランダムでカードみたいな行動をさせたいが未定
                    break;
                case BossOperation.Attack:
                    SameSquareAttack();
                    TurnEnd();
                    break;
            }
        }
    }
    public override int GetAtk()
    {
        if (!firstTurn) return atk;
        return 0;
    }
    public override void TurnStart()
    {
        base.TurnStart();
        bossOpe = BossOperation.None;
    }
    public void BossWakeAttack(int damage)
    {
        List<Character> sameSquarePlayers = GameManager.Instance.GetCharactersBySquareId(nowSquareId);
        foreach (Character chara in sameSquarePlayers)
        {
            if (chara.GetType() == typeof(Player))
            {
                chara.movingDirection = Random.Range(0, 2) * 2 - 1;
                ((Player)chara).Move(1);
                chara.ReceiveAttack(this, damage);
            }
        }
    }
    public void SetFirstStatus(int hp, int atk, int hpBonus, int atkBonus)
    {
        base.SetFirstStatus(hp, atk);
        this.hpBonus = hpBonus;
        this.atkBonus = atkBonus;
        this.firstTurn = true;
    }
    protected override void SetCharacterPositionBynowSquareId()
    {
        gameObject.transform.position = MapView.Instance.GetPlayerPositionById(nowSquareId) + gapCenter;
    }
    public override int ReceiveAttack(Character attacker, int damage)
    {
        if (!firstTurn)
        {
            int result = base.ReceiveAttack(attacker, damage);
            if (result == -1)
            {
                attacker.GetBonus(this.hpBonus, this.atkBonus);
                //ボスを消す
                GameManager.Instance.DefeatBoss();
            }
            return result;
        }
        return 0;
    }
}
