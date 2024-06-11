using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 보스 공격 spawner
// 최초 작성자: 홍원기
// 수정자: 
// 최종 수정일: 2024-06-11
public class BossAttackSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval;
    public float spawnForce;
    public bool moveLeft = true;
    public bool moveDown = false;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    private void SpawnObject()
    {
        GameObject newObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        Vector2 moveDirection = Vector2.zero;
        if (moveLeft)
        {
            moveDirection += Vector2.left * spawnForce;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }

        if (moveDown)
        {
            moveDirection += Vector2.down * spawnForce;
            newObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }

        rb.velocity = moveDirection;
    }
}