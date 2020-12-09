using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージ関連のことをなんやかんやするやつ
/// </summary>
public class StageManager : MonoBehaviour
{
    /// <summary>
    /// スコア
    /// </summary>
    public static int CurrentScore;
    /// <summary>
    /// スコアを表示するテキスト
    /// </summary>
    [SerializeField]
    private Text scoreText = default;
    /// <summary>
    /// ゲームオーバー時に出てくる奴
    /// </summary>
    [SerializeField]
    private GameObject gameOverText = default;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.SetActive(false);
        scoreText.text = CurrentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetScore(int score)
    {
        CurrentScore += score;
        scoreText.text = CurrentScore.ToString();
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(4);
    }
}
