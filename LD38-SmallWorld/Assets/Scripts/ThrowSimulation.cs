using UnityEngine;
using System.Collections;

public class ThrowSimulation : MonoBehaviour
{

	public float firingAngle = 45.0f;
	public float gravity = 9.8f;

	private Transform Projectile;      


	void Awake()
	{
		Projectile = transform;

	}



	public IEnumerator SimulateProjectile(Vector3 _target)
	{

		// Calculate distance to target
		float target_Distance = Vector3.Distance(Projectile.position, _target);

		// Calculate the velocity needed to throw the object to the target at specified angle.
		float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

		// Extract the X  Y componenent of the velocity
		float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

		// Calculate flight time.
		float flightDuration = target_Distance / Vx;

		// Rotate projectile to face the target.
		Projectile.rotation = Quaternion.LookRotation(_target - Projectile.position);

		float elapse_time = 0;

		while (elapse_time < flightDuration && Projectile.position.y >= 1f)
		{
			float deltaY = (Vy - (gravity * elapse_time)) * Time.deltaTime;

			Projectile.Translate(0, deltaY, Vx * Time.deltaTime);

			elapse_time += Time.deltaTime;

			yield return null;
		}

	}  
}
