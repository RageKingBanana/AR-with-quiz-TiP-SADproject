using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_Testing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform[] points;
    [SerializeField] private lr_LineController line;
    private void Start()
    {
        line.SetUpLine(points);
    }

    // Update is called once per frame

}
