using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    enum BossOperation
    {
        None,
        Move,
        Act
    }
    BossOperation bossOpe;
    // Start is called before the first frame update
    void Start()
    {
        movingDirection = Random.Range(0, 2) * 2 - 1; 
        nowLoop = 2;
        nowSquareId = 67;
        gameObject.transform.position = MapView.Instance.GetPlayerPositionById(nowSquareId);
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
                    BossWakeAttack(atk);
                    bossOpe = BossOperation.Move;
                    break;
                case BossOperation.Move:
                    //同じマスに誰かがいた時の処理

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
                    int dicenum = Random.Range(1,4);
                    for (int i = 0; i < dicenum; i++)
                    {
                        nowSquareId = GameMap.Instance.GetnextSquare(nowLoop, nowSquareId, movingDirection);
                    }
                    gameObject.transform.position = MapView.Instance.GetPlayerPositionById(nowSquareId);
                    TurnEnd();
                    break;
                case BossOperation.Act:

                    break;
            }
        }
    }
    public override void TurnStart()
    {
        base.TurnStart();
        bossOpe = BossOperation.None;
    }
    public void BossWakeAttack(int damage)
    {
        List<Character> sameSquarePlayers = GameManager.Instance.GetCharactersBySquareId(nowSquareId);
        foreach(Character chara in sameSquarePlayers)
        {
            if (chara.GetType() == typeof(Player))
            {
                chara.movingDirection = Random.Range(0, 2) * 2 - 1;
                ((Player)chara).Move(1);
                chara.ReceiveDamage(damage);
            }
        }
    }
}
