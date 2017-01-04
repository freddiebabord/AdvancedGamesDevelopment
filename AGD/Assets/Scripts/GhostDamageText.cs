using UnityEngine;
using System.Collections;

public class GhostDamageText : PooledObject
{

	public TextMesh damageText;
	public float floatingSpeed, rotationSpeed;
	public float visibleTime = 5.0f;
	private float startTime = 0.0f;
	public Color colour;

	// Use this for initialization
	void OnEnable () {
		Invoke("DisableFromPool", 5.0f);
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(floatingSpeed * Mathf.Sin(Time.time), floatingSpeed, floatingSpeed * Mathf.Sin(Time.time) * Time.deltaTime));
		transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime);
		colour.a = Mathf.Lerp(1, 0, (Time.time - startTime) / visibleTime);
		damageText.color = colour;
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
