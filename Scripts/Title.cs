using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField]
    private GameObject credit = default;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        credit.SetActive(false);
        // スコアの初期化
        StageManager.CurrentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowCredit()
    {
        credit.SetActive(true);
    }

    public void HideCredit()
    {
        credit.SetActive(false);
    }
}