using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Saelogi : MonoBehaviour
{

    #region 변수
    Rigidbody rigidbody;
    BoxCollider boxCollider;

    //캐릭터가 따라다니는 목표물 = player
    public Transform target;
    NavMeshAgent nav;
    Animator anim;
    public bool isChase;

        //새록이 ui
    public GameObject sealogi;
    public Canvas sealogiUI;
    public Text sealogiText;

    //새록이 ui를 사용자 방향으로 띄우기 위한 변수
    public Transform head;
    public string[] speak;
    int randomNum;
    #endregion

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        Invoke("ChaseStart", 2);
    }

    void Start()
    {
        sealogiUI.gameObject.SetActive(false);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk",true);
    }

    void Update()
    {
        if(isChase)
        {
            nav.SetDestination(target.position);
        }
    }
    
    void FreezeVelocity()
    {
        if(isChase)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

    }
    void FixedUpdate()
    {
        FreezeVelocity();
    }


    #region 새록이 UI
    //사용자와 가까워지면 ui를 띄움
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           sealogiUI.gameObject.SetActive(true);
           sealogiUI.transform.LookAt(new Vector3(head.position.x, sealogiUI.transform.position.y, head.position.z));
           speaking();
           Invoke("DeactivateUI", 5.0f);
        }   
    }
    void DeactivateUI()
    {
        sealogiUI.gameObject.SetActive(false);
    }

    void speaking()
    {
        speak = new string[]
        {
            "오늘 하루 어땠어?",
            "또 보네!",
            "나는 바다가 좋아? 산이 좋아?",
            "그림 실력이 늘었는걸!",
            "너와 같이 그림 그리고 싶어",
            "색감이 정말 이뻐",
            "너의 자유로운 선을 봐!",
            "나도 그림을 그리러 가야겠어...",
            "밥은 먹었어?",
            "오늘 날씨 어떄?",
            "오늘 그림 기대할게!",
            "가장 좋아하는 색이 뭐야?",
            "너의 색감 절망 좋아",
            "너의 그림 집에 걸어두고 싶어!",
            "멈추지 않는 한 속도는 중요치 않아",
            "가장 좋아하는 그림이 뭐야?",
            "전시장에 걸려있는 첫번째 그림 마음에 들어!",
            "두번째 그림 멋진걸?",
            "오늘은 어떤 그림을 그릴거야?",
            "오른은 어떤 그림을 그렸어?",
            "인생은 가능성으로 가득 차 있어",
            "나는 넘어지면 다시 일어서",
            "새벽의 빛과 어울리는 너믜 그림!",
            "매일 행복할 순 없지만, 행복한 것들은 매일 있어",
            "오늘은 참 행복한 날이야",
            "기적에도 시간은 조금 걸리기 마련이야",
            "너의 그림을 사랑해!",
            "난 너를 사랑해!"
        };

        randomNum = UnityEngine.Random.Range(0,speak.Length);
        sealogiText.text = speak[randomNum];
    }
    #endregion

    
}

