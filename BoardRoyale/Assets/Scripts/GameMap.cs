using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    public static GameMap Instance;
    public List<int> loop1;
    public List<int> loop2;
    public List<int> perloop1;
    public List<int> perloop2;
    public enum SquareSpecies
    {
        Normal,
        Cross,
        Card,
        Start,
        Boss
    }
    public class MapSquare
    {
        public int id;
        public SquareSpecies species;
        public MapCross cross;
    }
    public List<MapSquare> MapSquares = new List<MapSquare>();
    public class MapCross
    {
        public int cross1LoopId;
        public int cross1squareId;
        public int cross2LoopId;
        public int cross2squareId;
    }
    public List<MapCross> MapCrosses = new List<MapCross>();
    //squareIdはループや分岐があっても別マスとして扱うユニークなID
    //ループは別マスが同じだということを確認することで入れ替われる
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < 10; i++)
        {
            MapCross tempCross=new MapCross();
            switch(i)
            {
                case 0:
                    tempCross.cross1LoopId = 1;
                    tempCross.cross1squareId = 0;
                    tempCross.cross2LoopId = 2;
                    tempCross.cross2squareId = 46;
                    break;
                case 1:
                    tempCross.cross1LoopId = 1;
                    tempCross.cross1squareId = 2;
                    tempCross.cross2LoopId = 1;
                    tempCross.cross2squareId = 18;
                    break;
                case 2:
                    tempCross.cross1LoopId = 1;
                    tempCross.cross1squareId = 13;
                    tempCross.cross2LoopId = 2;
                    tempCross.cross2squareId = 65;
                    break;
                case 3:
                    tempCross.cross1LoopId = 1;
                    tempCross.cross1squareId = 16;
                    tempCross.cross2LoopId = 2;
                    tempCross.cross2squareId = 56;
                    break;
                case 4:
                    tempCross.cross1LoopId = 1;
                    tempCross.cross1squareId = 24;
                    tempCross.cross2LoopId = 2;
                    tempCross.cross2squareId = 78;
                    break;
                case 5:
                    tempCross.cross1LoopId = 1;
                    tempCross.cross1squareId = 25;
                    tempCross.cross2LoopId = 1;
                    tempCross.cross2squareId = 42;
                    break;
                case 6:
                    tempCross.cross1LoopId = 1;
                    tempCross.cross1squareId = 30;
                    tempCross.cross2LoopId = 2;
                    tempCross.cross2squareId = 69;
                    break;
                case 7:
                    tempCross.cross1LoopId = 1;
                    tempCross.cross1squareId = 40;
                    tempCross.cross2LoopId = 2;
                    tempCross.cross2squareId = 76;
                    break;
                case 8:
                    tempCross.cross1LoopId = 2;
                    tempCross.cross1squareId = 48;
                    tempCross.cross2LoopId = 2;
                    tempCross.cross2squareId = 54;
                    break;
                case 9:
                    tempCross.cross1LoopId = 2;
                    tempCross.cross1squareId = 58;
                    tempCross.cross2LoopId = 2;
                    tempCross.cross2squareId = 64;
                    break;
            }
            MapCrosses.Add(tempCross);
        }
        for (int i = 0; i < 82; i++)
        {
            if (i < 46)
            {
                perloop1.Add(i);
                loop1.Add(i);
            }
            else
            {
                perloop2.Add(i);
                loop2.Add(i);
            }
            MapSquare tempSquare = new MapSquare();
            tempSquare.id = i;
            if (i == 1 || i == 7 || i == 10 || i == 14 || i == 17 || i == 21
                || i == 27 || i == 33 || i == 35 || i == 41 || i == 44 || i == 47
                || i == 51 || i == 55 || i == 57 || i == 61 || i == 73 || i == 77
                || i == 80)
            {
                tempSquare.species = SquareSpecies.Card;
            }
            else if (i == 67)
            {
                tempSquare.species = SquareSpecies.Boss;
            }
            else if (i == 0 || i == 46)
            {
                tempSquare.species = SquareSpecies.Start;
                tempSquare.cross = MapCrosses[0];
            }
            else if (i == 2 || i == 13 || i == 16 || i == 18 || i == 24 || i == 25
                || i == 30 || i == 40 || i == 42 || i == 48 || i == 54 || i == 56
                || i == 58 || i == 64 || i == 65 || i == 69 || i == 76 || i == 78)
            {
                tempSquare.species = SquareSpecies.Cross;
                switch (i)
                {
                    case 2:
                        tempSquare.cross = MapCrosses[1];
                        break;
                    case 13:
                        tempSquare.cross = MapCrosses[2];
                        break;
                    case 16:
                        tempSquare.cross = MapCrosses[3];
                        break;
                    case 18:
                        tempSquare.cross = MapCrosses[1];
                        break;
                    case 24:
                        tempSquare.cross = MapCrosses[4];
                        break;
                    case 25:
                        tempSquare.cross = MapCrosses[5];
                        break;
                    case 30:
                        tempSquare.cross = MapCrosses[6];
                        break;
                    case 40:
                        tempSquare.cross = MapCrosses[7];
                        break;
                    case 42:
                        tempSquare.cross = MapCrosses[5];
                        break;
                    case 48:
                        tempSquare.cross = MapCrosses[8];
                        break;
                    case 54:
                        tempSquare.cross = MapCrosses[8];
                        break;
                    case 56:
                        tempSquare.cross = MapCrosses[3];
                        break;
                    case 58:
                        tempSquare.cross = MapCrosses[9];
                        break;
                    case 64:
                        tempSquare.cross = MapCrosses[9];
                        break;
                    case 65:
                        tempSquare.cross = MapCrosses[2];
                        break;
                    case 69:
                        tempSquare.cross = MapCrosses[6];
                        break;
                    case 76:
                        tempSquare.cross = MapCrosses[7];
                        break;
                    case 78:
                        tempSquare.cross = MapCrosses[4];
                        break;
                }
            }
            else
            {
                tempSquare.species = SquareSpecies.Normal;
            }
            MapSquares.Add(tempSquare);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoseSquare(int loopId, int squareId)
    {
        if (GetSquareById(squareId).species == SquareSpecies.Cross || GetSquareById(squareId).species == SquareSpecies.Start|| GetSquareById(squareId).species == SquareSpecies.Boss) return;
        //誰かほかの人がいた時の処理
        switch (loopId)
        {
            case 1:
                loop1.Remove(squareId);
                break;
            case 2:
                loop2.Remove(squareId);
                break;
        }
        MapView.Instance.LoseSquare(squareId);
        //print("GameMap.LoseSquare" +loopId+":"+ squareId);
    }
    public MapSquare GetSquareById(int squareId)
    {
        return MapSquares[squareId];
    }
    public int GetnextSquare(int loopId, int squareId, int moveDirection)
    {
        switch (loopId)
        {
            case 1:
                switch (moveDirection)
                {
                    case 1:
                        if (loop1.Count <= loop1.BinarySearch(squareId) + 1) return loop1[0];
                        else return loop1[loop1.BinarySearch(squareId) + 1];
                    case -1:
                        if (0 > loop1.BinarySearch(squareId) - 1) return loop1[loop1.Count - 1];
                        else return loop1[loop1.BinarySearch(squareId) - 1];
                }
                break;
            case 2:
                switch (moveDirection)
                {
                    case 1:
                        if (loop2.Count <= loop2.BinarySearch(squareId) + 1) return loop2[0];
                        else return loop2[loop2.BinarySearch(squareId) + 1];
                    case -1:
                        if (0 > loop2.BinarySearch(squareId) - 1) return loop2[loop2.Count - 1];
                        else return loop2[loop2.BinarySearch(squareId) - 1];
                }
                break;
        }
        print("GetnextSquare ERROR!");
        
        return 0;
    }
    public int GetnextSquareWithLose(int loopId, int squareId, int moveDirection)
    {
        switch (loopId)
        {
            case 1:
                switch (moveDirection)
                {
                    case 1:
                        if (perloop1.Count <= perloop1.BinarySearch(squareId) + 1) return perloop1[0];
                        else return perloop1[perloop1.BinarySearch(squareId) + 1];
                    case -1:
                        if (0 >= perloop1.BinarySearch(squareId) - 1) return perloop1[perloop1.Count - 1];
                        else return perloop1[perloop1.BinarySearch(squareId) - 1];
                }
                break;
            case 2:
                switch (moveDirection)
                {
                    case 1:
                        if (perloop2.Count <= perloop2.BinarySearch(squareId) + 1) return perloop2[0];
                        else return perloop2[perloop2.BinarySearch(squareId) + 1];
                    case -1:
                        if (0 >= perloop2.BinarySearch(squareId) - 1) return perloop2[perloop2.Count - 1];
                        else return perloop2[perloop2.BinarySearch(squareId) - 1];
                }
                break;
        }
        print("GetnextSquareWithLose ERROR!");
        return 0;
    }
    public bool IsSameSquare(int squareId1, int squareId2)
    {
        if (squareId1 == squareId2) return true;
        if (GetSquareById(squareId1).species != SquareSpecies.Cross &&
            GetSquareById(squareId1).species != SquareSpecies.Start) return false;
        if (GetSquareById(squareId2).species != SquareSpecies.Cross &&
            GetSquareById(squareId2).species != SquareSpecies.Start) return false;
        if (GetSquareById(squareId2).cross.cross1squareId == squareId1 || GetSquareById(squareId2).cross.cross2squareId == squareId1) return true;
        else
        {
            return false;
        }
    }
}
