using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    [SerializeField] bool isEnemy;

    CameraShake cameraShake;
    AudioPlayer player;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.getDamage());
            PlayeHitEffect();
            player.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        player = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void TakeDamage(int damage)
    {
        health -= damage;       
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isEnemy)
        {
            scoreKeeper.SetScore(score);
        }
        else
        {
            levelManager.LoadGameOverScene();
        }
        Destroy(gameObject);
    }

    void PlayeHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public int GetHealthPoints()
    {
        return health;
    }
}
