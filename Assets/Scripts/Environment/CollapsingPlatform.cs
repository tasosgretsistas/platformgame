using UnityEngine;
using System.Collections;

public class CollapsingPlatform : MonoBehaviour 
{
    // The delay between the hero touching the platform and the platform falling.
	public float collapseDelay = 1;

    // Self-reference to the platform's Rigidbody2D component.
    private Rigidbody2D rb;

	void Start()
	{
		rb = this.GetComponent<Rigidbody2D>();
	}	

	void OnCollisionStay2D(Collision2D collision)
	{
        // If this platform collides with the hero...
		if (collision.gameObject.name == "Hero")
            // ... it collapses after a delay.
			Invoke("Collapse", collapseDelay);

        // Else, if it collides with another platform...
		else if (collision.gameObject.CompareTag("Platform"))
            // ... it collapses instantly, so as to facilitate for a chain reaction of platforms falling on top of each other.
			Invoke("Collapse", 0);
	}

	void Collapse()
	{
        // This function is pretty plain and should probably become more complex sometime in the future.
        // However, the current iteration covers the necessary functionality.

        this.rb.isKinematic = false;
		this.GetComponent<BoxCollider2D>().isTrigger = true;
	}
}
