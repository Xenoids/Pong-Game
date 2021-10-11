using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb;

    // gaya untuk mendorong bola
    public float xforce;
    public float yforce;

    // titik asal lintasan bola
    private Vector2 trajectoryOrigin;
   


    // resetball method
    void resetbola()
    {
        // reset posisi
        transform.position = Vector2.zero;

        // reset kecepatan
        rb.velocity = Vector2.zero;
    }

    // pushball method
    void dorongbola()
    {
            // tentukan nilai y dari gaya dorong
            // antara -yforce dan yforce

            // y random untuk menentukan arah random y
          //  float yrandomforce = Random.Range(-yforce,yforce);

            // tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
            float randomarah = Random.Range(0,2);

            // jika randomarah < 1, bola ke kiri
            // jika tidak maka kanan
            
            // gaya gerakin bola 
            if(randomarah < 1.0f)
            rb.AddForce(new Vector2(-xforce,yforce));
            else rb.AddForce(new Vector2(xforce,yforce));
    }

    // restart game method
    void gamerestart()
    {
        // balikin posisi bola
        resetbola();

        // setelah 2 detik, beri gaya ke bola
        Invoke("dorongbola",2);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // mulai game
        gamerestart();
         trajectoryOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ketika bola beranjak dari tumbukan, rekam titiknya
    private void onCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    // mengakses info titik asal lintasan
    public Vector2 trajectoryOrigins {get {return trajectoryOrigin;}}
}
