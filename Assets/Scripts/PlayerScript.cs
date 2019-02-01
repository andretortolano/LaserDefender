using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab = null;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    float xMin, xMax;
    float yMin, yMax;

    Coroutine continueFire;

    void Start()
    {
        SetupMoveBounderies();
    }

    void Update() {
        Move();
        FireLaser();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer) {
            health -= damageDealer.GetDamage();
            damageDealer.Hit();
            if (health <= 0)
            {
                DestroyEnemy();
            }
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    private void SetupMoveBounderies() {
        var gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move() {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPosition, newYPosition);
    }

    private void FireLaser() {
        if(Input.GetButtonDown("Fire1")) {
            continueFire = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1") && continueFire != null) {
            StopCoroutine(continueFire);
        }
    }

    IEnumerator FireContinuously() {
        while (true)
        {
            var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
}
