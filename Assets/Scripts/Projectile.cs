using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ParticleSystem _hitParticles;

        void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Terrain"))
        {
            DestroyProjectile();
        }
    }
        public void InitializeProjectile(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * 10f; // or your speed
    }
    void DestroyProjectile()
    {
        ParticleSystem hitParticles = Instantiate(_hitParticles, transform.position, Quaternion.identity);
        Destroy(hitParticles.gameObject, 1f);
        Destroy(gameObject);
    }
}
