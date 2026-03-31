using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaffController : MonoBehaviour
{
    [SerializeField] float _fireRate;
    [SerializeField] Projectile _projectile;
    [SerializeField] AudioClip _shootSound;
    [SerializeField] Transform _tip;
    float _nextFireTime;
    Vector2 _lookDirection;

    void Update()
    {
        SetLookDirection();
        RotateStaff();
        if(Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + 1f / _fireRate;
            Shoot();
        }
    }

    void RotateStaff()
    {
        float angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void Shoot()
    {
        AudioManager.Instance.PlayAudio(_shootSound, AudioManager.SoundType.SFX, 0.4f, false);
        Projectile newProjectile = Instantiate(_projectile, _tip.position, Quaternion.identity);
        newProjectile.InitializeProjectile(_lookDirection);
    }
    void SetLookDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lookDirection = (mousePosition - (Vector2)transform.position).normalized;
    }
}
