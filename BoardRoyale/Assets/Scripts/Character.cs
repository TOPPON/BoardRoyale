using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int nowSquareId;
    public int nowLoop;
    public int movingDirection;
    protected int hp;
    protected int maxhp;
    protected int atk;

    //状態異常系
    protected int restTurn;//休み

    public Transform movingWay;

    protected bool isMyTurn;
    public Vector3 gapCenter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public virtual void SetFirstStatus(int hp, int atk)
    {
        maxhp = hp;
        this.hp = hp;
        this.atk = atk;
    }
    public virtual void TurnStart()
    {
        isMyTurn = true;
        if (restTurn > 0)
        {
            UIManager.Instance.UpdateSituationText("あと" + restTurn.ToString() + "ターン休み！");
            restTurn--;
            TurnEnd();
        }
    }
    protected virtual void TurnEnd()
    {
        isMyTurn = false;
        GameManager.Instance.TurnEnd();
    }
    public virtual void ChangeHp(int variation)
    {
        hp += variation;
        UIManager.Instance.ChangeHp(this, variation, hp, maxhp);
    }
    public virtual int GetAtk()
    {
        return atk;
    }
    protected void SameSquareAttack()
    {
        List<Character> sameSquarePlayers = GameManager.Instance.GetCharactersBySquareId(nowSquareId);
        foreach (Character chara in sameSquarePlayers)
        {
            if (chara != this)
            {
                //ボスが最初に攻撃されないように
                if (chara.GetType() == typeof(Player))
                {
                    chara.ReceiveAttack(this, atk);
                    ReceiveAttack(chara, chara.GetAtk());
                }
                else
                {
                    ReceiveAttack(chara, chara.GetAtk());
                    chara.ReceiveAttack(this, atk);
                }
            }
        }
    }
    protected virtual void SetCharacterPositionBynowSquareId()
    {
    }
    public bool CheckIsLive()
    {
        if (hp <= 0) return false;
        else return true;
    }
    //攻撃を受ける。遠距離やWakeUpAttackも適用
    public virtual int ReceiveAttack(Character attacker, int damage)
    {
        ChangeHp(-damage);
        if (CheckIsLive())
        {
            return damage;
        }
        else
        {
            return -1;//Dead
        }
    }
    public virtual void GetBonus(int hpBonus, int atkBonus)
    {
        hp += hpBonus;
        maxhp += hpBonus;
        atk += atkBonus;
    }
}
