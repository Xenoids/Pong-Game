using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Player 1
    public PlayerControl p1;
    private Rigidbody2D p1rb;

    // Player 2
    public PlayerControl p2;
    private Rigidbody2D p2rb;

    // Bola
    public BallControl bola;
    private Rigidbody2D bolarb;
    private CircleCollider2D bolacollider;

    // skor maks
    public int skormax;

    // boolean untuk nampilin debug window
    private bool isDebug = false;

    // Objek untuk menggambar prediksi lintasan bola
    public Trajectory trajectory;

    // Start is called before the first frame update
    private void Start()
    {
        p1rb = p1.GetComponent<Rigidbody2D>();
        p2rb = p1.GetComponent<Rigidbody2D>();
        bolarb = bola.GetComponent<Rigidbody2D>();
        bolacollider = bola.GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // GUI
    void OnGUI()
    {
        
        // tampilkan skor p1 kiri p2 kanan
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20,100,100), "" + p1.hasil);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12,20,100,100),"" + p2.hasil);

        // Tombol restart untuk restart
        if(GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120,53),"RESTART"))
        {
            // kalo ditekan maka reset skor
            p1.resetskor();
            p2.resetskor();

            // restart game
            bola.SendMessage("gamerestart",0.5f,SendMessageOptions.RequireReceiver);
        }

        // Jika pemain 1 menang (skor maks)
        if(p1.hasil == skormax)
        {
            // tampilkan p1 win
            GUI.Label(new Rect(Screen.width / 2 - 150,Screen.height / 2 - 10, 2000,1000),"PLAYER ONE WINS");

            // dan balek ke tengah bola
            bola.SendMessage("resetbola",null,SendMessageOptions.RequireReceiver);
        }

        // Jika p2 menang
        else if(p2.hasil == skormax)
        {
            // p2 win
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000),"PLAYER TWO WINS");

            // kembalikan ke tengah bolanya
            bola.SendMessage("resetbola",null, SendMessageOptions.RequireReceiver);
        }

        // jika isdebug == true, tampilkan
        if(isDebug)
        {

        // nilai warna lama GUI
        Color oldColor = GUI.backgroundColor;

        // Beri warna baru
        GUI.backgroundColor = Color.red;

        // Simpan variabel" fisika yg mau tampil
        float ballmass = bolarb.mass;
        Vector2 ballVelocity = bolarb.velocity;
        float ballspeed = bolarb.velocity.magnitude;
        Vector2 ballmomentum = ballmass * ballVelocity;
        float ballfriction = bolacollider.friction;

        float impulseP1X = p1.titikterakhirs.normalImpulse;
        float impulseP1Y = p1.titikterakhirs.tangentImpulse;
        float impulseP2X = p2.titikterakhirs.normalImpulse;
        float impulseP2Y = p2.titikterakhirs.tangentImpulse;

        // Tentukan debug text-nya
        string debugText =
        "Ball mass = " + ballmass + "\n" +
        "Ball velocity = " + ballVelocity + "\n" +
        "Ball speed = " + ballspeed + "\n" +
        "Ball momentum = " + ballmomentum + "\n" +
        "Ball friction = " + ballfriction + "\n" +
        "Last impulse from player 1 = (" + impulseP1X + ", " + impulseP1Y + ")\n" +
        "Last impulse from player 2 = (" + impulseP2X + ", " + impulseP2Y + ")\n";

        // Tampilkan debug window
        GUIStyle guistyle = new GUIStyle(GUI.skin.textArea);
        guistyle.alignment = TextAnchor.UpperCenter;
        GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height - 200, 400, 110), debugText,guistyle);

        // kembalikan warna lama GUI
        GUI.backgroundColor = oldColor;

        }

        
        // Toggle nilai debug window ketika pemain klik tombol ini
        if(GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 73, 120, 53),"TOGGLE\nDEBUG INFO"))
        {
            isDebug = !isDebug;
            trajectory.enabled = !trajectory.enabled;
        }

       
    }
}
