using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shooting : MonoBehaviour
{
    WeaponData weaponData;
    public GameObject player;

    private float damage;
    public float Damage
    {
        get { return weaponData.Damage; }
    }

    [SerializeField]
    JoystickController joystick;

    [SerializeField]
    string targetString; // цель для атаки

    [SerializeField]
    Transform firingPoint; // точка выстрела

    GameObject closestEnemy;

    float timer, timerClip;
    int curAmmo;
    Animator anim;
    BattleController battleController;

    private void Start()
    {
        battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
        weaponData = battleController.GetSpawnLevelData.GetWeaponData;
        curAmmo = weaponData.AmmoCount;
        anim = player.GetComponent<Animator>();
        // отображаем на экране количество пуль  в обойме из возможных
        UIManager.instance.ClipDisplay(weaponData.AmmoCount, curAmmo);
    }

 
    private void FixedUpdate()
    {
        // ищем цель в радиусе атаки
        closestEnemy = FindClosestEnemy();
        timerClip += Time.deltaTime;
        // если находим, и игрок стоит на месте, и враг не мертв, то поворачиваемся к цели
        if (closestEnemy != null && (Mathf.Abs(joystick.direction.x) < 0.01f && Mathf.Abs(joystick.direction.y) < 0.01f))
        {
            LookAtTarget(player, closestEnemy);
            Shoot();
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetString);
        GameObject closest = null;

        Dictionary<float, GameObject> distancesAndEnemy = new Dictionary<float, GameObject>();

        // вычисляем дистанцию для каждого врага изаносим ее в словарь, если дистанция меньше, чем макс радиус
        foreach (GameObject enemy in enemies)
        {
            var distance = Vector3.Distance(enemy.transform.position, transform.position);
            var isDead = enemy.GetComponent<EnemyHealth>().IsDied;
            // если  враг в радиусе атаки и он не мертв, то доавляем его в массив ближайших врагов
            if (distance < weaponData.MaxAttackRange && !isDead)
            {                
                try
                {
                    // добавляем в словарь врагов по дистанции
                    distancesAndEnemy.Add(distance, enemy);
                }
                catch(System.ArgumentException)
                {
                    Debug.Log("враги на одинаковом расстоянии");
                }
            }
        }

        // находим минимальный ключ из всех ключей и по нему выбираем врага
        if (distancesAndEnemy.Count > 0)
        {
            float minDis = distancesAndEnemy.Keys.Min();
            closest = distancesAndEnemy[minDis];
        }
       return closest;
    }

    public void LookAtTarget(GameObject player, GameObject target)
    {
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        player.transform.rotation = rotation;
    }

    public void Shoot()
    {
        if (curAmmo > 0)
        {
            timer += Time.deltaTime;
            if (timer >= weaponData.CdBetweenShots)
            {
                timer = 0f;
                curAmmo--; // уменьшаем на 1 количество патронов в обойме
                //Debug.Log("bullets: " + curAmmo);
                // создаем пулю
                UIManager.instance.ClipDisplay(weaponData.AmmoCount, curAmmo);
                anim.SetTrigger("Shot");

                for (int i = 0; i < weaponData.NumberOfBulletsPershot; i++)
                {
                    CreateBulletAndGivedItSpeed();
                }                
            }
            if (curAmmo == 0)
            {
                timerClip = 0f;
            }
        }
        else
        {
            //timerClip += Time.deltaTime;
            if (timerClip >= weaponData.ClipReload)
            {
                curAmmo = weaponData.AmmoCount;
                timerClip = 0f;
            }
        }

    }

    private void CreateBulletAndGivedItSpeed()
    {
        var bullet = ObjectPooler.instance.GetObject(weaponData.BulletType, firingPoint.transform.position, firingPoint.transform.rotation);
        //Rigidbody shellInstance = Instantiate(bullet.GetComponent<Rigidbody>(), firingPoint.transform.position, firingPoint.transform.rotation);
        // придаем ей скорость
        //считаем тангенс maxAngleDispersion
        //умножаем получившийся тангенс на рандом от -1 до 1, умножаем получившееся значение на вектор вправо, прибавляем к форварду и нормализуем
        var tg = Mathf.Tan(Mathf.Deg2Rad * weaponData.MaxAngleDispersion);
        float dispersion = tg * Random.Range(-1f, 1f);
        Vector3 direction = (firingPoint.forward + Vector3.right * dispersion).normalized;
        float speed = weaponData.ShootSpeed * Random.Range(0.9f, 1.1f);
        bullet.GetComponent<Rigidbody>().velocity = speed * direction;
    }
}
