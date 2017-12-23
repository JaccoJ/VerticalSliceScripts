using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiMovement : MonoBehaviour
{
    public float duratation;
    public Transform target;
    public bool fire = false;
    public float MinAttackDist;
    public Transform Player;
    public bool retreat;
    [SerializeField]
    private int MoveSpeed = 4;

    [SerializeField]
    private float MinDist = 1;
    private AiAttack _enemyattack;
    private float _fireRate = 2.00f;
    private float _nextFire = 0.0f;
    private Rigidbody rb;
    private Vector3 retreatPoint;
    private bool walk;




   

    void Start()
    {
        



    }
    
    void Awake()
    {

        Player = GameObject.FindWithTag("Player").transform;
        _enemyattack = GetComponent<AiAttack>();
		rb = GetComponent<Rigidbody>();
    }

	void Update()
	{
        print(fire);
        print(MoveSpeed);

        StartCoroutine(WaitAndPrint());

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
		{
			Move();
		}

		else if (Vector3.Distance(transform.position, Player.position) <= MinDist)
		{
            Shoots();
        }

        if (fire == true)
		{
            
            MoveToPoint();
        }   
        if (Input.GetKey(KeyCode.Space))
        {
            
        }
	}

    IEnumerator WaitAndPrint()
    {
        float elapsedtime = 0f;
        while (elapsedtime < duratation)
        {
            
            transform.LookAt(Player);

            if (Time.time > _nextFire)
            {
                
                _nextFire = Time.time + _fireRate;
                _enemyattack.Shoot();
                
            }
          

            elapsedtime += Time.deltaTime;
            MoveSpeed = 0;
            yield return null;
        }
        MoveSpeed = 2;

        MoveToPoint();
       
    }

    void Shoots()
	{
        fire = true;
		
		
	}

	void Move()
	{
       
		transform.LookAt(Player);
        fire = false;
        MoveSpeed = 3;
		transform.position += transform.forward * MoveSpeed * Time.deltaTime;
		print("go forward");
	}

	void MoveToPoint()
	{
      //  MoveSpeed = 3;

		float step = MoveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);

		transform.LookAt(target);

	}
  

}   