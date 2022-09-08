using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<Character> characters=new List<Character>();
    public int gameTurn;
    public Character nowPlayer;
    public bool isAppearBoss;
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameObject BossPrefab;
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
        Invoke(nameof(AppearBoss), 0.5f);
    }
    public void GameStart()
    {
        for (int i=0;i<gameSettings.playerNum; i++)
        {
            GameObject obj=Instantiate(PlayerPrefab);
            characters.Add(obj.GetComponent<Player>());
            characters[i].SetFirstStatus(gameSettings.defaulthp, 1);
        }
        nowPlayer = characters[characters.Count-1];
        gameTurn=1;
        NextPlayerTrun();
        isAppearBoss = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextPlayerTrun()
    {
        int nextPlayerIndex = characters.IndexOf(nowPlayer) + 1;
        if (nextPlayerIndex >= characters.Count)
        {
            nextPlayerIndex = 0;
            gameTurn++;
        }
        nowPlayer = characters[nextPlayerIndex];
        nowPlayer.TurnStart();
        UIManager.Instance.TurnStart();
        //もしボスじゃなかったら、みたいなのをいれる
        if (nowPlayer.GetType() == typeof(Player))
        {
            UIManager.Instance.AttachOperationButton((Player)nowPlayer);
        }
        //UIなどもここに入れる(○○のターン！みたいな)
    }
    public void ArriveBossSquare()//現在のプレイヤーがボスマスに到着した
    {
        if (isAppearBoss)
        {

        }
        else
        {
            AppearBoss();
        }
    }
    public void AppearBoss()
    {
        GameObject obj = Instantiate(BossPrefab);
        characters.Add(obj.GetComponent<Character>());
        obj.GetComponent<Character>().SetFirstStatus(gameSettings.defaulthp, 2);
        isAppearBoss = true;
    }
    public List<Character> GetCharactersBySquareId(int squareId)
    {
        List<Character> aSquareCharacters = new List<Character>();
        foreach(Character chara in characters)
        {
            if (GameMap.Instance.IsSameSquare(chara.nowSquareId, squareId))
            {
                aSquareCharacters.Add(chara);
                continue;
            }
        }
        return aSquareCharacters;
    }
}
