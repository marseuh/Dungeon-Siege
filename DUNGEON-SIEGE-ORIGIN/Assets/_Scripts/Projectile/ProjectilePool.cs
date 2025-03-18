using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;
    public GameObject ProjectilePrefab;
    public int PoolSize = 15;

    private List<GameObject> _projectiles;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _projectiles = new List<GameObject>();

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject projectile = Instantiate(ProjectilePrefab, transform);
            projectile.SetActive(false);
            _projectiles.Add(projectile);
        }  
    }
    public GameObject GetProjectile()
    {
        for (int i = 0 ; i < _projectiles.Count; i++)
        {
            if (!_projectiles[i].activeInHierarchy)
            {
                _projectiles[i].SetActive(true);
                return _projectiles[i];
            }
        }

        GameObject newProjectile = Instantiate(ProjectilePrefab, transform);
        _projectiles.Add(newProjectile);
        newProjectile.SetActive(true);
        return newProjectile;
    }
    public void ClearOneProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
    }
    public void ClearProjectilePool()
    {
        foreach (GameObject projectile in _projectiles)
        {
            Destroy(projectile);
        }
        _projectiles.Clear();
    }
}
