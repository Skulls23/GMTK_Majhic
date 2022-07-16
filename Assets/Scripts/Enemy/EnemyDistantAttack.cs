using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistantAttack : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private UnitStats  unitStats;
    private GameObject bulletPrefab;
    private float timeUntilNextShot;

    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        unitStats = GetComponent<UnitStats>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && timeUntilNextShot < Time.time)
        {
            Shoot();
            timeUntilNextShot = Time.time + unitStats.CurrentNbSecondsBetweenEachAtk;
        }
    }

    private void Shoot()
    {
        /*float angle = characterAimingScript.Angle;

        if (unitStats.onShoot != null)
            unitStats.onShoot();

        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));*/
    }
}
