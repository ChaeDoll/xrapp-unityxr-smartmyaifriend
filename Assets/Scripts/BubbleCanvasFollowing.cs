using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCanvasFollowing : MonoBehaviour
{
    public GameObject bubbleCanvas;
    RectTransform bubbleCanvasRectTransform;

    void Awake()
    {
        bubbleCanvas = GameObject.FindGameObjectWithTag("BubbleCanvas");
        print(bubbleCanvas);
        if (bubbleCanvas != null)
        {
            bubbleCanvasRectTransform = bubbleCanvas.GetComponent<RectTransform>();
        }
        else
        {
            print("bubbleCanvas is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bubbleCanvasRectTransform != null)
        {
            print("bef"+bubbleCanvasRectTransform.position);
            bubbleCanvasRectTransform.position = transform.position;
            print("af" + bubbleCanvasRectTransform.position);
            bubbleCanvasRectTransform.rotation = transform.rotation;
        }
    }
}
