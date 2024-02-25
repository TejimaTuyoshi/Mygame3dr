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
    [SerializeField] AudioClip sound1;//���������̃T�E���h
    [SerializeField] AudioClip sound2;//�J�����Ƃ��̃T�E���h
    [SerializeField] AudioClip sound3;//�������Ƃ��̃T�E���h
    [SerializeField] AudioClip sound4;//�Ԃ��������̃T�E���h
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
        TextCount.text = String.Format("Time: {0:00.00}", CountTime);// �J�E���g�_�E���^�C���𐮌`���ĕ\��
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
            // ���[�J�����W����ɁA��]���擾
            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.y -= 90.0f; // ���[�J�����W����ɁAy�������ɂ�����]��10�x�ɕύX
            myTransform.localEulerAngles = localAngle; // ��]�p�x��ݒ�
        }
        if (Input.GetKeyDown("d") && stop == false && notTurned == false)
        {
            Transform myTransform = this.transform;
            // ���[�J�����W����ɁA��]���擾
            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.y += 90.0f; // ���[�J�����W����ɁAy�������ɂ�����]��10�x�ɕύX
            myTransform.localEulerAngles = localAngle; // ��]�p�x��ݒ�
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
            Debug.Log("���e���|�[�^�[���Q�[���I�[�o�[�g���K�[");
            if (nottrap)
            {
                Traptext.text = "�����Ȃ��ǂ����蔲����...";
            }
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
        if (other.gameObject.CompareTag("Trap"))
        {
            nottrap = false;
            Traptext.text = "���̒ꂪ������!";
            other.gameObject.SetActive (false);
            audioSource.PlayOneShot(sound1);
            Debug.Log("㩂𓥂񂾁I");
        }
        if (other.gameObject.CompareTag("Trap(swich)"))
        {
            nottrap = false;
            Traptext.text = "����������X�C�b�`��!";
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(sound1);
            Debug.Log("�X�C�b�`�ɂ���ď������I");
        }
        if (other.gameObject.CompareTag("Trap(move)"))
        {
            nottrap = false;
            Traptext.text = "������Q���ɓ������Ă��܂���...";
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(sound1);
            Debug.Log("�U��q�ɂԂ�����...");
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
