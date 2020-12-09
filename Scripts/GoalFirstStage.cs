using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalFirstStage : MonoBehaviour
{
    private int sceneNum;

    [SerializeField]
    private GameObject playerCam = default;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCam();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            this.enabled = true;
            playerCam.transform.parent = null;
            StartCoroutine("SceneChange");
        }
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
    /// <summary>
    /// カメラを動かす
    /// </summary>
    private void MoveCam()
    {
        playerCam.transform.Rotate(-transform.right * 15 * Time.deltaTime);
    }
}
