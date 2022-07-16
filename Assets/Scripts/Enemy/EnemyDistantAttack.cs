using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistantAttack : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float      distanceMaxToAttack;
    [SerializeField] private Transform  firingPoint;

    private UnitStats  unitStats;
    private GameObject bulletPrefab;
    private float      timeUntilNextShot;
    private float      angle;

    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Enemy Bullet");
        unitStats = GetComponent<UnitStats>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = Camera.main.WorldToScreenPoint(target.transform.position) - Camera.main.WorldToScreenPoint(transform.position);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distanceMaxToAttack && timeUntilNextShot < Time.time)
        {
            Shoot();
            timeUntilNextShot = Time.time + unitStats.CurrentNbSecondsBetweenEachAtk;
        }
    }

    private void Shoot()
    {
        if (unitStats.onShoot != null)
            unitStats.onShoot();

        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
