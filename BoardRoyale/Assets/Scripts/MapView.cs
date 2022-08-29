using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    public static MapView Instance;
    [SerializeField] GameObject StartSquare;
    [SerializeField] GameObject CrossSquare;
    [SerializeField] GameObject CardSquare;
    [SerializeField] GameObject BossSquare;
    [SerializeField] GameObject NormalSquare;
    [SerializeField] Sprite LoseSquareSprite;
    List<GameObject> MapSquares=new List<GameObject>();
    List<Vector3> SquarePositions = new List<Vector3>() 
    { new Vector3(600, 121), new Vector3(567,104),new Vector3(534,92),new Vector3(466,68),new Vector3(394,43),
    new Vector3(350,30),new Vector3(293,24),new Vector3(224,46),new Vector3(198,82),new Vector3(199,134),
    new Vector3(219,184),new Vector3(248,228),new Vector3(299,257),new Vector3(355,266),new Vector3(430,239),
    new Vector3(456,210),new Vector3(474,175),new Vector3(495,132),new Vector3(534,92),new Vector3(603,67),
    new Vector3(669,67),new Vector3(738,84),new Vector3(779,112),new Vector3(795,170),new Vector3(769,246),
    new Vector3(737,282),new Vector3(673,321),new Vector3(610,357),new Vector3(568,386),new Vector3(528,414),
    new Vector3(478,437),new Vector3(420,486),new Vector3(442,537),new Vector3(495,548),new Vector3(557,555),
    new Vector3(621,558),new Vector3(680,554),new Vector3(735,513),new Vector3(762,461),new Vector3(768,417),
    new Vector3(761,366),new Vector3(752,322),new Vector3(737,282),new Vector3(707,236),new Vector3(678,191),
    new Vector3(645,157),new Vector3(600, 121),new Vector3(584,138),new Vector3(572,159),new Vector3(565,198),
    new Vector3(573,260),new Vector3(637,302),new Vector3(657,249),new Vector3(629,187),new Vector3(572,159),
    new Vector3(528,162),new Vector3(474,175),new Vector3(420,201),new Vector3(359,215),new Vector3(301,214),
    new Vector3(251,157),new Vector3(282,106),new Vector3(339,97),new Vector3(367,155),new Vector3(359,215),
    new Vector3(355,266),new Vector3(361,318),new Vector3(370,356),new Vector3(411,396),new Vector3(478,437),
    new Vector3(517,463),new Vector3(560,484),new Vector3(607,489),new Vector3(652,489),new Vector3(699,470),
    new Vector3(738,427),new Vector3(761,366),new Vector3(771,304),new Vector3(769,246),new Vector3(753,181),
    new Vector3(723,124),new Vector3(662,104)};
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
        Vector3 squareExtRate = new Vector3(0.015f,-0.015f);
        Vector3 squareOffset = new Vector3(-10,4.5f);
        for (int i = 0; i < 82; i++)
        {
            SquarePositions[i]=new Vector3(SquarePositions[i].x*squareExtRate.x, SquarePositions[i].y * squareExtRate.y);
            SquarePositions[i] += squareOffset;
            switch (i)
            {
                case 0:
                    GameObject obj = Instantiate(StartSquare,SquarePositions[i],Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 1:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 2:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 3:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 4:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 5:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 6:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 7:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 8:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 9:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 10:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 11:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 12:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 13:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 14:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 15:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 16:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 17:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 18:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 19:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 20:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 21:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 22:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 23:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 24:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 25:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 26:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 27:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 28:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 29:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 30:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 31:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 32:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 33:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 34:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 35:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 36:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 37:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 38:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 39:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 40:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 41:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 42:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 43:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 44:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 45:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 46:
                    obj = Instantiate(StartSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 47:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 48:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 49:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 50:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 51:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 52:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 53:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 54:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 55:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 56:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 57:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 58:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 59:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 60:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 61:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 62:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 63:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 64:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 65:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 66:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 67:
                    obj = Instantiate(BossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 68:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 69:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 70:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 71:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 72:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 73:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 74:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 75:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 76:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 77:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 78:
                    obj = Instantiate(CrossSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 79:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 80:
                    obj = Instantiate(CardSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
                case 81:
                    obj = Instantiate(NormalSquare, SquarePositions[i], Quaternion.identity);
                    MapSquares.Add(obj);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GetPlayerPositionById(int squareId)
    {
        return SquarePositions[squareId]+new Vector3(0,0.05f);
    }
    public void LoseSquare(int squareId)
    {
        
    }
}
