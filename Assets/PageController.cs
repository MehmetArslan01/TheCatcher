using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    private List<CanvasGroup> pages = new List<CanvasGroup>();
    public int currentPageIndex = 0;


    void Start()
    {
        int pageCount = this.gameObject.transform.childCount;

        for (int i = 0; i < pageCount; i++)
        {
            this.pages.Add(this.gameObject.transform.GetChild(i).GetComponent<CanvasGroup>());
        }

        this.pages.Sort((obj1, obj2) => obj1.name.CompareTo(obj2.name));

        ResetCurrentPageIndex();
        SetPageVisibility();

    }

    public void GetNextPage()
    {

        if (this.currentPageIndex >= (this.pages.Count - 1))
        {
            return;
        }
        else
        {
            this.currentPageIndex++;
            SetPageVisibility();
        }
    }

    public void GetPreviousPage()
    {
        if (this.currentPageIndex == 0)
        {
            return;
        }
        else
        {
            this.currentPageIndex--;
            SetPageVisibility();
        }
    }

    private void SetPageVisibility()
    {
        for (int i = 0; i < this.pages.Count; i++)
        {
            if (i == this.currentPageIndex)
            {
                this.pages[i].alpha = 1.0f;
            }
            else
            {
                this.pages[i].alpha = 0.0f;
            }
        }
    }

    public void ResetCurrentPageIndex()
    {
        this.currentPageIndex = 0;
        SetPageVisibility();
    }
}
