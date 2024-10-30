using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    float h = 0.0f; //int, string, bool
    float v = 0.0f;
    float r = 0.0f;
    float moveSpeed = 10.0f;
    float rotaionSpeed = 500.0f;
    Transform playerTr;
    int bottle = 0;
    bool isJump = false;
    float jumpPower = 7.0f;
    Rigidbody rigidbody;
    AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject effectObject;
    Animator animator;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        playerTr = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");
        //Debug.Log("H : " + h.ToString() + ", V : " + v.ToString());
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //playerTr.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime);
        playerTr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
        }
        if (v == 0.0f && h == 0.0f)
        {
            //∏ÿ√„
            animator.SetBool("Walk", false);
        } else
        {
            //∞»±‚
            animator.SetBool("Walk", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BOTTLE")
        {
            Vector3 effectPosition = collision.gameObject.GetComponent<Transform>().position;
            GameObject effect = Instantiate(effectObject, effectPosition, Quaternion.identity);
            Destroy(effect, 2.0f);
            Destroy(collision.gameObject);  //√Êµπ«— ∞‘¿” ø¿∫Í¡ß∆Æ ªË¡¶
            bottle += 1; //»πµÊ«— ƒ⁄¿Œ¿« ∞πºˆ 1∞≥ √ﬂ∞°
            Debug.Log("»πµÊ«— ƒ⁄¿Œ ºˆ : " + bottle.ToString());
            audioSource.PlayOneShot(audioClip);
            text.text = "ƒ⁄¿Œ " + bottle + "/50∞≥";
        }
        if(collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }
}
