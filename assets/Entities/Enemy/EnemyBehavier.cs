using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavier : MonoBehaviour {

    public GameObject asdpoitd;
    public float projectileSpeed = 10;
    public float health = 150;
    public float shotsPerSeconds = 0.5f;
    public int scoreValue = 150;

    public AudioClip fireSound;
    public AudioClip deathSound;

    private ScoreKepper scoreKepper;

    private void Start()
    {
        scoreKepper = GameObject.Find("Score").GetComponent<ScoreKepper>();
    }

    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSeconds;
        if (Random.value < probability)
        {
            if (asdpoitd)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject missile = Instantiate(asdpoitd, startPosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }

        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
        scoreKepper.Score(scoreValue);
    }
}
