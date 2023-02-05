using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public int selectingCard = 0;
    int cardCount = 0;
    int count = 0;//デバッグ用
    [SerializeField] Button CardButton;
    [SerializeField] Button DiceButton;
    [SerializeField] Button BackButton;
    [SerializeField] Button TrashButton;
    [SerializeField] Button UseButton;
    [SerializeField] Button Card1Button;
    [SerializeField] Button Card2Button;
    [SerializeField] Button Card3Button;
    [SerializeField] Button Card4Button;
    [SerializeField] Image Card1SelectUI;
    [SerializeField] Image Card2SelectUI;
    [SerializeField] Image Card3SelectUI;
    [SerializeField] Image Card4SelectUI;
    [SerializeField] Text[] CardSelectNumberTexts;
    [SerializeField] Text[] CardSelectGenreTexts;
    [SerializeField] Text[] CardSelectRestTurnTexts;
    [SerializeField] Text[] CardSelectMainValTexts;
    [SerializeField] Text[] CardSelectSubValTexts;
    [SerializeField] GameObject CardSeeView;
    [SerializeField] Text SituationText;//だれだれのターンです
    [SerializeField] Text TurnText;//Turns : 30
    [SerializeField] GameObject BossDisplay;

    [SerializeField] GameObject PlayerStatusPrefab;
    [SerializeField] GameObject canvas;

    List<PlayerStatusView> playerStatusViews = new List<PlayerStatusView>();
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    public void AttachOperationButton(Player player)
    {
        DiceButton.onClick.RemoveAllListeners();
        DiceButton.onClick.AddListener(player.DiceRoll);
        DiceButton.onClick.AddListener(DiceRoll);
        CardButton.onClick.RemoveAllListeners();
        CardButton.onClick.AddListener(player.SeeCard);
        CardButton.onClick.AddListener(SeeCard);
        BackButton.onClick.RemoveAllListeners();
        BackButton.onClick.AddListener(player.BackCard);
        BackButton.onClick.AddListener(BackCard);
        TrashButton.onClick.RemoveAllListeners();
        TrashButton.onClick.AddListener(player.TrashSeeCard);
        TrashButton.onClick.AddListener(TrashSeeCard);
        UseButton.onClick.RemoveAllListeners();
        UseButton.onClick.AddListener(player.UseCard);
        UseButton.onClick.AddListener(UseCard);
        cardCount = player.GetCardCount();
        SetCardUI(player, cardCount);
    }
    public void AttachTrashCardSelectButton(Player player)
    {
        TrashButton.onClick.RemoveAllListeners();
        TrashButton.onClick.AddListener(player.TrashExtraCard);
        TrashButton.onClick.AddListener(TrashExtraCard);
        cardCount = player.GetCardCount();
        SetCardUI(player, 4);
    }
    public void TurnStart()
    {
        CardSeeView.SetActive(false);
        DiceButton.gameObject.SetActive(true);
        CardButton.gameObject.SetActive(true);
    }
    public void SetMovingArrow(Character chara)
    {
        Vector3 tempCoordinate =
   MapView.Instance.GetTwoSquareCoordinate(chara.nowSquareId,
   GameMap.Instance.GetnextSquareWithLose(chara.nowLoop, chara.nowSquareId, chara.movingDirection));
        chara.movingWay.rotation =
                     Quaternion.Euler(0, 0, Mathf.Atan2(tempCoordinate.y, tempCoordinate.x) * Mathf.Rad2Deg);
    }
    // Update is called once per frame
    void Update()
    {

    }
    void SeeCard()
    {
        CardSeeView.SetActive(true);
        DiceButton.gameObject.SetActive(false);
        CardButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(true);
        TrashButton.gameObject.SetActive(true);
        UseButton.gameObject.SetActive(true);
        Card1Button.gameObject.SetActive(false);
        Card2Button.gameObject.SetActive(false);
        Card3Button.gameObject.SetActive(false);
        Card4Button.gameObject.SetActive(false);
        Card1SelectUI.enabled = false;
        Card2SelectUI.enabled = false;
        Card3SelectUI.enabled = false;
        Card4SelectUI.enabled = false;
        //カード情報のアップデートをしていない
        SetCardUI((Player)GameManager.Instance.nowPlayer, cardCount);
        if (cardCount >= 1) Card1Button.gameObject.SetActive(true);
        if (cardCount >= 2) Card2Button.gameObject.SetActive(true);
        if (cardCount >= 3) Card3Button.gameObject.SetActive(true);
        selectingCard = 0;
    }
    void DiceRoll()
    {
        CardSeeView.SetActive(false);
        DiceButton.gameObject.SetActive(false);
        CardButton.gameObject.SetActive(false);
    }
    public void TrashCardSelect()
    {
        CardSeeView.SetActive(true);
        DiceButton.gameObject.SetActive(false);
        CardButton.gameObject.SetActive(false);
        UseButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        TrashButton.gameObject.SetActive(true);
        Card1Button.gameObject.SetActive(true);
        Card2Button.gameObject.SetActive(true);
        Card3Button.gameObject.SetActive(true);
        Card4Button.gameObject.SetActive(true);
        Card1SelectUI.enabled = false;
        Card2SelectUI.enabled = false;
        Card3SelectUI.enabled = false;
        Card4SelectUI.enabled = false;
    }
    public void BackCard()
    {
        CardSeeView.SetActive(false);
        DiceButton.gameObject.SetActive(true);
        CardButton.gameObject.SetActive(true);
    }
    void UseCard()
    {
        if (selectingCard != 0)
        {
            CardSeeView.SetActive(false);
        }
        else
        {
            UpdateSituationText("使うカードを選択してください");
        }
    }
    void TrashSeeCard()
    {
        if (selectingCard != 0)
        {
            cardCount--;
            SeeCard();
        }
        else
        {
            UpdateSituationText("捨てるカードを選択してください");
        }
    }
    void TrashExtraCard()
    {
        if (selectingCard != 0)
        {
            CardSeeView.SetActive(false);
        }
        else
        {
            UpdateSituationText("捨てるカードを選択してください");
        }
    }
    public void Card1Select()
    {
        selectingCard = 1;
        Card1SelectUI.enabled = true;
        Card2SelectUI.enabled = false;
        Card3SelectUI.enabled = false;
        Card4SelectUI.enabled = false;
    }
    public void Card2Select()
    {
        selectingCard = 2;
        Card1SelectUI.enabled = false;
        Card2SelectUI.enabled = true;
        Card3SelectUI.enabled = false;
        Card4SelectUI.enabled = false;
    }
    public void Card3Select()
    {
        selectingCard = 3;
        Card1SelectUI.enabled = false;
        Card2SelectUI.enabled = false;
        Card3SelectUI.enabled = true;
        Card4SelectUI.enabled = false;
    }
    public void Card4Select()
    {
        selectingCard = 4;
        Card1SelectUI.enabled = false;
        Card2SelectUI.enabled = false;
        Card3SelectUI.enabled = false;
        Card4SelectUI.enabled = true;
    }
    void SetCardUI(Player player, int cardcount)
    {
        for (int i = 0; i < cardCount; i++)
        {
            Card card = player.GetCard(i);
            CardSelectNumberTexts[i].text = "No."+card.id.ToString();
            CardSelectGenreTexts[i].text = "Type:"+card.genre.ToString();
            CardSelectRestTurnTexts[i].text = "Rest:"+card.restTurn.ToString();
            switch (card.genre)
            {
                case Card.CardGenre.Move:
                    CardSelectMainValTexts[i].text = card.moveSquareNumber.ToString();
                    CardSelectSubValTexts[i].text = "";
                    break;
                case Card.CardGenre.Attack:
                    CardSelectMainValTexts[i].text = "D:"+card.attackDamage.ToString();
                    CardSelectSubValTexts[i].text = "R:" + card.attackRange.ToString() + (card.attackSingle ? " Single" : " All");
                    break;
                case Card.CardGenre.PowerUP:

                    CardSelectMainValTexts[i].text = "A:" + card.powerUpAtk.ToString()+ " H:" + card.powerUpHp.ToString();
                    CardSelectSubValTexts[i].text = "Rest:"+card.restTurn+" Turn:"+card.powerUpTurn;
                    break;
                case Card.CardGenre.Heal:
                    CardSelectMainValTexts[i].text = "HP:"+card.healHp.ToString();
                    CardSelectSubValTexts[i].text = "Turn:"+card.healingTurn.ToString();
                    break;
                case Card.CardGenre.Other:
                    CardSelectMainValTexts[i].text = "Change Direction";
                    CardSelectSubValTexts[i].text = "";
                    break;
                case Card.CardGenre.Auto:
                    CardSelectMainValTexts[i].text = "AutoRevive";
                    CardSelectSubValTexts[i].text = "";
                    break;
            }
        }
    }
    public void SetChoiceWayButton(Player player)
    {
        if (player.wayButtons.Count != 4)
        {
            print("error! wayButtons is not 4");
            return;
        }
        for (int i = 0; i < player.wayButtons.Count; i++)
        {
            player.wayButtons[i].SetActive(true);
            player.wayButtons[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            int nextDirection = i / 2 * 2 - 1;
            int nextWay = i % 2 + 1;
            int nextLoop = 0;
            int nextSquareId = 0;
            if (nextWay == 1)
            {
                nextLoop = GameMap.Instance.GetSquareById(player.nowSquareId).cross.cross1LoopId;
                nextSquareId = GameMap.Instance.GetSquareById(player.nowSquareId).cross.cross1squareId;/*ここにスクエアとWayもいれる*/
            }
            else
            {
                nextLoop = GameMap.Instance.GetSquareById(player.nowSquareId).cross.cross2LoopId;
                nextSquareId = GameMap.Instance.GetSquareById(player.nowSquareId).cross.cross2squareId;/*ここにスクエアとWayもいれる*/
            }
            player.wayButtons[i].GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                player.movingDirection = nextDirection;
                player.nowLoop = nextLoop;
                player.nowSquareId = nextSquareId;
                player.isPushedChoiceWayButton = true;
                foreach (GameObject wayButton in player.wayButtons)
                {
                    wayButton.SetActive(false);
                }
            });
            Vector3 tempCoordinate =
                 MapView.Instance.GetTwoSquareCoordinate(nextSquareId,
                 GameMap.Instance.GetnextSquareWithLose(nextLoop, nextSquareId, nextDirection));
            player.wayButtons[i].GetComponent<RectTransform>().rotation =
                 Quaternion.Euler(0, 0, Mathf.Atan2(tempCoordinate.y, tempCoordinate.x) * Mathf.Rad2Deg);

        }
    }
    public void GetCard(Player player)
    {

    }
    public void CreatePlayerStatus(int playerNumber, int hp, int maxHp, int atk, int cardCount)
    {
        GameObject obj = Instantiate(PlayerStatusPrefab, canvas.transform);
        obj.GetComponent<RectTransform>().localPosition = new Vector3(710, (playerNumber - 1) * -266 + 399);
        obj.GetComponent<PlayerStatusView>().RefreshPlayerStatus(playerNumber, hp, maxHp, atk, cardCount);
        playerStatusViews.Add(obj.GetComponent<PlayerStatusView>());
    }
    public void UpdatePlayerStatus(int playerNumber, int hp, int maxHp, int atk, int cardCount)
    {
        playerStatusViews[playerNumber - 1].RefreshPlayerStatus(playerNumber, hp, maxHp, atk, cardCount);
    }
    public void ChangeHp(Character character, int varidation, int hp, int maxHp)
    {
        if (character.GetType() == typeof(Player))
        {
            int playerId = GameManager.Instance.GetPlayerIdByInstance((Player)character);
            if (playerId != -1)
            {
                playerStatusViews[playerId].ReduceHp(varidation, hp, maxHp);
            }
        }
        else
        {
            BossDisplay.GetComponent<BossStatusView>().ReduceHp(varidation, hp, maxHp);
        }
    }
    public void UpdateStatusCard(Player player, List<Card> lastCards)
    {
        int playerId = GameManager.Instance.GetPlayerIdByInstance(player);
        if (playerId != -1)
        {
            playerStatusViews[playerId].RefreshCard(lastCards);
        }
    }
    public void UpdateSituationText(string situationText)
    {
        count++;
        SituationText.text = situationText;
        print("Si"+count+":"+situationText);
    }
    public void UpdateTurnsText(int turns)
    {
        TurnText.text = "Turns : " + turns.ToString();
    }
    public void AppearBoss(int maxHp, int atk, int atkBonus, int hpBonus)
    {
        BossDisplay.SetActive(true);
        BossDisplay.GetComponent<BossStatusView>().AppearBoss(maxHp, atk, atkBonus, hpBonus);
    }
    public void DefeatBoss()
    {
        BossDisplay.SetActive(false);
    }
}
