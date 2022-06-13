using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScence : MonoBehaviour {

	// Use this for initialization
	void Start () {
      
	}
	
    void ScenceLoad()
    {
        SceneManager.LoadScene("GameScence");

    }

	// Update is called once per frame
	void Update () {
		
	}
}
