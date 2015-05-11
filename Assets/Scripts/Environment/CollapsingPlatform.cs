using UnityEngine;
using System.Collections;

public class CollapsingPlatform : MonoBehaviour 
{
	public float collapseDelay = 1;

    private Rigidbody2D rb;

	void Start()
	{
		rb = this.GetComponent<Rigidbody2D>();
	}	

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Hero")
			Invoke("Collapse", collapseDelay);
		else if (collision.gameObject.CompareTag("Platform"))
			Invoke("Collapse", 0);
	}

	void Collapse()
	{
		this.rb.isKinematic = false;
		this.GetComponent<BoxCollider2D>().isTrigger = true;
	}
}
