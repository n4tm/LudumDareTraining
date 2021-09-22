
using UnityEngine;

namespace Character
{
	public class CharacterAttack
	{
		private float nextAttack1;
		public bool Attack1Pressed { get; set; }
		private float _baseDamage;
		private float _attackSpeed;

		public CharacterAttack(float baseDamage, float attackSpeed)
		{
			_baseDamage = baseDamage;
			_attackSpeed = attackSpeed;
		}
		public void CheckAttack()
		{
			if (Attack1Pressed && Time.time > nextAttack1)
			{
				Attack1();
			}
		}
		private void Attack1()
		{
			ApplyCooldown();
			
			// Do damage here
			
			
		}

		private void ApplyCooldown()
		{
			nextAttack1 = Time.time + 3f/_attackSpeed;
		}
		

		
	}
}
