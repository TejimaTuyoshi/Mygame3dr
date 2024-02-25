using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public bool stop = false;
    public bool nomal = false;
    [SerializeField] GameObject Light;
    [SerializeField] GameObject Dark;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject DemoClearPanel;
    [SerializeField] Text Traptext;
    [SerializeField] Text Gettext;
    [SerializeField] Text TextCount;
    float CountTime = 0;
    public bool notTurned = true;
    public bool LightOn = false;
    bool nottrap = true;
    float losttime = 0;
    [SerializeField] AudioClip sound1;//落ちた時のサウンド
    [SerializeField] AudioClip sound2;//開いたときのサウンド
    [SerializeField] AudioClip sound3;//歩いたときのサウンド
    [SerializeField] AudioClip sound4;//ぶつかった時のサウンド
    AudioSource audioSource;
    [SerializeField]Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        TextCount.text = String.Format("Time: {0:00.00}", CountTime);// カウントダウンタイムを整形して表示
        if (Input.GetKeyDown("w") && stop == false)
        {
            audioSource.PlayOneShot(sound3);
            transform.position += transform.TransformDirection(Vector3.forward) * 810f * Time.deltaTime;
        }
        if (Input.GetKeyDown("s") && stop == false)
        {
            audioSource.PlayOneShot(sound3);
            transform.position += transform.TransformDirection(Vector3.back) * 810f * Time.deltaTime;
        }
        if (Input.GetKeyDown("a") && stop == false && notTurned == false)
        {
            Transform myTransform = this.transform;
            // ローカル座標を基準に、回転を取得
            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.y -= 90.0f; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.localEulerAngles = localAngle; // 回転角度を設定
        }
        if (Input.GetKeyDown("d") && stop == false && notTurned == false)
        {
            Transform myTransform = this.transform;
            // ローカル座標を基準に、回転を取得
            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.y += 90.0f; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.localEulerAngles = localAngle; // 回転角度を設定
        }
        if (stop == false)
        {
            CountTime += Time.deltaTime;
        }
        if (stop == true)
        {
            TextCount.text = "notime";
        }
        if (nomal == true)
        {
            animator.SetBool("Fade", true);
            losttime += Time.deltaTime;
        }
        else if (nomal == false)
        {
            losttime = 0;
            animator.SetBool("Fade", false);
        }
        if (losttime >= 1.7f)
        {
            nomal = false;
        }
        if (LightOn == true)
        {
            Light.gameObject.SetActive(true);
            Dark.gameObject.SetActive(false);
        }
        else
        {
            Light.gameObject.SetActive(false);
            Dark.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Teleporter"))
        {
            GameOverPanel.gameObject.SetActive(true);
            stop = true;
            notTurned = true;
            Debug.Log("元テレポーター現ゲームオーバートリガー");
            if (nottrap)
            {
                Traptext.text = "見えない壁をすり抜けた...";
            }
        }
        if (other.gameObject.CompareTag("Dark"))
        {
            LightOn = true;
            Debug.Log("暗闇");
        }
        if (other.gameObject.CompareTag("Light"))
        {
            LightOn = false;
            Debug.Log("明るさが戻った！");
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            nottrap = false;
            Traptext.text = "床の底が抜けた!";
            other.gameObject.SetActive (false);
            audioSource.PlayOneShot(sound1);
            Debug.Log("罠を踏んだ！");
        }
        if (other.gameObject.CompareTag("Trap(swich)"))
        {
            nottrap = false;
            Traptext.text = "床が消えるスイッチだ!";
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(sound1);
            Debug.Log("スイッチによって消えた！");
        }
        if (other.gameObject.CompareTag("Trap(move)"))
        {
            nottrap = false;
            Traptext.text = "動く障害物に当たってしまった...";
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(sound1);
            Debug.Log("振り子にぶつかった...");
        }
        if (other.gameObject.CompareTag("DemoClear"))
        {
            DemoClearPanel.gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("Key"))
        {
            nomal = true;
            audioSource.PlayOneShot(sound2);
        }
    }

    public void Turned()
    {
        notTurned = false;
    }
}
