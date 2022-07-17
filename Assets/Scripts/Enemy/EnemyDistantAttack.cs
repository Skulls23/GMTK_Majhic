using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistantAttack : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject firingPoint;
    [SerializeField] private float      distanceMaxToAttack;

    private UnitStats  unitStats;
    private GameObject bulletPrefab;
    private float      timeUntilNextShot;

    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Enemy Bullet");
        unitStats = GetComponent<UnitStats>();
    }

    void Start()
    {
        player = UnitsManager.Instance.player;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceMaxToAttack && timeUntilNextShot < Time.time)
        {
            Shoot();
            timeUntilNextShot = Time.time + unitStats.CurrentNbSecondsBetweenEachAtk;
        }
    }

    private void Shoot()
    {
        if (unitStats.onShoot != null)
            unitStats.onShoot();

        Vector3 direction = Camera.main.WorldToScreenPoint(player.transform.position) - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Instantiate(bulletPrefab, firingPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
