using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName =  "Power Up Config", fileName = "New Power Up Config")]
//Creates a set of variables that control how the power up works and methods to access those variables 

public class PowerUpConfigSO : ScriptableObject
{
    [Header("General")]
    [SerializeField] float powerUpLength;

    
    [Header("Damage Immunity")]
    [SerializeField] bool isImmunityPowerUp;

    [Header("Projectile Power Up")]
    [SerializeField] bool isProjectilePowerUp;
    [SerializeField] GameObject newProjectilePrefab;
    [SerializeField] float newProjectileSpeed;
    [SerializeField] float newFiringRate;

    public bool IsImmunityPowerUp()
    {
        return isImmunityPowerUp;
    }

    public GameObject GetProjectilePrefab()
    {
        return newProjectilePrefab;
    }

    public float GetProjectileSpeed()
    {
        return newProjectileSpeed;
    }

    public float GetFiringRate()
    {
        return newFiringRate;
    }

    public float GetPowerUpLength()
    {
        return powerUpLength;
    }

    public bool IsProjectilePowerUp()
    {
        return isProjectilePowerUp;
    }




}
