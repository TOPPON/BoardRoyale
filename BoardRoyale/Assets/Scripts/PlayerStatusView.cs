using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusView : MonoBehaviour
{
    [SerializeField] Text numberText;
    [SerializeField] Text hpText;
    [SerializeField] Text atkText;
    [SerializeField] GameObject nowHpGage;
    [SerializeField] GameObject maxHpGage;
    [SerializeField] Image Card1;
    [SerializeField] Image Card2;
    [SerializeField] Image Card3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RefreshPlayerStatus(int playerNumber, int hp, int maxHp, int atk, int cardCount)
    {
        numberText.text = playerNumber.ToString() + "P";
        switch (playerNumber)
        {
            case 1:
                numberText.color = Color.red;
                gameObject.GetComponent<Image>().color = new Color(0.764151f, 0.5731132f, 0.5731132f, 1);
                break;
            case 2:
                numberText.color = Color.blue;
                gameObject.GetComponent<Image>().color = new Color(0.5801887f, 0.5917347f, 0.7735849f, 1);
                break;
            case 3:
                numberText.color = Color.green;
                gameObject.GetComponent<Image>().color = new Color(0.6616234f, 0.7924528f, 0.6910599f, 1);
                break;
            case 4:
                numberText.color = Color.yellow;
                gameObject.GetComponent<Image>().color = new Color(0.7735849f, 0.7735849f, 0.5947846f, 1);
                break;
        }
        hpText.text = hp.ToString() + " / " + maxHp.ToString();
        atkText.text = atk.ToString();
        Vector2 sd = nowHpGage.GetComponent<RectTransform>().sizeDelta;
        Vector2 maxsd = maxHpGage.GetComponent<RectTransform>().sizeDelta;
        sd.x = maxsd.x * hp / maxHp;
        nowHpGage.GetComponent<RectTransform>().sizeDelta = sd;
        Vector3 nowHpPos = maxHpGage.GetComponent<RectTransform>().localPosition;
        nowHpPos.x -= (maxsd.x - sd.x) / 2;
        nowHpGage.GetComponent<RectTransform>().localPosition = nowHpPos;
        switch (cardCount)
        {
            case 0:
                Card1.enabled = false;
                Card2.enabled = false;
                Card3.enabled = false;
                break;
            case 1:
                Card1.enabled = true;
                Card2.enabled = false;
                Card3.enabled = false;
                break;
            case 2:
                Card1.enabled = true;
                Card2.enabled = true;
                Card3.enabled = false;
                break;
            case 3:
                Card1.enabled = true;
                Card2.enabled = true;
                Card3.enabled = true;
                break;
            default:
                Card1.enabled = false;
                Card2.enabled = false;
                Card3.enabled = false;
                break;
        }
    }
    public void ReduceHp(int damage, int hp, int maxHp)
    {
        hpText.text = hp.ToString() + " / " + maxHp.ToString();
        Vector2 sd = nowHpGage.GetComponent<RectTransform>().sizeDelta;
        Vector2 maxsd = maxHpGage.GetComponent<RectTransform>().sizeDelta;
        sd.x = maxsd.x * hp / maxHp;
        nowHpGage.GetComponent<RectTransform>().sizeDelta = sd;
        Vector3 nowHpPos = maxHpGage.GetComponent<RectTransform>().localPosition;
        nowHpPos.x -= (maxsd.x - sd.x) / 2;
        nowHpGage.GetComponent<RectTransform>().localPosition = nowHpPos;
    }
    public void RefreshCard(List<Card> havingCard)
    {
        Card1.enabled = false;
        Card2.enabled = false;
        Card3.enabled = false;
        switch (havingCard.Count)
        {
            case 0:
                break;
            case 1:
                Card1.enabled = true;
                break;
            case 2:
                Card1.enabled = true;
                Card2.enabled = true;
                break;
            case 3:
                Card1.enabled = true;
                Card2.enabled = true;
                Card3.enabled = true;
                break;
        }
    }
}