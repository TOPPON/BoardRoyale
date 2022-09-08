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
    protected int restTurn;//休み
    protected bool isMyTurn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetFirstStatus(int hp, int atk)
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
            restTurn--;
            TurnEnd();
        }
    }
    protected virtual void TurnEnd()
    {
        isMyTurn = false;
        GameManager.Instance.NextPlayerTrun();
    }
    public void ReceiveDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            print("dead!");
        }
    }
}
