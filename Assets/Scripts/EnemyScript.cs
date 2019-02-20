using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter = 0;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3;
    [SerializeField] GameObject projectile = null;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject deathVFX = null;
    [SerializeField] float deathVFXDuration = 1f;
    [SerializeField] AudioClip deathSFX = null;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.5f;
    [SerializeField] AudioClip shootSFX = null;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.2f;

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
        if (shootSFX != null)
        {
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
        }
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
        if (deathVFX != null) {
            GameObject explostion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explostion, deathVFXDuration);
        }

        if (deathSFX != null) {
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        }
    }
}
