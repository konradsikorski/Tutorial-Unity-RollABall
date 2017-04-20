using UnityEngine;

public class AsteroidManager : MonoBehaviour {
    public float SizeMin;
    public float SizeMax;
    public float SpeedMin;
    public float SpeedMax;
    public float TumbleMin;
    public float TumbleMax;
    public float DeltaTime;

    public GameObject[] Asteroids;

    private float nextAsteroidTime;
    	
	// Update is called once per frame
	void Update () {
		if(UIController.Instance.GameReady && nextAsteroidTime < Time.time)
        {
            nextAsteroidTime = Time.time + DeltaTime;
            CreateAsteroid();
        }
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
