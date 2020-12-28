using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour
{
    [SerializeField] protected int maxHP = 50;
    [SerializeField] protected int currentHp = 50;
    public LifeBar healthBar;
    public float moveSpeed = 3;

    [SerializeField] [Range(.5f, 5f)] protected float animationSpeedModifer = 2.5f;
    [SerializeField] protected Animator animator;
    [HideInInspector] public Rigidbody rb;
    public Animator Animator { get { return animator; } }

    [SerializeField] protected AbilityManager abilityManager;

    [HideInInspector] public bool canMoveCharacter = true;
    [HideInInspector] public bool canAbility = true;
    [HideInInspector] public bool isDead;

    public AudioClip hitSound;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHp = maxHP;
        if (healthBar.CompareTag("PlayerBar"))
        {
            healthBar.gameObject.SetActive(true);
        }
        else
        {
            healthBar.gameObject.SetActive(false);
        }
        healthBar.SetMaxHealth(maxHP);
    }

    public void LoseHP(int damage)
    {
        Debug.Log(this.name + " AIE CA FAIT MAL");
        currentHp -= damage;
        if (currentHp > 0)
        {
            SoundManager.instance.PlaySFX(hitSound);
        }
       
        currentHp = Mathf.Max(currentHp, 0);

        if (healthBar.CompareTag("PlayerBar"))
        {
            healthBar.SetHealth(currentHp);
        }
        else
        {
            StartCoroutine(HealthCouroutine());
        }
    }

    protected void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoseHP(1);
        }
    }

    public void LateUpdate()
    {
        if (!isDead)
        {
            if (currentHp <= 0)
            {
                Death();
                isDead = true;
            }
        }
        if (transform.position.y<=-20)
        {
            Death();
            isDead = true;
        }
    }

    protected void Death()
    {
        canAbility = false;
        canMoveCharacter = false;
        animator.SetTrigger("Death");
    }

    private IEnumerator SpeedUpCoroutine(float abilityDuration, float mvtSpeedBoost)
    {
        moveSpeed *= (1 + mvtSpeedBoost);

        yield return new WaitForSeconds(abilityDuration);

        moveSpeed /= (1 + mvtSpeedBoost);
    }

    public void SpeedUp(float abilityDuration, float mvtSpeedBoost)
    {
        StartCoroutine(SpeedUpCoroutine(abilityDuration, mvtSpeedBoost));
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Terrain"))
        {
            canMoveCharacter = true;
            canAbility = true;
        }
    }

	private IEnumerator HealthCouroutine()
    {
        healthBar.gameObject.SetActive(true);
        healthBar.SetHealth(currentHp);
        yield return new WaitForSeconds(1f);
        healthBar.gameObject.SetActive(false);
    }

    public void KALM()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.forward = transform.forward.normalized;
    }

    public IEnumerator getPushed(float f)
    {
        var old = gameObject.layer;
        gameObject.layer = 8;
        yield return new WaitForSeconds(f);
        KALM();
        gameObject.layer = old;
    }

}
