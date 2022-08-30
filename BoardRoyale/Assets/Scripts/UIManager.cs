using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Button CardButton;
    [SerializeField] Button DiceButton;
    [SerializeField] Button BackButton;
    [SerializeField] Button TrashButton;
    [SerializeField] Button UseButton;
    [SerializeField] Button Card1Button;
    [SerializeField] Button Card2Button;
    [SerializeField] Button Card3Button;
    [SerializeField] Button Card4Button;
    [SerializeField] Text SituationText;//だれだれのターンです
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
    }
    public void TurnStart()
    {
        DiceButton.gameObject.SetActive(true);
        CardButton.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(false);
        TrashButton.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
    void SeeCard()
    {
        DiceButton.gameObject.SetActive(false);
        CardButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(true);
        TrashButton.gameObject.SetActive(false);
    }
    void DiceRoll()
    {
        DiceButton.gameObject.SetActive(false);
        CardButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        TrashButton.gameObject.SetActive(false);
    }
    void GetCard()
    {

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
                foreach(GameObject wayButton in player.wayButtons)
                {
                    wayButton.SetActive(false);
                }
            });
            Vector3 tempCoordinate =
                 MapView.Instance.GetTwoSquareCoordinate(nextSquareId,
                 GameMap.Instance.GetnextSquare(nextLoop, nextSquareId, nextDirection));
            player.wayButtons[i].GetComponent<RectTransform>().rotation =
                 Quaternion.Euler(0, 0, Mathf.Atan2(tempCoordinate.y,tempCoordinate.x)*Mathf.Rad2Deg);

        }
    }
    public void SetChoiceWayButton()
    {

    }
}
