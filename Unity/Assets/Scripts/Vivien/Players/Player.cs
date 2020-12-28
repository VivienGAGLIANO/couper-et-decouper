using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    //[SerializeField] private int jumpStrength = 300;
    public KeyCode[] abilitiesKeyboardMapping = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };
    public bool lookAtMouse = true;

    private Dictionary<KeyCode, int> abilitiesKeyboardMappingDictionary = new Dictionary<KeyCode, int>();
    //private bool jump = false;
    //private bool canJump = true;
    //private float jumpTimer;
    private bool dontMove = false;

    private float walkSoundFrequency = 0.45f;
    private float walkCounter = 0.5f;
    public AudioClip walkSound;
    public AudioClip deathSound;

    [SerializeField] private GameObject boss;

    protected new void Start()
    {
        base.Start();

        GameManager.instance.UpdateAbilityKeys();
        UpdateKeyMapping();
    }

    // Player control here
    protected new void Update()
    {
        base.Update();

        UpdateAnimator();

        // He face towards mouse
        // Orthographic camera
        // transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        // Perspective camera
        if (lookAtMouse)
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            if (!Object.Equals(hit, null))
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }

        // He jumpn't
        //if (canJump && Input.GetKeyDown(KeyCode.Space))
        //{
        //    jump = true;
        //}
        //else
        //{
        //    jump = false;
        //}
        //jumpTimer -= Time.deltaTime;

        // But most importantly
        // He attakn't
        //if (Input.GetKeyDown(GameManager.instance.action_1))
        //{
        //    animator.SetTrigger("BaseAttack");
        //    animator.SetTrigger("CastSpell");
        //}
        //if (Input.GetKeyDown(GameManager.instance.action_3))
        //{
        //    // De-activate look at mouse during spin attack
        //    // De-activate jump during spin attack
        //    animator.SetTrigger("SpinAttack");
        //}

        // And he abilititties
        if (canAbility)
        {
            foreach (KeyCode key in abilitiesKeyboardMappingDictionary.Keys)
            {
                if (Input.GetKeyDown(key))
                {
                    abilityManager.CastAbility(abilitiesKeyboardMappingDictionary[key]);
                    //Debug.Log("[Ability manager : " + this.gameObject.name + "] " + abilitiesKeyboardMappingDictionary[key] + " ability cast : " + (abilityManager.CastAbility(abilitiesKeyboardMappingDictionary[key]) ? "successful" : "failure"));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Death();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(boss, 2 * new Vector3(0, 2, 2), Quaternion.identity);
        }
    }

    protected void FixedUpdate()
    {
        // He movement
        if (canMoveCharacter)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 finalVelocity = new Vector3(horizontal * moveSpeed, /*rb.velocity.y*/0, vertical * moveSpeed);
            rb.velocity = Vector3.Lerp(rb.velocity, finalVelocity, .1f);
            if (rb.velocity.magnitude < .1f)
            {
                rb.velocity = Vector3.zero;
                transform.forward = new Vector3(0, 0, 1);
                transform.up = new Vector3(0, 1, 0);
                transform.right = new Vector3(1, 0, 0);
            }

            if (horizontal != 0 || vertical !=0)
            {
                walkCounter += Time.deltaTime;
                if (walkCounter >= walkSoundFrequency)
                {
                    walkCounter = 0;
                    SoundManager.instance.PlaySFX(walkSound,0.2f);
                }
            }
            //if (jump && canJump && jumpTimer < 0)
            //{
            //    rb.velocity -= rb.velocity.y * Vector3.up;
            //    rb.AddForce(Vector3.up * jumpStrength);
            //    canJump = false;
            //    animator.SetTrigger("Jump");
            //}
        }
		if (dontMove)
        {
            rb.velocity = Vector3.zero;
            transform.forward = new Vector3(0, 0, 1);
            transform.up = new Vector3(0, 1, 0);
            transform.right = new Vector3(1, 0, 0);
        }
    }

    public new void LateUpdate()
    {
        if (!isDead)
        {
            if (currentHp <= 0)
            {
                Death();
                isDead = true;
            }
        }
        
    }

    protected new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.collider.CompareTag("Terrain"))
        {
            //canJump = true;
            lookAtMouse = true;
        }
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("FrontVelocity", Vector3.Dot(rb.velocity, transform.forward) * animationSpeedModifer);
        animator.SetFloat("LateralVelocity", Vector3.Dot(rb.velocity, transform.right) * animationSpeedModifer);
        float velocity = rb.velocity.magnitude < .1f ? 1f : rb.velocity.magnitude / animationSpeedModifer;
        animator.SetFloat("Velocity", velocity);
    }

    public void UpdateKeyMapping()
    {
        for (int i = 0; i < abilitiesKeyboardMapping.Length; i++)
        {
            abilitiesKeyboardMappingDictionary.Add(abilitiesKeyboardMapping[i], i);
        }
    }

    public void ResetForNewWave()
    {
        currentHp = maxHP;
        Vector3 back = GameManager.instance.map.GetSpawn();
        transform.position = new Vector3(back.x,1,back.z);
    }

    protected new void Death()
    {
        base.Death();
        SoundManager.instance.PlaySFX(deathSound);
        lookAtMouse = false;
        StartCoroutine(EndRoutine());
    }

    private IEnumerator EndRoutine()
    {
        Time.timeScale = 0.5f;
        Camera.main.GetComponent<CameraFollowPlayer>().zoom = 0.4f;
        SoundManager.instance.PlayMusicWithCrossFade(GameManager.instance.looseMusics[Random.Range(0,12)]);
        SoundManager.instance.ChangeLoop(false);
        yield return new WaitForSeconds(3f);
        GameManager.defeat = true;
        Time.timeScale = 0f;
    }
}
