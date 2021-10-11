using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    // Skrip, collider, dan rb bola
    public BallControl bola;
    CircleCollider2D bolacollider;
    Rigidbody2D bolarb;

    // Bola "bayangan" yang akan ditampilkan di titik
    // tumbukan

    public GameObject ballAtCollision;



    // Start is called before the first frame update
    void Start()
    {
        bolarb = bola.GetComponent<Rigidbody2D>();
        bolacollider = bola.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // inisiasi status pantulan lintasan,
        // yg hanya akan ditampilkan jika lintasan
        // bertumbukan dengan objek tertentu.

        bool drawBallAtCollision = false;

        // Titik tumbukan yang digeser, utk menggambar
        // ballAtCollision

        Vector2 offsetHitPoint = new Vector2();

        // Tentukan titik tumbukan dgn deteksi 
        // pergerakan lingkaran

        RaycastHit2D[] circleCastHit2DArray =
        Physics2D.CircleCastAll(bolarb.position, bolacollider.radius,bolarb.velocity.normalized);

        // Untuk setiap titik tumbukan
        foreach(RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            // jika terjadi tumbukan, tumbukan tsb tdk dgn bola
            // (krn garis lintasan digambar dri titik tngh bola)
            if(circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallControl>() == null)
            {

        // Garis lintasan akan digambar dri titik tengah bola saat ini
        // ke titik tengah bola pd saat tumbukan,
        // yaitu sebuah titik yang dioffset dri titik tumbukan
        // berdasar vektor normal titik tersebut sebesar jari" bola

        // Tentukan titik tumbukan
        Vector2 hitPoint = circleCastHit2D.point;

        // Tentukan normal di titik tumbukan
        Vector2 hitNormal = circleCastHit2D.normal;

        // Tentukan offsetHitPoint, yaitu titik tengah
        // bola pada saat bertumbukan
        offsetHitPoint = hitPoint + hitNormal * bolacollider.radius;

        // Gambar garis lintasan dari titik tengah saat ini
        // ke titik tengah bola pada saat tumbukan

        DottedLine.DottedLine.Instance.DrawDottedLine(bola.transform.position,offsetHitPoint);

             // kalau bukan sidewall, gambar pantulannya
             if(circleCastHit2D.collider.GetComponent<SideWall>() == null)
            {
                // Hitung vektor datang
                Vector2 inVector = (offsetHitPoint - bola.trajectoryOrigins).normalized;

                // Hitung vektor keluar
                Vector2 outVector = Vector2.Reflect(inVector,hitNormal);

                // Hitung dot product dari outVector dan hitNormal.
                // Digunakan supaya garis lintasan ketika
                // terjadi tumbukan tidak digambar.

                float outDot = Vector2.Dot(outVector, hitNormal);
                if(outDot > -1.0f && outDot < 1.0)
                {
                    // gambar lintasan pantulannya
                    DottedLine.DottedLine.Instance.DrawDottedLine(
                        offsetHitPoint, offsetHitPoint + outVector * 10.0f
                    );

                    // untuk menggambar bola "bayangan" di prediksi titik
                    // tumbukan

                    drawBallAtCollision = true;
                }
            }

            // Hanya gambar lintasan utk satu titik
            // tumbukan, jadi keluar dari loop

            break;

           
            }

         
        }

           // jika true,
            if(drawBallAtCollision)
            {
                // gambar bola "bayangan" di prediksi titikk tumbukan
                ballAtCollision.transform.position = offsetHitPoint;
                ballAtCollision.SetActive(true);
            }

            else
            {
                // sembunyikan bola "bayangan"
                ballAtCollision.SetActive(false);
            }  

           
    
    }
}
