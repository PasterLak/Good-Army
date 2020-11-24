
 public abstract class Unit 
{
	protected int id { get; private set; }
	protected int hp { get; private set; }
	protected int damage { get; private set; }
	public string sprite { get; private set; }

	public delegate void EventHandler();
	public event EventHandler OnDied;

	protected Unit(byte id,int hp, int damage, string linkToSprite)
	{
		this.id = id;
		this.hp = hp;
		this.damage = damage;
		sprite = linkToSprite;
	}
	
	public void SetDamage (int damage)
	{
		hp -= damage;
		if (hp <= 0) Death();

	}
	private void Death()
	{
		OnDied?.Invoke();
	}
	
	public override string ToString()
	{
		return   $"{sprite} ( ID : {id}) \nHP : {hp} \nDamage : {damage}";
	}
	
	protected abstract void Attack();

	
}
