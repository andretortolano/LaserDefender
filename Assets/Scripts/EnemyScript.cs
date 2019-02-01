using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter = 0;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3;
    [SerializeField] GameObject projectile = null;
    [SerializeField] float projectileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        ResetShotCounter();
    }

    private void ResetShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0) {
            Fire();
        }
    }

    private void Fire()
    {
        var laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        ResetShotCounter();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) return;

        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }
}
