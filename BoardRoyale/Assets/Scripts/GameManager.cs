using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<Player> players=new List<Player>();
    public int gameTurn;
    public Player nowPlayer;
    [SerializeField] GameObject PlayerPrefab;
    public class GameSettings
    {
        public int playerNum;
        public int defaulthp;
    }
    public GameSettings gameSettings=new GameSettings();
    // Start is called before the first frame update
    void Start()
    {
        gameSettings.playerNum = 2;
        gameSettings.defaulthp = 10;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Invoke(nameof(GameStart), 0.5f);
    }
    public void GameStart()
    {
        for (int i=0;i<gameSettings.playerNum; i++)
        {
            GameObject obj=Instantiate(PlayerPrefab);
            players.Add(obj.GetComponent<Player>());
            players[i].SetFirstStatus(gameSettings.defaulthp, 1);
        }
        nowPlayer = players[players.Count-1];
        gameTurn=1;
        NextPlayerTrun();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextPlayerTrun()
    {
        print("NextPlayer");
        int nextPlayerIndex = players.IndexOf(nowPlayer) + 1;
        if (nextPlayerIndex >= players.Count)
        {
            nextPlayerIndex = 0;
            gameTurn++;
        }
        nowPlayer = players[nextPlayerIndex];
        nowPlayer.TurnStart();
        UIManager.Instance.TurnStart();
        UIManager.Instance.AttachOperationButton(nowPlayer);
        //UIなどもここに入れる(○○のターン！みたいな)
    }
    public void ArriveBossSquare()//現在のプレイヤーがボスマスに到着した
    {
    }
    public void AppearBoss()
    {
        //players.Add(Boss)
    }
}
