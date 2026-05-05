using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ParticleSystem _hitParticles;
    [SerializeField] float _travelSpeed;
    [SerializeField] float _damage;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] AudioClip _enemyHitSound;

        void OnTriggerEnter2D(Collider2D collision)
{
    Debug.Log("Projectile hit: " + collision.gameObject.name + " Tag: " + collision.tag);

    EntityHealth entityHealth = collision.GetComponentInParent<EntityHealth>();

    if (entityHealth != null)
    {
        DealDamage(collision.gameObject);
        DestroyProjectile();
        return;
    }

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

    void DealDamage(GameObject target)
    {
    EntityHealth entityHealth = target.GetComponentInParent<EntityHealth>();

    if (entityHealth == null)
    {
        Debug.LogWarning("No EntityHealth found on " + target.name);
        return;
    }

    Debug.Log("Damaging enemy for: " + _damage);
    entityHealth.LoseHealth(_damage);

    if (_enemyHitSound != null)
    {
        AudioManager.Instance.PlayAudio(_enemyHitSound, AudioManager.SoundType.SFX, 1.0f, false);
    }
}
}
