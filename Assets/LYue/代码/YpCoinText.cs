using TMPro;
using UnityEngine;

public class YpCoinText : MonoBehaviour
{
    private Camera _mainCam;

    void Start()
    {
        _mainCam = Camera.main;
        GetComponent<TextMeshPro>().sortingOrder = 100; // 确保显示在最前
    }

    void Update()
    {
        transform.LookAt(_mainCam.transform);
        transform.rotation = Quaternion.LookRotation(transform.position - _mainCam.transform.position);
    }
}