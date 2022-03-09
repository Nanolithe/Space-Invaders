using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [FormerlySerializedAs("bullet")] public GameObject bulletPrefab;
    [FormerlySerializedAs("shottingOffset")] public Transform shootOffsetTransform;
    private float movementPerSecond = 10f;
    private Animator playerAnimator;
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    //-----------------------------------------------------------------------------
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // todo - trigger a "shoot" on the animator
            playerAnimator.SetTrigger("Shoot");
            GameObject shot = Instantiate(bulletPrefab, shootOffsetTransform.position, Quaternion.identity);
            Debug.Log("Bang!");

            Destroy(shot, 3f);
        }
        
        float movementAxis = Input.GetAxis("Horizontal");
        Transform transform = GetComponent<Transform>();
        transform.position += Vector3.right * movementAxis * movementPerSecond * Time.deltaTime;

        Vector3 force = Vector3.right * movementAxis * movementPerSecond * Time.deltaTime;

        Rigidbody rbody = GetComponent<Rigidbody>();
        if (rbody)
        {
            rbody.AddForce(force, ForceMode.VelocityChange);
        }

         transform.position += Vector3.right * movementAxis * movementPerSecond * Time.deltaTime;
    }
}
