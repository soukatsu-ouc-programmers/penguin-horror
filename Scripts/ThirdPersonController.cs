using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ThirdPersonController : MonoBehaviour
{
    /// <summary>
    /// ペンギンのコントローラー
    /// </summary>
    private CharacterController penguinController;
    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    private float moveSpeed = 1f;
    /// <summary>
    /// ジャンプ力
    /// </summary>
    //[SerializeField]
    //private float jumpPower = 5;
    /// <summary>
    /// 重力
    /// </summary>
    //[SerializeField]
    //private float gravity = 10;
    /// <summary>
    /// 動く向き
    /// </summary>
    private Vector3 moveDirection = Vector3.zero;

    [SerializeField]
    private GameObject cameraObj = default;

    /// <summary>
    /// 垂直方向の入力（移動）
    /// </summary>
    private float inputMoveVertical;
    /// <summary>
    /// 水平方向の入力（移動）
    /// </summary>
    private float inputMoveHorizontal;

    /// <summary>
    /// スコア
    /// </summary>
    [SerializeField]
    private int currentScore = 0;

    /// <summary>
    /// アニメーションコントローラー
    /// </summary>
    private Animator animator;
    /// <summary>
    /// 待機モーション用乱数
    /// </summary>
    private int randnum;
    /// <summary>
    /// 待機モーションの数
    /// </summary>
    private int idleMotionNum = 2;

    /// <summary>
    /// ステージマネージャー
    /// </summary>
    private StageManager manager;

    // Start is called before the first frame update
    void Start()
    {
        penguinController = this.GetComponent<CharacterController>();
        //カーソルのロック
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator = this.GetComponent<Animator>();

        manager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();

    }

    void FixedUpdate()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(cameraObj.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputMoveVertical + cameraObj.transform.right * inputMoveHorizontal;

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            this.transform.rotation = Quaternion.LookRotation(cameraForward);
        }

        // キーボードの入力を取って動かす
        inputMoveVertical = Input.GetAxis("Vertical");
        inputMoveHorizontal = Input.GetAxis("Horizontal");
        Move(inputMoveVertical, inputMoveHorizontal, moveSpeed);

        // アニメーション
        Animate(inputMoveVertical + inputMoveHorizontal);
    }

    private void Move(float vertical, float horizontal, float speed)
    {
        // 前後移動
        //this.transform.Translate(Vector3.forward * vertical * speed);
        //playerRigidbody.AddForce(this.transform.forward * vertical * speed, ForceMode.Impulse);
        // 左右移動
        //this.transform.Translate(Vector3.right * horizontal * speed);
        //playerRigidbody.AddForce(this.transform.right * horizontal * speed, ForceMode.Impulse);

        // 止める
        //playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);

        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        //moveDirection.y -= gravity * Time.deltaTime;
        // 移動
        //penguinController.Move(moveDirection * Time.deltaTime);
        penguinController.SimpleMove(moveDirection);
    }

    private void GetFish(int score)
    {
        currentScore += score;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fish")
        {
            manager.GetScore(100);
            Destroy(other.gameObject);
        }
    }

    private void Animate(float inputAxis)
    {
        animator.SetFloat("inputAxis", inputAxis);
        // ランダムなモーションに行ったり行かなかったりする
        randnum = Random.Range(0, idleMotionNum + 1);
        animator.SetInteger("random", randnum);
    }
}
