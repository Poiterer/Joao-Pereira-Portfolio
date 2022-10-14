using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{ 
    [Header("Camera and Animator Ref")]

    [SerializeField] private Camera camera;
    [SerializeField] private Animator myAnimator;
    private Rigidbody rb;

    [Space]

    [Header("Movement:")]
    [SerializeField] private float MoveSpeed = 3f;
    private PlayerInputHandler input;
    private Vector2 inputVector = Vector2.zero;
    private Vector3 moveDirection = Vector2.zero;

    [Space]

    [Header("Jump:")]
    [SerializeField] private float JumpForce = 150f;
    private bool jumpInput = false;
    private bool canJump = true;
    private bool isGrounded = true;

    [Space]

    [Header("Ground Detection:")]
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private LayerMask pickupCastLayerMask;
    [SerializeField] private LayerMask noPickupCastLayerMask;
    [SerializeField] private GameObject holdPosition;
    [SerializeField] private GameObject detectedObject;
    [SerializeField] private GameObject heldGameObject;
    private bool interactInput = false;

    [Space]

    [Header("Throw:")]
    [SerializeField] private float throwStrenght;
    private bool throwInput = false;

    [Space]

    [Header("Dash:")]
    [SerializeField] private float dashForce = 150f;
    [SerializeField] private float dashCooldown = 1f;
    private bool dashInput = false;
    private bool canDash = true;
    private bool isDashing = false;
    private bool isDashCooldown = false;
    [SerializeField] private ParticleSystem dashParticleSystem;

    [Space]

    [Header("Taping:")]
    [SerializeField] private Canvas ductapeUICanvas;
    private bool tapeInput = false;
    private bool isTapingObject = false;
    private Ductapable objectBeingTaped;
    private float timeToTape = 2f;
    private float timeTaping = 0f;
    [SerializeField] private ParticleSystem tapeParticleSystem;

    [Space]

    [Header("Audio:")]
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource pickUpSound;
    [SerializeField] private AudioSource DropSound;
    [SerializeField] private AudioSource JumpSound;
    [SerializeField] private AudioSource TapingSound;

    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        ductapeUICanvas.GetComponent<LookAtTarget>().SetTarget(camera.gameObject);

    }

    void Update()
    {


        if (!isDashing)
        {
            if (!isTapingObject)
            {
                moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
            }

        }

        if (dashInput && !canDash)
        {
            dashInput = false;
        }

        if (isTapingObject)
        {
            if (timeTaping < timeToTape)
            {
                TapeChanneling();
                myAnimator.SetBool("TapingObject", true);
            }
            else
            {
                objectBeingTaped.TapeInPlace();
                isTapingObject = false;
                objectBeingTaped = null;
                var tape = heldGameObject.GetComponent<DuctapeScript>();
                if (tape != null)
                {
                    int charges = tape.CurrentCharges();
                    if(charges == 1)
                    {
                        heldGameObject = null;
                        myAnimator.SetLayerWeight(1, 0f);
                    }
                    tape.UseCharge();
                }

                tapeParticleSystem.Stop();
                tapeParticleSystem.Clear();
                TapingSound.Stop();


                LookAtTarget ui = ductapeUICanvas.GetComponent<LookAtTarget>();
                if (ui != null)
                {
                    ui.ChangeVisibility(false);
                }

                myAnimator.SetBool("TapingObject", false);

            }
        }

        myAnimator.SetBool("IsGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            if (jumpInput && canJump)
            {
                if (isGrounded)
                {
                    Jump();

                }
                jumpInput = false;
            }
            if (interactInput)
            {
                Interact();

                interactInput = false;
            }
            if (throwInput)
            {
                Throw();

                throwInput = false;
            }
            if (tapeInput && !isTapingObject)
            {
                TapeObject();
            }
            else if (!tapeInput && isTapingObject)
            {
                isTapingObject = false;
                objectBeingTaped = null;

                tapeParticleSystem.Stop();
                tapeParticleSystem.Clear();

                LookAtTarget ui = ductapeUICanvas.GetComponent<LookAtTarget>();
                if (ui != null)
                {
                    ui.ChangeVisibility(false);
                }
            }

            if (!isTapingObject)
            { 
                var movementVector = Move(moveDirection);
                Rotate(movementVector);

                if (jumpInput && !canJump)
                {
                    jumpInput = false;
                }

                myAnimator.SetFloat("MoveSpeed", moveDirection.magnitude);
            }

            if (dashInput)
            {
                if (isGrounded && canDash)
                {
                    if (!isDashCooldown)
                    {
                        Dash();
                    }                 
                }

                dashInput = false;
            }

            isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.25f, groundCheckLayerMask);

            CastForward();
        }

    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
        inputVector.Normalize();
    }

    public void JumpInput()
    {
        jumpInput = true;
        Debug.Log("JUMPED");
    }

    public void InteractInput()
    {
        //create Interact Inputs
        interactInput = true;
        Debug.Log("Interacted");
    }

    public void ThrowInput()
    {
        throwInput = true;
        Debug.Log("Throw");
    }

    public void DashInput()
    {
        dashInput = true;
        Debug.Log("Dash");
    }

    public void TapeObjectInputStarted()
    {
        tapeInput = true;
        Debug.Log("TapeInputStart");
    }

    public void TapeObjectInputEnded()
    {
        tapeInput = false;
        Debug.Log("TapeInputStart");
    }

    private Vector3 Move(Vector3 vector)
    {
        vector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * vector;

        var speed = MoveSpeed * Time.deltaTime;

        var targetPosition = transform.position + vector * speed;
        transform.position = targetPosition;
        return vector;
    }

    private void Rotate(Vector3 vector)
    {
        if (vector != Vector3.zero)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            var rotation = Quaternion.LookRotation(vector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 15f);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    private void Jump()
    {
        rb.AddForce(0.0f, JumpForce, 0.0f);
        myAnimator.SetTrigger("Jump");

        JumpSound.Play();
    }

    private void Interact()
    {
        if (detectedObject != null && heldGameObject == null)
        {

            PickupComponent component = detectedObject.GetComponent<PickupComponent>();

            if (component != null)
            {
                component.Pickup(gameObject);
                heldGameObject = detectedObject;

                if (heldGameObject != null)
                {
                    myAnimator.SetTrigger("GrabObject");

                    pickUpSound.Play();
                }
            }
            else
            {
                DuctapeHolderScript crate = detectedObject.GetComponent<DuctapeHolderScript>();
                if (crate != null)
                {
                    heldGameObject = crate.Interact(gameObject);
                    if (heldGameObject != null)
                    {
                        myAnimator.SetTrigger("GrabObject");

                        pickUpSound.Play();

                    }
                }

                else
                {
                    CounterScript counter = detectedObject.GetComponent<CounterScript>();
                    if (counter != null)
                    {
                        heldGameObject = counter.Interact(gameObject);
                        Debug.Log("Counter Interact");

                        if (heldGameObject != null)
                        {
                            myAnimator.SetTrigger("GrabObject");

                            pickUpSound.Play();

                        }
                    }
                }
            }
        }

        else if (heldGameObject != null)
        {
            if (detectedObject != null)
            {
                CounterScript counter = detectedObject.GetComponent<CounterScript>();

                if (counter != null)
                {
                    GameObject obj = counter.GetHeldObject();
                    if (obj != null)
                    {
                        PickupComponent comp = heldGameObject.GetComponent<PickupComponent>();

                        if (comp != null)
                        {
                            comp.Pickup(gameObject);

                            heldGameObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
                            heldGameObject = null;

                            myAnimator.SetLayerWeight(1, 0f);

                            DropSound.Play();
                        }
                    }
                    else
                    {
                        heldGameObject = counter.Interact(gameObject);
                        Debug.Log("Counter Interact");

                        if (heldGameObject != null)
                        {
                            myAnimator.SetLayerWeight(1, 0f);

                            DropSound.Play();
                        }
                    }
                }

                else
                {
                    PickupComponent comp = heldGameObject.GetComponent<PickupComponent>();

                    if (comp != null)
                    {
                        comp.Pickup(gameObject);

                        heldGameObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
                        heldGameObject = null;

                        myAnimator.SetLayerWeight(1, 0f);

                        DropSound.Play();
                    }
                }
            }

            else
            {
                PickupComponent component = heldGameObject.GetComponent<PickupComponent>();
                if (component != null)
                {
                    component.Pickup(gameObject);

                    heldGameObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
                    heldGameObject = null;

                    DropSound.Play();

                    myAnimator.SetLayerWeight(1, 0f);
                }
            }
        }
    }

    private void Throw()
    {
        if (heldGameObject != null)
        {
            PickupComponent component = heldGameObject.GetComponent<PickupComponent>();

            if (component != null)
            {
                component.Pickup(gameObject);
                Vector3 direction = new Vector3(transform.forward.x, 0.75f, transform.forward.z);
                heldGameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                heldGameObject.GetComponent<Rigidbody>().AddForce(direction * throwStrenght);
                heldGameObject = null;

                //start throwing animation here
                myAnimator.SetLayerWeight(1, 0f);
                myAnimator.SetTrigger("Throw");

                DropSound.Play();
            }
        }
    }

    private void Dash()
    {
        isDashing = true;
        isDashCooldown = true;

        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);

        dashParticleSystem.Play();
        dashSound.Play();

        StartCoroutine(DashDuration());
    }

    private void TapeObject()
    {
        if (heldGameObject != null)
        {
            DuctapeScript tapeObject = heldGameObject.GetComponent<DuctapeScript>();
            if (tapeObject != null)
            {
                if (detectedObject != null)
                {
                    CounterScript counter = detectedObject.GetComponent<CounterScript>();
                    if (counter != null)
                    {
                        GameObject heldObject = counter.GetHeldObject();
                        if (heldObject != null)
                        {
                            Ductapable ductapable = heldObject.GetComponent<Ductapable>();
                            if (ductapable != null)
                            {
                                if (!ductapable.GetTapedStatus())
                                {
                                    objectBeingTaped = ductapable;
                                    isTapingObject = true;
                                    timeTaping = 0f;

                                    tapeParticleSystem.Play();

                                    TapingSound.Play();

                                    LookAtTarget ui = ductapeUICanvas.GetComponent<LookAtTarget>();
                                    if (ui != null)
                                    {
                                        ui.ChangeVisibility(true);
                                        ui.UpdateBar(0f);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void TapeChanneling()
    {
        timeTaping += Time.deltaTime;

        LookAtTarget ui = ductapeUICanvas.GetComponent<LookAtTarget>();
        if (ui != null)
        {
            ui.UpdateBar(timeTaping / timeToTape);
        }
    }

    private void CastForward()
    {
        Vector3 boxHalfSize = new Vector3(0.25f, 1.0f, 0.25f);
        RaycastHit hitInfo;
        bool hit;
        if (heldGameObject != null)
        {
            hit = Physics.BoxCast(transform.position, boxHalfSize, transform.forward, out hitInfo, Quaternion.identity, 2f, noPickupCastLayerMask);
        }
        else
        {
            hit = Physics.BoxCast(transform.position, boxHalfSize, transform.forward, out hitInfo, Quaternion.identity, 2f, pickupCastLayerMask);
        }

        Debug.DrawRay(transform.position, transform.forward * 2f, Color.red);

        if (hit)
        {
            if (detectedObject != hitInfo.transform.gameObject && detectedObject != null)
            {
                Outline outlineCompOld = detectedObject.GetComponent<Outline>();
                if (outlineCompOld != null)
                {
                    outlineCompOld.enabled = false;                    
                }
                DuctapeHolderScript ductHolderCompOld = detectedObject.GetComponent<DuctapeHolderScript>();
                if (ductHolderCompOld != null)
                {
                    if (!ductHolderCompOld.isRecharging)
                    {
                        ductHolderCompOld.SetIconState(false);
                    }
                }
            }

            detectedObject = hitInfo.transform.gameObject;

            Outline outlineComp = detectedObject.GetComponent<Outline>();
            if (outlineComp != null)
            {
                outlineComp.enabled = true;                
            }
            DuctapeHolderScript ductHolderComp = detectedObject.GetComponent<DuctapeHolderScript>();
            if (ductHolderComp != null)
            {
                ductHolderComp.SetIconState(true);
            }

        }
        else if (detectedObject != null)
        {
            Outline outlineComp = detectedObject.GetComponent<Outline>();
            if (outlineComp != null)
            {
                outlineComp.enabled = false;
            }

            DuctapeHolderScript ductHolderCompOld = detectedObject.GetComponent<DuctapeHolderScript>();
            if (ductHolderCompOld != null)
            {
                if (!ductHolderCompOld.isRecharging)
                {
                    ductHolderCompOld.SetIconState(false);
                }
            }

            detectedObject = null;
        }
    }

    public Transform GetHoldTransform()
    {
        return holdPosition.transform;
    }

    public GameObject GetHeldObject()
    {
        return heldGameObject;
    }

    public void SetHeldObject(GameObject obj)
    {
        heldGameObject = obj;
    }

    public void SetCamera(Camera cam)
    {
        camera = cam;
    }


    private IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(0.15f);
        rb.velocity = Vector3.zero;
        isDashing = false;
        dashParticleSystem.Stop();
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        isDashCooldown = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, 0.25f);
    }
}
