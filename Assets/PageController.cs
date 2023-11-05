using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{

    public CanvasGroup page1;
    public CanvasGroup page2;

    public void getNextPage()
    {
        page1.alpha = 0.0f;
        page2.alpha = 1.0f;
    }

    public void getPreviousPage()
    {
        page1.alpha = 1.0f;
        page2.alpha = 0.0f;
    }

}
