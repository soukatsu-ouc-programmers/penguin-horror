using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 奥スクロール時のプレイヤーコントローラー
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ScrollPlayerController : MonoBehaviour
{
    /// <summary>
    /// 左右のキー操作を反転させるかどうか
    /// </summary>
    [SerializeField]
    private bool isInverse = false;

    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    private float moveSpeed = 1;
    /// <summary>
    /// ジャンプ力
    /// </summary>
    [SerializeField]
    private float jumpPower = 1;
    /// <summary>
    /// ジャンプ中かどうか
    /// </summary>
    private bool isJump = false;

    /// <summary>
    /// 水平方向の入力
    /// </summary>
    private float inputHorizontal;
    /// <summary>
    /// 水平方向のスピード
    /// </summary>
    [SerializeField]
    private float horizontalSpeed = 1;

    /// <summary>
    /// 剛体
    /// </summary>
    private Rigidbody playerRigidbody;

    ///// <summary>
    ///// レーンを配列で
    ///// </summary>
    //[SerializeField]
    //private Transform[] lanes = default;

    ///// <summary>
    ///// レーン番号
    ///// </summary>
    //[SerializeField]
    //private int targetLaneNum = 1;

    /// <summary>
    /// ステージマネージャー
    /// </summary>
    private StageManager manager;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = this.GetComponent<Rigidbody>();

        manager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(moveSpeed);
        inputHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            Jump(jumpPower);
        }
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    if (isInverse)
        //    {
        //        ChangeLane("right");
        //    }
        //    else
        //    {
        //        ChangeLane("left");
        //    }
        //}else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    if (isInverse)
        //    {
        //        ChangeLane("left");
        //    }
        //    else
        //    {
        //        ChangeLane("right");
        //    }
        //}
        if (isInverse)
        {
            MoveHorizontal(-inputHorizontal);
        }
        else
        {
            MoveHorizontal(inputHorizontal);
        }
        if (!isJump)
        {
        // 重力をさらに加える
        //playerRigidbody.AddForce(Vector3.up * -3, ForceMode.Force);
        }   

    }

    private void Move(float speed)
    {
        this.transform.Translate(Vector3.forward * speed);
    }

    private void Jump(float power)
    {
        playerRigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);
        isJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }
    }

    //private void ChangeLane(string direction)
    //{
    //    if(direction == "right" && targetLaneNum < 2)
    //    {
    //        targetLaneNum++;
    //    }else if(direction == "left" && targetLaneNum > 0)
    //    {
    //        targetLaneNum--;
    //    }

    //    this.transform.position = new Vector3(lanes[targetLaneNum].transform.position.x, this.transform.position.y, this.transform.position.z);
    //}

    private void MoveHorizontal(float horizontal)
    {
        this.transform.Translate(Vector3.right * horizontal * horizontalSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fish")
        {
            // ステージマネージャーのスコア加算処理を呼び出す
            manager.GetScore(100);
            Destroy(other.gameObject);
        }

        if(other.tag == "Enemy")
        {
            // ステージマネージャーのゲームオーバー処理を呼び出す
            manager.GameOver();
        }
    }
}
