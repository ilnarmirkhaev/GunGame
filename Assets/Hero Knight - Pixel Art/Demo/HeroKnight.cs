using System;
using Player;
using UnityEngine;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] private Transform attackPoint;


    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack;
    private float               m_timeSinceAttack;
    private float               m_delayToIdle;

    public static event Action OnAttacked;
    private bool _isDead;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerHealth.OnHurt += Hurt;
        PlayerHealth.OnDied += Die;
    }
    
    private void OnDisable()
    {
        PlayerHealth.OnHurt += Hurt;
        PlayerHealth.OnDied += Die;
    }
    
    

    private void Update ()
    {
        if (_isDead) return;
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // -- Handle input and movement --
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        var attackPointPosition = attackPoint.localPosition;

        // Swap direction of sprite depending on walk direction
        if (inputX > 0 && m_facingDirection != 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
            
            attackPointPosition.x = 0.7f;
            attackPoint.localPosition = attackPointPosition;
        }
            
        else if (inputX < 0 && m_facingDirection != -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
            
            attackPointPosition.x = -0.7f;
            attackPoint.localPosition = attackPointPosition;
        }

        m_body2d.velocity = new Vector2(inputX, inputY) * m_speed;

        // -- Handle Animations --
        //Attack
        if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);
            OnAttacked?.Invoke();

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon || Mathf.Abs(inputY) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }

    private void Hurt() => m_animator.SetTrigger("Hurt");

    private void Die()
    {
        _isDead = true;
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
    }
}
