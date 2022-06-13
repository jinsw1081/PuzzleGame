using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsBlock : MonoBehaviour {

    public int BlockHp = 3;
    public bool OnDoubleRotate = false;
    void Start() {

    }

    public void BlockRotateDouble()
    {
        Transform[] chilTransform = gameObject.transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < chilTransform.Length; i++)
        {
            Vector3 LocalPos = chilTransform[i].localPosition;
            Vector3 LocalPosChange;
            if (OnDoubleRotate)
            {
                LocalPosChange.x = LocalPos.z;
                LocalPosChange.z = LocalPos.x;
                LocalPosChange.y = 0;
                if (i > 1) LocalPosChange.x *= -1;
            }
            else
            { 
                LocalPosChange.x = -LocalPos.z;
                LocalPosChange.z = LocalPos.x;
                LocalPosChange.y = 0;
            }
                chilTransform[i].localPosition = LocalPosChange;
        }
    }

    void Update () {
        if (BlockHp == 0)
        {
            Destroy(gameObject);
        }
	}
}
