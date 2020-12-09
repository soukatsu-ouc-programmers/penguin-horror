using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    /// <summary>
    /// シーン番号
    /// </summary>
    [SerializeField]
    private int sceneNumber = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Change(sceneNumber);
        }
    }
    /// <summary>
    /// シーン遷移するための関数
    /// </summary>
    /// <param name="sceneNum"></param>
    public void Change(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
