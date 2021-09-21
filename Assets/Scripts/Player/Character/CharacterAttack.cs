
using UnityEngine;

namespace Character
{
	public class CharacterAttack
	{
		public float NextAttack1 { get; private set; }
		private float _baseDamage;
		private float _attackSpeed;

		public CharacterAttack(float baseDamage, float attackSpeed)
		{
			_baseDamage = baseDamage;
			_attackSpeed = attackSpeed;
		}

		public void Attack1()
		{
			ApplyCooldown();
			
			// Do damage here
			
			
			
		}

		private void ApplyCooldown()
		{
			NextAttack1 = Time.time + 3f/_attackSpeed;
		}
		
	}
}
