using UnityEngine;
using System.Collections;


public class Character : MonoBehaviour {

	[Header("Character Stats")]
	[SerializeField] private int _health;
	[SerializeField] private string _identity;
	[SerializeField] private CrossHairController _defaultCursor;
	[SerializeField] private int _damage;
	[SerializeField] private float _movSpeed;
	[SerializeField] private float _turnSpeed;

	private CrossHairController _cursor;

	[Header("Weapon Options")]
	[SerializeField] private Weapon _wpn;

	//Properties
	public int Health
	{
		get{ return _health;}
	}

	public string Identity {
		get { return _identity; }
		set { _identity = value; }
	}

	public int Damage
	{
		get {return _damage; }
	}

	public float MovSpeed {
		get { return _movSpeed; }
		set	{ _movSpeed = value > -1 ? value : 1; }
	}

	public float TurnSpeed {
		get{ return _turnSpeed; }
		set { _turnSpeed = value > -1 ? value : 1; }
	}

	public CrossHairController Cursor {
		get { return _cursor; }
	}

	public Weapon Wpn {
		get { return _wpn; }
		set { 
			_wpn = value; 
			InstCursor();
		}
	}

	void Awake ()
	{
		InstCursor();
	}


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (_health <= 0) {
			Debug.Log ("Destroying " + this.name + "Character");
			Destroy (this.gameObject);
		}
	}

	//Mutators
	public void subHealth(int dmg = 1)
	{
		_health -= dmg;
	}

	public void addHealth(int heal = 1)
	{
		_health += heal;
	}

	public void InstCursor ()
	{
		if(HasWeapon())
			_cursor = Instantiate(_wpn.crossHairPrefab);
		else
			_cursor = Instantiate(_defaultCursor);
	}

	public bool HasWeapon()
	{
		return _wpn != null;
	}



}//EOF
