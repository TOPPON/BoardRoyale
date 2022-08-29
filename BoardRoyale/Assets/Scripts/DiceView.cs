using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceView : MonoBehaviour
{
    public static DiceView Instance;
    float timer = 0;
    public bool rolling = false;
    public int diceNumber = 1;
    [SerializeField] List<Sprite> diceImages;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
        gameObject.GetComponent<Image>().enabled = false;
    }
    public void Roll()
    {
        timer = 0;
        rolling = true;
        gameObject.GetComponent<Image>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (rolling)
        {
            if((int)(timer*20)%10!= (int)((timer+Time.deltaTime) * 20) % 10)
            {
                diceNumber = Random.Range(1, 6);
                gameObject.GetComponent<Image>().sprite = diceImages[diceNumber - 1];
            }
            timer += Time.deltaTime;
            if (timer > 1)
            {
                rolling = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)) Roll();
    }
    public int GetDiceNumber()
    {
        if (rolling == false)
        {
            gameObject.GetComponent<Image>().enabled = false;
            return diceNumber;
        }
        else return 0;
    }
}
