
 public abstract class Unit 
{
	public int ID { get; private set; }
	public int Hp { get; private set; }
	public int Damage { get; private set; }
	public string Sprite { get; private set; }

	protected Unit(byte id,int hp, int damage, string linkToSprite)
	{
		ID = id;
		Hp = hp;
		Damage = damage;
		Sprite = linkToSprite;
	}
	
	public void SetDamage (int damage)
	{
		Hp -= damage;
		if (Hp <= 0) Death();

	}
	private void Death()
	{
		
	}
	
	public override string ToString()
	{
		return   $"{Sprite} ( ID : {ID}) \nHP : {Hp} \nDamage : {Damage}";
	}
	
	protected abstract void Attack();

	
}
