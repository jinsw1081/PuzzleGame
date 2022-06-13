using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSInputBlock : MonoBehaviour {
    CSPuzzleMangaer Cspuzzle;
    Vector3 m_curPos;
    Vector3 m_prevPos;
    GameObject FillGameobject;
    bool CutMode;
    bool CutButtonOn = false;
    public Material cutMat;
    public Material CenterMat;

    List<Vector2> MakeBlockpoint = new List<Vector2>();

    public GameObject EmtyObject;
    public GameObject StandardCube;

    void Start()
    {
        GameObject MapCreator = GameObject.Find("MapCreator");
        Cspuzzle = MapCreator.GetComponent<CSPuzzleMangaer>();
    }

    public void rotateButtun()
    {
        if (FillGameobject)
        {
            CsBlock Blcok = FillGameobject.GetComponent<CsBlock>();
            Blcok.BlockRotateDouble();

        }
    }



    public void cutButton()
    {
      
        if (CutButtonOn)
        {
            CreateBlock();
            CutButtonOn = false;
        }
        else if (!CutButtonOn)
        {
            CutButtonOn = true;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int Mask = 2 << 9;

        if (Physics.Raycast(ray, out hit, Mask))
        {
            GameObject CurrentDrag = GameObject.Find("MapCreator");
            Cspuzzle.StopRaw();

            //자를려고 하면은 맵 멈추고 자르는 범위를 정해서 하든가 
            //맵을 멈춰서 멈춘다음에  
            //받아오는 것들에 대해서 위치를 마우스 위치를 매번 받은 다음 중복되는 것들을 삭제해서 받거나
            //중복으로 하는것이 문제가 있는가(?) 상관없을거같은데?    
            //버튼 누른뒤 -> 맵멈추고 ->맵을 클릭하면 위로 떠오르면서 커지는것처럼 느껴지게 한뒤에 원상태로
            //여러개 선정한뒤에 어떻게? 문제는 보면은 선택됏는데 키가 없어서버튼키로다시돌아갈떄 문제생김
        }
    }
    void CreateBlock()
    {
        //foeach문 시작하기전에 다넣고 넣을때 검사해버리면 
        if (MakeBlockpoint.Count == 0)
        {
            return;
        }
        Vector2 StandardPoint = new Vector2(MakeBlockpoint[0].x, MakeBlockpoint[0].y);
        GameObject ParentBlock = Instantiate<GameObject>(StandardCube, 
            new Vector3(MakeBlockpoint[0].x, 0, MakeBlockpoint[0].y),Quaternion.identity);
        ParentBlock.transform.position = new Vector3(14, 1, -3);
        ParentBlock.AddComponent<BoxCollider>();
        ParentBlock.AddComponent<CsBlock>();
        Renderer Rend=ParentBlock.GetComponent<Renderer>();
        Rend.material = CenterMat;
        ParentBlock.tag = "Block";

        for (int i=1;i<MakeBlockpoint.Count;i++)
        {
            
            Vector2 NextPoint = new Vector2(MakeBlockpoint[i].x, MakeBlockpoint[i].y);

            NextPoint = NextPoint - StandardPoint;
            GameObject ChildBlock = Instantiate<GameObject>(StandardCube);
            ChildBlock.transform.parent = ParentBlock.transform;
            ChildBlock.transform.localPosition = new Vector3(NextPoint.x, 0, NextPoint.y);
        }
        //   ParentBlock.transform.position = new Vector3(14, 1, -3);
        MakeBlockpoint.Clear();
    }

    void Update()
    {
        if (!CutButtonOn)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                int Mask = 2 << 9;

                if (Physics.Raycast(ray, out hit, Mask))
                {
                    GameObject CurrentDrag = hit.transform.root.gameObject;
                    if (CurrentDrag.transform.tag == "Block")
                    {
                        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                        FillGameobject = CurrentDrag;
                        Vector3 Pos = Camera.main.ScreenToWorldPoint(mousePosition);
                        Pos.y = 1;
                        CurrentDrag.transform.position = Pos;
                        
                    }
                }
            }
          
        }

        if (!CutButtonOn)
        {
            if (Input.GetMouseButtonDown(0) && FillGameobject)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1 << 10))
                {
                    
                    if (FillGameobject && hit.transform.tag == "Map")
                    {
                        CsBlock Blcok = FillGameobject.GetComponent<CsBlock>();
                        Blcok.BlockHp--;
                        Cspuzzle.Fill(FillGameobject, hit.transform.gameObject);
                        FillGameobject = null;
                    }
                }
            }
        }

        if(CutButtonOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1 << 10))
                {
                    if(hit.transform.tag=="Map")
                    {
                        Renderer rend = hit.transform.gameObject.GetComponent<Renderer>();
                        rend.material = cutMat;
                        Vector3 hitpos = hit.transform.position;
                        Vector2 Index;
                        Index.x = (int)hitpos.z;
                        Index.y = (int)hitpos.x;
                        bool CheckInsert = false;
                        
                        if(MakeBlockpoint.Count==0)
                        {
                            MakeBlockpoint.Add(Index);
                        }
                        foreach(Vector2 element in MakeBlockpoint)
                        {
                            if (element.x==Index[0]&& element.y ==Index[1])
                            {
                                CheckInsert = false;
                            }
                            else
                            {
                                CheckInsert = true;
                            }
                        }
                        if(CheckInsert)
                        {
                            MakeBlockpoint.Add(Index);
                        }
                    }
                }
            }
        }
     
        
    }
}

