using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startposition = new Vector3(840.48f, 0.45f, 259.48f);
    public bool stop = false;
    public bool nomal = false;
    Vector3 force = new Vector3(0.0f, 0.0f, 50.0f);    // 力を設定
    Vector3 back = new Vector3(0.0f, 0.0f, -50.0f);    // 力を設定
    [SerializeField] GameObject Light;
    [SerializeField] GameObject Dark;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject DemoClearPanel;
    [SerializeField] Text Traptext;
    [SerializeField] Text Gettext;
    public bool notTurned = true;
    public bool LightOn = false;
    float cooltime = 0;
    float losttime = 0;
    [SerializeField] AudioClip sound1;//落ちた時のサウンド
    [SerializeField] AudioClip sound2;//開いたときのサウンド
    AudioSource audioSource;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(cooltime);
        Debug.Log(losttime);
        if (Input.GetKeyDown("w") && stop == false)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * 270f * Time.deltaTime;
            stop = true;
        }
        if (Input.GetKeyDown("s") && stop == false)
        {
            transform.position += transform.TransformDirection(Vector3.back) * 270f * Time.deltaTime;
            stop = true;
        }
        if (Input.GetKeyDown("a") && stop == false && notTurned == false)
        {
            Transform myTransform = this.transform;
            // ローカル座標を基準に、回転を取得
            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.y -= 90.0f; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.localEulerAngles = localAngle; // 回転角度を設定
            stop = true;
        }
        if (Input.GetKeyDown("d") && stop == false && notTurned == false)
        {
            Transform myTransform = this.transform;
            // ローカル座標を基準に、回転を取得
            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.y += 90.0f; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.localEulerAngles = localAngle; // 回転角度を設定
            stop = true;
        }
        if (stop == true)
        {
            cooltime += Time.deltaTime;
        }
        if (cooltime >= 0.1f)
        {
            cooltime = 0;
            stop = false;
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
        if (other.gameObject.CompareTag("restart"))
        {
            Debug.Log("もう使いません");
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            Traptext.text = "床の底が抜けた!";
            other.gameObject.SetActive (false);
            audioSource.PlayOneShot(sound1);
            Debug.Log("罠を踏んだ！");
        }
        if (other.gameObject.CompareTag("Trap(swich)"))//テキストを後々変えられるようにします。
        {
            Traptext.text = "床が消えるスイッチだ!";
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(sound1);
            Debug.Log("スイッチによって消えた！");
        }
        if (other.gameObject.CompareTag("DemoClear"))
        {
            DemoClearPanel.gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("Key"))//テキストを後々変えられるようにします。
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
