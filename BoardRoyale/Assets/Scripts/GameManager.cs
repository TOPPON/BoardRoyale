using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<Character> characters = new List<Character>();
    public int gameTurn;
    public Character nowPlayer;
    public bool isAppearBoss;
    float nextPlayerWaitingTimer = 0;

    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameObject BossPrefab;
    [SerializeField] List<Sprite> PlayerImages;
    public class GameSettings
    {
        public int playerNum;
        public int defaulthp;
    }
    public GameSettings gameSettings = new GameSettings();
    // Start is called before the first frame update
    void Start()
    {
        gameSettings.playerNum = 4;
        gameSettings.defaulthp = 10;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Invoke(nameof(GameStart), 0.1f);
        //Invoke(nameof(AppearBoss), 0.5f);
    }
    public void GameStart()
    {
        for (int i = 0; i < gameSettings.playerNum; i++)
        {
            GameObject obj = Instantiate(PlayerPrefab);
            obj.GetComponent<SpriteRenderer>().sprite = PlayerImages[i];
            characters.Add(obj.GetComponent<Player>());
            characters[i].SetFirstStatus(gameSettings.defaulthp, 1);
            characters[i].gapCenter = new Vector3(i / 2 * 0.2f - 0.1f, i % 2 * 0.2f + 0.05f);
            UIManager.Instance.CreatePlayerStatus(i + 1, gameSettings.defaulthp, gameSettings.defaulthp, 1, 0);
        }
        Card.MakeCards();
        nowPlayer = characters[characters.Count - 1];
        gameTurn = 0;
        NextPlayerTrun();
        isAppearBoss = false;
        foreach (Character chara in characters)
        {
            UIManager.Instance.SetMovingArrow(chara);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(nextPlayerWaitingTimer>0)
        {
            nextPlayerWaitingTimer -= Time.deltaTime;
            if (nextPlayerWaitingTimer <= 0)
            {
                NextPlayerTrun();
            }
        }
    }
    public void TurnEnd()
    {
        nextPlayerWaitingTimer = 0.2f;
        foreach (Character chara in characters)
        {
            UIManager.Instance.SetMovingArrow(chara);
        }
    }
    public void NextPlayerTrun()
    {
        if (CheckGameFinish())
        {
            UIManager.Instance.UpdateSituationText("ゲーム終了！");
            return;
        }
        int nextPlayerIndex = characters.IndexOf(nowPlayer);
        while (true)
        {
            nextPlayerIndex++;
            if (nextPlayerIndex >= characters.Count)
            {
                nextPlayerIndex = 0;
                gameTurn++;
                UIManager.Instance.UpdateTurnsText(gameTurn);
            }
            if (characters[nextPlayerIndex].CheckIsLive()) break;
        }
        nowPlayer = characters[nextPlayerIndex];
        nowPlayer.TurnStart();
        //もしボスじゃなかったら、みたいなのをいれる
        if (nowPlayer.GetType() == typeof(Player))
        {
            UIManager.Instance.TurnStart();
            UIManager.Instance.AttachOperationButton((Player)nowPlayer);
            UIManager.Instance.UpdateSituationText((nextPlayerIndex + 1).ToString() + "Pのターン！");
        }
        else
        {
            UIManager.Instance.UpdateSituationText("ボスのターン！");
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
        //obj.GetComponent<Boss>().SetFirstStatus(1, 2, 3, 1);
        obj.GetComponent<Boss>().SetFirstStatus(gameSettings.defaulthp, 2,3,1);
        obj.GetComponent<Character>().gapCenter = new Vector3(0, 0.4f);
        UIManager.Instance.AppearBoss(1, 2, 3, 1);
        UIManager.Instance.AppearBoss(gameSettings.defaulthp, 2,3,1);
        isAppearBoss = true;
    }
    public void DefeatBoss()
    {
        isAppearBoss = false;
        //ボスのUIをけす
        UIManager.Instance.DefeatBoss();
        //ボスの画像をけす
        //キャラクター一覧からボスを消す
        foreach (Character a in characters)
        {
            if (a.GetType() == typeof(Boss))
            {
                print("destroy:"+a.GetType());
                characters.Remove(a);
                Destroy(a.gameObject);
                break;
            }
        }
    }
    public List<Character> GetCharactersBySquareId(int squareId)
    {
        List<Character> aSquareCharacters = new List<Character>();
        foreach (Character chara in characters)
        {
            if (GameMap.Instance.IsSameSquare(chara.nowSquareId, squareId))
            {
                if (chara.CheckIsLive()) aSquareCharacters.Add(chara);
                continue;
            }
        }
        return aSquareCharacters;
    }
    public int GetPlayerIdByInstance(Player player)
    {
        Predicate<Character> a = p => player.gameObject == p.gameObject;
        return characters.FindIndex(a);
    }
    public bool CheckGameFinish()
    {
        int count = 0;
        foreach (Character aChara in characters)
        {
            if (aChara.GetType() == typeof(Player) & aChara.CheckIsLive()) count++;
        }
        if (count == 1) return true;
        else if (count >= 2) return false;
        else//相打ちなど
        {
            print("error! Living players count is less than 1");
            return false;
        }
    }
}
