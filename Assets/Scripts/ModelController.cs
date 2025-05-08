using UnityEngine;

public class ModelController : MonoBehaviour
{
    private CharacterController controller;
    //private Camera cam;
    private Animator animator;

    [SerializeField] float speed = 5f;
    private float pitch;
    private float yaw;
    private Camera cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        ////Cam NONSENSE.
        float mouseX = Input.GetAxis("Mouse Y");
        float mouseY = Input.GetAxis("Mouse X");

        yaw += mouseY;
        pitch -= mouseX;
        pitch = Mathf.Clamp(pitch, -30, 60f);

        cam.transform.rotation = Quaternion.Euler(pitch ,yaw, 0);
        transform.rotation = Quaternion.Euler(0 ,yaw, 0);

        //Player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        if (inputDirection.magnitude >= 0.01f)
        {
            Vector3 moveDirection = transform.right * inputDirection.x + transform.forward * inputDirection.z;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        animator.SetFloat("Move_X", inputDirection.normalized.x);
        animator.SetFloat("Move_Y", inputDirection.normalized.z);

    }
}
