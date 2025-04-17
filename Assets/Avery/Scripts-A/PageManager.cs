using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PageManager : MonoBehaviour
{
    public List<GameObject> allItems;  // 所有按钮或内容项
    public int itemsPerPage = 4;       // 每页显示多少个
    public Button nextButton;
    public Button prevButton;

    private int currentPage = 0;
    private int totalPages;

    void Start()
    {
        totalPages = Mathf.CeilToInt((float)allItems.Count / itemsPerPage);
        ShowPage(currentPage);

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PreviousPage);
    }

    void ShowPage(int pageIndex)
    {
        for (int i = 0; i < allItems.Count; i++)
        {
            allItems[i].SetActive(false);
        }

        int start = pageIndex * itemsPerPage;
        int end = Mathf.Min(start + itemsPerPage, allItems.Count);

        for (int i = start; i < end; i++)
        {
            allItems[i].SetActive(true);
        }

        prevButton.interactable = pageIndex > 0;
        nextButton.interactable = pageIndex < totalPages - 1;
    }

    void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }

    void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }
}
