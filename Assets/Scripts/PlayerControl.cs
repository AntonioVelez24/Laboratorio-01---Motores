using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float xDirection;
    private float yDirection;

    private SpriteRenderer mySpriteRenderer;

    [SerializeField] private float speed;

    [SerializeField] private LayerMask gameLayer;

    private GameObject lastHitObject = null;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        xDirection = Input.GetAxis("Horizontal");
        yDirection = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xDirection, yDirection, 0) * speed * Time.deltaTime;

        transform.position += movement;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement.normalized * 2, 2f, gameLayer);

        Debug.DrawRay(transform.position, movement.normalized * 2, Color.red);

        if (hit.collider != null && hit.collider.gameObject != lastHitObject) 
        {
            if(hit.collider.gameObject.tag != "Player")
            {
                Debug.Log("Colisionando con: " + hit.collider.gameObject.name);
                Debug.Log("Posición: " + hit.collider.transform.position);
                Debug.Log("Tag: " + hit.collider.tag);

                lastHitObject = hit.collider.gameObject;
            }           
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shape")
        {
            Debug.Log("Cambiando la forma del jugador");
            SpriteRenderer objectRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (objectRenderer != null)
            {
                mySpriteRenderer.sprite = objectRenderer.sprite;
                transform.localScale = collision.transform.localScale;
            }
        }
        if (collision.tag == "Color" )
        {
            Debug.Log("Cambiando el Color del jugador");
            SpriteRenderer objectRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (objectRenderer != null)
            {
                mySpriteRenderer.color = objectRenderer.color;
            }
        }
    }
}
