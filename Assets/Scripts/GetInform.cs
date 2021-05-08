using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        /*while (!pressedinc)
        {
            if (Input.touchCount > 0 || Input.GetMouseButton(0))
                pressedinc = true;
            yield return null;
        }*/
        GameObject.FindGameObjectWithTag("MainCamera").transform.Find("Mercury3D").gameObject.SetActive(true);
    }
}
