using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startposition = new Vector3(840.48f, 0.45f, 259.48f);
    public bool stop = false;
    Vector3 force = new Vector3(0.0f, 0.0f, 50.0f);    // �͂�ݒ�
    Vector3 back = new Vector3(0.0f, 0.0f, -50.0f);    // �͂�ݒ�
    [SerializeField] GameObject Light;
    [SerializeField] GameObject Dark;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject DemoClearPanel;
    [SerializeField] Text Traptext;
    public bool notTurned = true;
    public bool LightOn = false;
    float cooltime = 0;
    [SerializeField] AudioClip sound1;//���������̃T�E���h
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(cooltime);
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
            // ���[�J�����W����ɁA��]���擾
            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.y -= 90.0f; // ���[�J�����W����ɁAy�������ɂ�����]��10�x�ɕύX
            myTransform.localEulerAngles = localAngle; // ��]�p�x��ݒ�
            stop = true;
        }
        if (Input.GetKeyDown("d") && stop == false && notTurned == false)
        {
            Transform myTransform = this.transform;
            // ���[�J�����W����ɁA��]���擾
            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.y += 90.0f; // ���[�J�����W����ɁAy�������ɂ�����]��10�x�ɕύX
            myTransform.localEulerAngles = localAngle; // ��]�p�x��ݒ�
            stop = true;
        }
        if (stop == true)
        {
            cooltime += Time.deltaTime;
        }
        if (cooltime <= 0.1)
        {
            cooltime = 0;
            stop = false;
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
            Debug.Log("���e���|�[�^�[���Q�[���I�[�o�[�g���K�[");
        }
        if (other.gameObject.CompareTag("Dark"))
        {
            LightOn = true;
            Debug.Log("�È�");
        }
        if (other.gameObject.CompareTag("Light"))
        {
            LightOn = false;
            Debug.Log("���邳���߂����I");
        }
        if (other.gameObject.CompareTag("restart"))
        {
            Debug.Log("�����g���܂���");
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            Traptext.text = "���̒ꂪ������!";
            other.gameObject.SetActive (false);
            audioSource.PlayOneShot(sound1);
            Debug.Log("㩂𓥂񂾁I");
        }
        if (other.gameObject.CompareTag("Trap(swich)"))//�e�L�X�g����X�ς�����悤�ɂ��܂��B
        {
            Traptext.text = "����������X�C�b�`��!";
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(sound1);
            Debug.Log("�X�C�b�`�ɂ���ď������I");
        }
        if (other.gameObject.CompareTag("DemoClear"))
        {
            DemoClearPanel.gameObject.SetActive(true);
        }
    }

    public void Turned()
    {
        notTurned = false;
    }
}
