using UnityEngine;
using System.Collections;

// ----- Low Poly FPS Pack Free Version -----
public class Bullet : MonoBehaviour {

	[Range(5, 100)]
	[Tooltip("After how long time should the bullet prefab be destroyed?")]
	public float destroyAfter;
	[Tooltip("If enabled the bullet destroys on impact")]
	public bool destroyOnImpact = false;
	[Tooltip("Minimum time after impact that the bullet is destroyed")]
	public float minDestroyTime;
	[Tooltip("Maximum time after impact that the bullet is destroyed")]
	public float maxDestroyTime;

    [SerializeField] float damageBullet;
    [SerializeField] GameObject bloodPrefab;
	[SerializeField] bool turretBullet;

	[Header("Impact Effect Prefabs")]
	public Transform [] metalImpactPrefabs;

    private Rigidbody myRB;

	private void Start () 
	{
		//Start destroy timer
		StartCoroutine (DestroyAfter ());
        myRB = GetComponent<Rigidbody>(); 
		if(!turretBullet)
		{
			damageBullet = PlayerState.gunDamage;
		}   
	}

    private void Update() 
    {
    }

	//If the bullet collides with anything
	private void OnCollisionEnter (Collision collision) 
	{

        if(collision.gameObject.tag == "EnemyBody")
        {
            collision.gameObject.transform.parent.parent.GetComponent<EnemyController>().DamageEnemy(damageBullet);
            GameObject actualBlood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            actualBlood.transform.rotation = Quaternion.FromToRotation(actualBlood.transform.right, transform.up);
			Destroy(actualBlood, 3f);
        }

		//If destroy on impact is false, start 
		//coroutine with random destroy timer
		if (!destroyOnImpact) 
		{
			StartCoroutine (DestroyTimer ());
		}
		//Otherwise, destroy bullet on impact
		else 
		{
			Destroy (gameObject);
		}

		
	}

	private IEnumerator DestroyTimer () 
	{
		//Wait random time based on min and max values
		yield return new WaitForSeconds
			(Random.Range(minDestroyTime, maxDestroyTime));
		//Destroy bullet object
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfter () 
	{
		//Wait for set amount of time
		yield return new WaitForSeconds (destroyAfter);
		//Destroy bullet object
		Destroy (gameObject);
	}
}
// ----- Low Poly FPS Pack Free Version -----