using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class boolcheck
{
    public string name;
    [SerializeField]

    public bool[] Onecheck;

}

public class csSave : MonoBehaviour
{

    public boolcheck Savecheckbool;
    public boolcheck Loadcheckbool;
    string dataPath;
    CSPuzzleMangaer Cspuzzle;

    public bool[] starOnecheck;

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "CharacterData.txt");

        GameObject MapCreator = GameObject.Find("MapCreator");
        Cspuzzle = MapCreator.GetComponent<CSPuzzleMangaer>();


    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            int num = 0;
            int row = Cspuzzle.ArrayX;
            int col = Cspuzzle.ArrayY;

            bool[,] Copybool = Cspuzzle.Blockboolcheck;
            starOnecheck = new bool[row * col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    starOnecheck[num] = Copybool[i, j];
                    num++;
                }
            }

            Savecheckbool.Onecheck = starOnecheck;
            Savecheckbool.name = "ss";
            SaveCharacter(Savecheckbool, dataPath);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Loadcheckbool = LoadCharacter(dataPath);
            Cspuzzle.LoadBoolcheck = Loadcheckbool.Onecheck;
            Cspuzzle.LoadMap();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {

            Loadcheckbool = LoadCharacter(dataPath);
            Cspuzzle.LoadBoolcheck = Loadcheckbool.Onecheck;
            
            int num =Cspuzzle.CheckdMap();
            CSScoreText.Score = num;
        }
    }

    static void SaveCharacter(boolcheck data, string path)
    {
         string jsonString = JsonUtility.ToJson(data);
         Debug.Log(jsonString);
         using (StreamWriter streamWriter = File.CreateText(path))
         {
            streamWriter.Write(jsonString);
         }
         
    }

    static boolcheck LoadCharacter(string path)
    {
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<boolcheck>(jsonString);
        }
    }
}