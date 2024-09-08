using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode runHotkey = KeyCode.LeftShift;
    public float runningAnimMultiplier = 1.5f;
    public float collisionOffset = 0.3f;
    public Player player;
    public GameObject gameManager;

    private float movement_x;
    private float movement_y;
    private Rigidbody2D p_rigidbody;
    private PersistentData persistentData;
    private Animator animator;
    private UIManager uiManager;
    private bool paused;

    private void Start()
    {
        p_rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        uiManager = gameManager.GetComponent<UIManager>();

        persistentData = PersistentData.Instance;
        if (persistentData.GetPlayer() == null)
        {
            SwitchCharacter(Characters.Jin);
            persistentData.SetPlayer(player);
        }
        else
        {
            player = persistentData.GetPlayer();
        }
        transform.position = new Vector2(player.posX, player.posY);

        animator.runtimeAnimatorController = player.character.animator;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!paused)
        {
            animator.SetFloat("horizontalSpeed", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("verticalSpeed", Input.GetAxisRaw("Vertical"));

            if (Input.GetKey(runHotkey))
            {
                movement_x = Input.GetAxisRaw("Horizontal") * player.runSpeed * 10;
                movement_y = Input.GetAxisRaw("Vertical") * player.runSpeed * 10;

                animator.SetFloat("runningMultiplier", runningAnimMultiplier);
            }
            else
            {
                movement_x = Input.GetAxisRaw("Horizontal") * player.moveSpeed * 10;
                movement_y = Input.GetAxisRaw("Vertical") * player.moveSpeed * 10;

                animator.SetFloat("runningMultiplier", 1f);
            }

            // Take the hypotenuse of the speed vector if moving diagonally
            if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0)
            {
                movement_x /= math.sqrt(2);
                movement_y /= math.sqrt(2);
            }
        }
        else
        {
            movement_x = 0;
            movement_y = 0;
        }
    }

    private void FixedUpdate()
    {
        p_rigidbody.velocity = new Vector2(movement_x * Time.fixedDeltaTime, movement_y * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;
        if (collisionObj.name == "Coin")
        {
            persistentData.GetPlayer().coins++;
            Destroy(collisionObj);
        }
        else if (collisionObj.CompareTag("Enemy"))
        {
            StoreOffsetPos(collisionObj.transform.position);
            TogglePaused();
            GetComponent<LoadNextScene>().LoadScene();
        }
        else if (collisionObj.CompareTag("Campsite"))
        {
            uiManager.ToggleCampfireMenu();
            TogglePaused();
            StoreOffsetPos(collisionObj.transform.position);
            StoreSavePointData(collisionObj.transform.position);
        }
    }

    public void TogglePaused()
    {
        paused = !paused;
    }

    public void StorePlayerData()
    {
        persistentData.SetPlayer(player);
        StorePlayerPos(transform.position.x, transform.position.y);
    }

    private void StorePlayerPos(float posX, float posY)
    {
        persistentData.GetPlayer().posX = posX;
        persistentData.GetPlayer().posY = posY;
    }

    private void StoreSavePointData(Vector2 collisionPos)
    {
        persistentData.GetPlayer().lastSaveX = CalculateOffset(transform.position.x, collisionPos.x);
        persistentData.GetPlayer().lastSaveY = CalculateOffset(transform.position.y, collisionPos.y);
    }

    private void StoreOffsetPos(Vector2 collisionPos)
    {
        StorePlayerPos(CalculateOffset(transform.position.x, collisionPos.x), CalculateOffset(transform.position.y, collisionPos.y));
    }

    private float CalculateOffset(float playerPos, float collisionPos)
    {
        return playerPos < collisionPos ? playerPos - collisionOffset : playerPos + collisionOffset;
    }

    public void SwitchCharacter(Characters character)
    {
        player.character = player.characterList[(int)character];
    }
}
