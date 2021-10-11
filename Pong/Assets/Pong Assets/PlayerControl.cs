using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Keycode arahan
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;

    // Kecepatan
    public float speed = 10.0f;

    // Batasan atas dan bawah (yang bawah pake -)

    public float boundary = 9.0f;

    // Rigidbody 2d raket
    private Rigidbody2D rb;

    // Skor pemain
    private int skor;

    // Titik tumbukan terakhir dgn bola
    private ContactPoint2D titikterakhir;

    // untuk akses info titik kontak dari kelas lain

    public ContactPoint2D titikterakhirs
    {
        get { return titikterakhir;}
    }

    // apabila terjadi tumbukan dgn bola, rekam titiknya
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Ball"))
        titikterakhir = collision.GetContact(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // dapatkan kecepatan raket
        Vector2 v = rb.velocity;

        // if up, v++ (koordinat y pun ke atas)
        if(Input.GetKey(up)) v.y = speed;

        // if down, v-- (koordinat y pun ke bawah)
        else if(Input.GetKey(down)) v.y = -speed;

        // ga pencet apa"
        else v.y = 0.0f;

        // transfer kecepatan ke rigidbody2D nya.
        rb.velocity = v;

        // Bagian boundary
        Vector3 posisi = transform.position;

        // apabila posisi raket lewat boundary bagian atas, tahan
        if(posisi.y > boundary) posisi.y = boundary;

        // apabila lewat boundary bagian bawah, tahan
        else if(posisi.y < -boundary) posisi.y = -boundary;

        // transfer posisi ke dalam transform
        transform.position = posisi;


          

    }
    // Manajemen Skor
     public void naikinskor()
        {
            skor++;
        }

        public void resetskor()
        {
            skor = 0;
        }

        public int hasil
        {
            get {return skor;}
        }
}
