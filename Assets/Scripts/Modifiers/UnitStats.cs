using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    #region Hitpoints health
    public int startingHitPoint;
    private int _hitPointBonusFlat;
    public int HitPointBonusFlat
    {
        get => _hitPointBonusFlat;
        set => _hitPointBonusFlat = value;
    }

    private float _hitPointBonusPercent = 1;
    public float HitPointBonusPercent
    {
        get => _hitPointBonusPercent;
        set => _hitPointBonusPercent = value;
    }

    public int MaximumHitPoint
    {
        get
        {
            return (int)((HitPointBonusFlat + startingHitPoint) * HitPointBonusPercent);
        }
    }

    #endregion
    #region Movement speed
    public float startingMoveSpeed;

    private float _moveSpeedBonusFlat;
    public float MoveSpeedBonusFlat
    {
        get => _moveSpeedBonusFlat;
        set => _moveSpeedBonusFlat = value;
    }

    private float _moveSpeedBonusPercent = 1.0f;
    public float MoveSpeedBonusPercent
    {
        get => _moveSpeedBonusPercent;
        set => _moveSpeedBonusPercent = value;
    }
    public float MaximumMoveSpeed
    {
        get
        {
            return (float)((startingMoveSpeed + MoveSpeedBonusFlat) * MoveSpeedBonusPercent);
        }
    }

    #endregion
    #region Weapon speed
    public float startingNbAtkPerSecond;
    private float _nbAtkPerSecondBonusFlat = 0.0f;
    public float NbAtkPerSecondBonusFlat
    {
        get { return _nbAtkPerSecondBonusFlat; }
        set { _nbAtkPerSecondBonusFlat = value; }
    }

    private float _nbAtkPerSecondBonusPercent = 1.0f;
    public float NbAtkPerSecondBonusPercent
    {
        get { return _nbAtkPerSecondBonusPercent; }
        set
        {
            _nbAtkPerSecondBonusPercent = value;
        }
    }
    public float CurrentNbAtkPerSecond
    {
        get
        {
            return (float)((startingNbAtkPerSecond + NbAtkPerSecondBonusFlat) * NbAtkPerSecondBonusPercent);
        }
    }
    #endregion

    public delegate void OnCreated();
    public OnCreated onCreated;
    public delegate void OnDeath();
    public OnDeath onDeath;

    public delegate void OnTakeDamage(int amount, Vector3 attackPos);
    public OnTakeDamage onTakeDamage;
    public delegate void OnShoot();
    public OnShoot onShoot;
}