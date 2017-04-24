using UnityEngine;

public class ExplosionController : MonoBehaviour {

	void Start () {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, 2);
	}
}
