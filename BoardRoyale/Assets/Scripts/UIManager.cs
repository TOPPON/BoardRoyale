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
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SeeCard()
    {

    }
    void DiceRoll()
    {
        [SerializeField] Button DiceButton;
    }
}
