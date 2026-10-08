using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- 1. 宣告變數與陣列 ---
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    // 這裡使用了「陣列 (Array)」來儲存多個關卡檢查點或訊息提示
    public string[] checkpointNames = { "起點", "中間平台", "終點" };

    // 這裡使用了「陣列」來儲存多個浮點數（例如不同的跳躍加成倍率）
    public float[] jumpMultipliers = { 1.0f, 1.2f, 1.5f };

    void Start()
    {
        // 取得人物身上的 Rigidbody 2D 元件
        rb = GetComponent<Rigidbody2D>();
        
        // 測試讀取陣列資料（可在 Unity 的 Console 視窗看到輸出）
        Debug.Log("第一個檢查點名稱是: " + checkpointNames[0]);
    }

    void Update()
    {
        // --- 2. 使用 Vector2 處理移動 ---
        // 取得鍵盤左右方向鍵的輸入 (-1 到 1 之間)
        float moveX = Input.GetAxis("Horizontal");

        // 宣告一個 Vector2 來儲存角色的移動速度向量
        Vector2 movementVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        
        // 將向量指定給 Rigidbody2D 的速度
        rb.linearVelocity = movementVelocity;

        // --- 3. 上跳操作 ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // 這裡也使用了 Vector2 來處理跳躍的向上推力
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    // 簡單的碰撞偵測，當角色碰到地面時重設跳躍狀態
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}