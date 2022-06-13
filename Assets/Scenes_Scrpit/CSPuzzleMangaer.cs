using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CSPuzzleMangaer : MonoBehaviour
{

    public int ArrayX = 10;
    public int ArrayY = 10;  //y축  x축
    public GameObject Block;

    bool[] Scorecheck;
    public bool[,] Blockboolcheck;
    bool boolDownmap = true;
    GameObject[,] BlockArray;
    public Material MaBlc;
    public Material MaDefualtBlc;
    public Material RedMat;
    List<bool> Blist;

    int Shapenum = 0;
    int twobytwo = 0;
    int threebythree = 0; 
    int resttime = 0;

    public bool[] LoadBoolcheck;

    public GameObject CubePrefab;

    GameObject CreatPrefab;
    int downRowSum = 0;
    bool NotCreate = true;

    void Awake()
    {
        Blockboolcheck = new bool[ArrayX, ArrayY];
        BlockArray = new GameObject[ArrayX, ArrayY];
        Scorecheck = new bool[ArrayX]; // 마지막줄 점수로넘기기  
        Blist = new List<bool>();

        for (int i = 0; i < ArrayX; i++)
        {
            for (int j = 0; j < ArrayY; j++)
            {
                Blockboolcheck[i, j] = true;
            }
        }
        FirstDrawMap();
        //Newrow(true);
        downmap();
    }
    void FirstDrawMap()
    {
        for (int i = 0; i < ArrayX; i++)
        {
            for (int j = 0; j < ArrayY; j++)
            {
                GameObject Blc = Instantiate<GameObject>(Block, new Vector3(j , 0, i ), Quaternion.identity);
                BlockArray[i, j] = Blc;
                Blc.tag = "Map";
                Blc.layer = 10;
            }
        }
    }

    void downmap()
    {
        //downRowSum++;
        //for (int j = 0; j < ArrayY; j++)
        //{
        //    for (int i = 0; i < ArrayX; i++)
        //    {
        //        if (j == 0)
        //        {
        //            Scorecheck[i] = Blockboolcheck[i, j];
        //        }
        //        else
        //        {
        //            Blockboolcheck[i, j - 1] = Blockboolcheck[i, j];

        //        }
        //    }
        //}
        //for (int i = 0; i < ArrayX; i++)
        //{
        //    Blockboolcheck[i, ArrayY - 1] = Blist[i];
        //}

        //switch (Shapenum)
        //{
        //    case 1:
        //        twobytwo++;
        //        break;
        //    case 2:
        //        threebythree++;
        //        break;
        //    case 3:
        //        resttime++;
        //        break;
        //    default:
        //        break;
        //}
        //for (int i = 0; i < ArrayX; i++)
        //{
        //    if (Scorecheck[i])
        //    {
        //        CSScoreText.Score -= 100;
        //    }
        //    else
        //    {
        //        CSScoreText.Score += 50;
        //    }
        //}
        //if (twobytwo == 2 || threebythree == 3 || resttime == 2)
        //{
        //    bool restbool = false;
        //    if (twobytwo == 2 || threebythree == 3)
        //    {
        //        restbool = true;
        //    }
        //    Blist.Clear();
        //    Newrow(restbool);
        //    twobytwo = 0;
        //    threebythree = 0;
        //    resttime = 0;
        //}
        

        if (boolDownmap)
        {
            Invoke("downmap", 3);
            Redarw();
        }
    }

    void Redarw()
    {
        for (int i = 0; i < ArrayX; i++)
        {
            for (int j = 0; j < ArrayY; j++)
            {
                if (Blockboolcheck[i, j])
                {
                    BlockArray[i, j].GetComponent<MeshRenderer>().material = MaBlc;
                  //  BlockArray[i, j].layer = 10;
                }
                else
                {
                    BlockArray[i, j].GetComponent<MeshRenderer>().material = MaDefualtBlc;
                  //  BlockArray[i, j].layer = 10;
                }
            }
        }

    }

    //void Newrow(bool rest) //새로운 줄 추가  
    //{
    //    Shapenum = Random.Range(1, 3);
    //    if (rest)
    //    {
    //        Shapenum = 3;
    //    }

    //    int Positrand;
    //    switch (Shapenum)
    //    {
    //        case 1: //2x2 네모
    //                //Positrand 의 따라서 x축에서 어디에 위치할지 선정
    //            Positrand = Random.Range(0, ArrayX - 1);  //0~2
    //            for (int k = 0; k < ArrayX; k++)
    //            {
    //                if (k == Positrand || k == Positrand + 1) // 1,2,3
    //                {
    //                    Blist.Add(false);
    //                }
    //                else // 
    //                {
    //                    Blist.Add(true);
    //                }
    //            }

    //            break;
    //        case 2: // 3x3 네모
    //            Positrand = Random.Range(0, ArrayX - 2); //01
    //            for (int k = 0; k < ArrayX; k++)
    //            {
    //                if (k == Positrand || k == Positrand + 1 || k == Positrand + 2) // 1,2,3
    //                {
    //                    Blist.Add(false);
    //                }
    //                else
    //                {
    //                    Blist.Add(true);
    //                }
    //            }
    //            break;
    //        case 3: // 쉬는타임
    //            for (int k = 0; k < ArrayX; k++)//0  1 2 3
    //            {
    //                Blist.Add(true);
    //            }
    //            break;
    //        default:
    //            break;
    //    }   
    //}
    //두개의 행렬을 더해서 

    public void Fill(GameObject Fill, GameObject target)
    {
        Vector3 targetPos = target.transform.position;
        int Index_X = (int)targetPos.z;
        int Index_Y = (int)targetPos.x;
        Transform[] chilTransform = Fill.transform.GetComponentsInChildren<Transform>();
        Blockboolcheck[Index_X, Index_Y] = false;
        List<int[]> Remat = new List<int[]>();
        for (int i = 1; i < chilTransform.Length; i++)
        {
            Vector3 LocalPos = chilTransform[i].localPosition;
            int child_IndeX = (int)LocalPos.z;
            int child_IndeZ = (int)LocalPos.x;

            child_IndeX = Index_X + child_IndeX;
            child_IndeZ = Index_Y + child_IndeZ;

            if (child_IndeX < 0){}
            else if (child_IndeX > ArrayX - 1){}
            else if (child_IndeZ < 0){}
            else if (child_IndeZ > ArrayY - 1){}
            else
            {
                if (Blockboolcheck[child_IndeX, child_IndeZ])
                {
                    Blockboolcheck[child_IndeX, child_IndeZ] = false;
                   
                }
                else
                {
                    //CSScoreText.Score -= 50;
                    int[] RedmatArray = { child_IndeX, child_IndeZ };
                    Remat.Add(RedmatArray);
                }
            }
            
        }
        Redarw();
        for (int i = 0; i < Remat.Count; i++)
        {
            int x = Remat[i][0];
            int y = Remat[i][1];
             BlockArray[x, y].GetComponent<MeshRenderer>().material=RedMat;
        }
    }
   
    
    //void RandPrefab()
    //{
    //    int Num=Random.Range(1, 6);
    //    switch (Num)
    //    {
    //        case 1:
    //            CreatPrefab = CubePrefab;
    //            break;
    //        case 2:
    //            CreatPrefab = CubePrefab2;
    //            break;
    //        case 3:
    //            CreatPrefab = CubePrefab3;
    //            break;
    //        case 4:
    //            CreatPrefab = CubePrefab4;
    //            break;
    //        case 5:
    //            CreatPrefab = CubePrefab5;
    //            break;
    //    }
    //}
    
    public void StopRaw()
    {
        boolDownmap = false;
    }
    
    public void LoadMap()
    {
        int num = 0;
        for (int i = 0; i < ArrayX; i++)
        {
            for (int j = 0; j < ArrayY; j++)
            {
                Blockboolcheck[i, j] = LoadBoolcheck[num];
                num++;
                   
            }
        }
        Redarw();
    }
    public int CheckdMap()
    {
        int num = 0;
        int equal=0;
        for (int i = 0; i < ArrayX; i++)
        {
            for (int j = 0; j < ArrayY; j++)
            {

                if(Blockboolcheck[i, j] == LoadBoolcheck[num])
                    equal++;
                num++;

            }
        }
        return equal;
    }

    private void Update()
    {
        if (downRowSum == 10)
        {
            Instantiate<GameObject>(CreatPrefab, new Vector3(3, 1, 0), Quaternion.identity);
            downRowSum = 0;
            
        }
    }
}

