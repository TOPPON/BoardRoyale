using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatusView : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Text atkText;
    [SerializeField] GameObject nowHpGage;
    [SerializeField] GameObject maxHpGage;
    [SerializeField] Text hpBonusText;
    [SerializeField] Text atkBonusText;
    [SerializeField] Image hpBonusImage;
    [SerializeField] Image atkBonusImage;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void AppearBoss(int maxHp, int atk, int hpBonus, int atkBonus)
    {
        hpText.text = maxHp.ToString() + " / " + maxHp.ToString();
        atkText.text = atk.ToString();
        if (atkBonus <= 0)
        {
            atkBonusImage.enabled = false;
            atkBonusText.enabled = false;
        }
        else
        {
            atkBonusImage.enabled = true;
            atkBonusText.enabled = true;
            atkBonusText.text = atkBonus.ToString();
        }
        if (hpBonus <= 0)
        {
            hpBonusImage.enabled = false;
            hpBonusText.enabled = false;
        }
        else
        {
            hpBonusImage.enabled = true;
            hpBonusText.enabled = true;
            hpBonusText.text = hpBonus.ToString();
        }
        nowHpGage.GetComponent<RectTransform>().sizeDelta = maxHpGage.GetComponent<RectTransform>().sizeDelta;
        nowHpGage.GetComponent<RectTransform>().localPosition = maxHpGage.GetComponent<RectTransform>().localPosition;
    }
    // Update is called once per frame
    void Update()
    {

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
}
