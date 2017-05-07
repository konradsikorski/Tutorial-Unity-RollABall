using UnityEngine;

public class AsteroidManager : MonoBehaviour {
    public float SizeMin;
    public float SizeMax;
    public float SpeedMin;
    public float SpeedMax;
    public float TumbleMin;
    public float TumbleMax;
    public float DeltaTime;
    public float AsteroidsDeltaTime;
    public float EnemiesDeltaTime;

    public GameObject[] Asteroids;
    public GameObject[] Enemies;

    private float nextAsteroidTime;
    private float nextEnemyTime;

    // Update is called once per frame
    void Update () {
        if (!GameController.Instance.IsActive) return;

        if (AsteroidsDeltaTime != 0 && nextAsteroidTime < Time.time)
        {
            nextAsteroidTime = Time.time + AsteroidsDeltaTime;
            CreateAsteroid();
        }

        if (EnemiesDeltaTime != 0 && nextEnemyTime < Time.time)
        {
            nextEnemyTime = Time.time + EnemiesDeltaTime;
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        var position = new Vector3(
            Random.Range(-6, 6),
            0,
            16
            );

        var enemyIndex = Random.Range(0, Enemies.Length);
        var enemy = Instantiate(
            Enemies[enemyIndex],
            position,
            Quaternion.identity);

        //var controler = enemy.GetComponent<EnemyController>();
        //controler.Speed = Random.Range(SpeedMin, SpeedMax);
    }

    private void CreateAsteroid()
    {
        var size = Random.Range(SizeMin, SizeMax);
        var scale = new Vector3(size, size, size);
        
        var rotation = Random.rotation;
        var position = new Vector3(
            Random.Range(-6, 6),
            0,
            16
            );

        var asteroidIndex = Random.Range(0, Asteroids.Length );
        var asteroid = Instantiate(
            Asteroids[asteroidIndex],
            position,
            rotation);

        asteroid.transform.localScale = scale;
        var controler = asteroid.GetComponent<AsteroidController>();
        controler.Speed = Random.Range(SpeedMin, SpeedMax);
        controler.Tumble = Random.Range(TumbleMin, TumbleMax);
        controler.Size = size;
    }
}
