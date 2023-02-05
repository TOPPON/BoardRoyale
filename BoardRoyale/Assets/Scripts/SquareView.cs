using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareView : MonoBehaviour
{
    bool shining = false;
    float shiningTimer;
    private SpriteRenderer sprite;
    float SHININGTIME = 1f;
    bool losing = false;
    float losingTimer;
    float LOSINGTIME = 1f;
    float defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        defaultScale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (shining)
        {
            shiningTimer += Time.deltaTime;
            sprite.color = new Color(1, 1, 1,1- Mathf.Abs(shiningTimer - SHININGTIME / 2)/SHININGTIME);
            if (shiningTimer > SHININGTIME) shiningTimer -= SHININGTIME;
        }
        if (losing)
        {
            losingTimer -= Time.deltaTime;
            gameObject.transform.localScale = new Vector3(losingTimer / LOSINGTIME * defaultScale, losingTimer / LOSINGTIME * defaultScale);
            if (losingTimer<0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void ShiningStart()
    {
        if (sprite != null)
        {
            shining = true;
            shiningTimer = 0;
        }
    }
    public void ShiningStop()
    {
        if (sprite != null)
        {
            sprite.color = new Color(1, 1, 1, 1);
            shining = false;
        }
    }
    public void LoseSquare()
    {
        losing = true;
        losingTimer = 1f;
    }
}
