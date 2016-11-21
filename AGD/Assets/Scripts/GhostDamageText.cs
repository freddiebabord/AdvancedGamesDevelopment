using UnityEngine;
using System.Collections;

public class GhostDamageText : MonoBehaviour {

	public TextMesh damageText;
	public float floatingSpeed;
	public ObjectPool owningPool;

	// Use this for initialization
	void Start () {
		Invoke("DisableFromPool", 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(Mathf.Sin(Time.time), floatingSpeed, Mathf.Sin(Time.time) * Time.deltaTime));
	}

	void DisableFromPool()
	{
		owningPool.Despawn(gameObject);
	}

	public void SetDamageText(float dmg)
	{
		damageText.text = dmg.ToString();
	}
}
