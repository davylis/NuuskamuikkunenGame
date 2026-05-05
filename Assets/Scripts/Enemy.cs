using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent _agent;
    GameObject _target;
    EntityHealth _entityHealth;
    [SerializeField] AudioClip _deathSound;

    void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();

        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _agent.updateRotation = false;
    }

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");

        _entityHealth.OnDeath += DestroyEnemy;
    }

    void Update()
    {
        _agent.SetDestination(_target.transform.position);
    }

    void OnDisable()
    {
        _entityHealth.OnDeath -= DestroyEnemy;
    }
    public void DestroyEnemy()
    {
        AudioManager.Instance.PlayAudio(_deathSound, AudioManager.SoundType.SFX, 1.0f, false);
        Destroy(gameObject);
    }
}
