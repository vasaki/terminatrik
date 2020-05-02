using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 topLeftMostCorner;
    Vector2 bottomRightMostCorner;
    Vector3 bulletSpawnDelta;
    Equipment equipment;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //clamp parameters
        Vector2 boxSize = GetComponent<BoxCollider2D>().size * 0.5f;
        Vector2 boxCenterDelta = 
            GetComponent<BoxCollider2D>().bounds.center - transform.position;
        topLeftMostCorner = new Vector2(ScreenUtils.ScreenLeft + boxSize.x - boxCenterDelta.x,
            ScreenUtils.ScreenTop - boxSize.y - boxCenterDelta.y);
        bottomRightMostCorner = 
            new Vector2(ScreenUtils.ScreenRight - boxSize.x - boxCenterDelta.x,
            ScreenUtils.ScreenBottom + boxSize.y - boxCenterDelta.y);
        //bullet spawn offset
        Vector3 barrelPosition = transform.Find("Barrel").position;
        GameObject testBullet = Pools.Get(PoolsEnum.PlayerBullet);
        Vector2 bulletSize = testBullet.GetComponent<BoxCollider2D>().size;
        bulletSpawnDelta = new Vector3(barrelPosition.x + bulletSize.x * 0.5f,
            barrelPosition.y, barrelPosition.z) - transform.position;
        Pools.Return(PoolsEnum.PlayerBullet, testBullet);
    }
    public void Initialize(Equipment equipment)
    {
        this.equipment = equipment;
        equipment.Initialize(gameObject);
    }
    private void FixedUpdate()
    {
        //check movement input
        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");
        if (vertInput != 0 || horInput != 0)
            rb.MovePosition(ClampPosition(new Vector2(
                rb.position.x + horInput * GameConstants.RobotMovementUnitsPerSecond,
                rb.position.y + vertInput * GameConstants.RobotMovementUnitsPerSecond)));
    }
    private void Update()
    {
        //check firing input. Firing must be in Update and not FixedUpdate,
        //otherwise it will be firing many bullets at once
        //not firing if Paused, as Update method is called even if timeScale is zero
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale > 0)
            equipment.Shoot(EquipmentHand.Primary, transform.position + bulletSpawnDelta);
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Time.timeScale > 0)
            equipment.Shoot(EquipmentHand.Secondary, transform.position + bulletSpawnDelta);
    }
    private Vector2 ClampPosition(Vector2 position)
    {
        Vector2 clampedPosition = position;
        if (position.y > topLeftMostCorner.y) clampedPosition.y = topLeftMostCorner.y;
        else if (position.y < bottomRightMostCorner.y)
            clampedPosition.y = bottomRightMostCorner.y;
        if (position.x > bottomRightMostCorner.x) clampedPosition.x = bottomRightMostCorner.x;
        else if (position.x < topLeftMostCorner.x)
            clampedPosition.x = topLeftMostCorner.x;
        return clampedPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.IsEnemyObject(collision.gameObject))
        {
            Explosion.InstantiateExplosion(gameObject)
                .GetComponent<Explosion>().eventOnEndAnimation = EventNames.PlayerDies;
            gameObject.SetActive(false);
        }
    }
}
