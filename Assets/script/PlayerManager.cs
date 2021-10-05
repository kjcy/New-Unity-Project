using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 기능 구현 내용
1.플레이어는 상하좌우로 음직인다.
2.플레이어는 0도 45도 90도 135도 180도로 발사한다.(보는 방향 기준으로 0도 45도 90도)
3.플레이어는 적(일반 탄환, 패링탄환, 적 보스)에 충돌하면 체력이 감소한다.
4.패링탄환에 충돌하기전 플레이어가 페링상태가 된다면 데미지를 무시하고 플레이어 능력코스트를 올려준다.
5.플레이어는 발 바로 아래에 바닥 또는 발판이 없다면 아래로 내려간다.
6.발판은 아래에서 위로는 올라갈 수 있다.
7.스페이스를 사용시 특수 능력을 사용
 */


public class PlayerManager : MonoBehaviour
{
    public GameManager gameManager;


    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Sprite[] playerSprite = new Sprite[2];

    private float attackTime;

    private float speed;
    private int hp;
    [SerializeField]
    private Text hpText;
    private float abilityBar;
    [SerializeField]
    private Text abilityBarText;

    private bool downPlayer;
    private bool jumpPlayer;
    private bool parringPlayer;
    public bool ParringPalyer{
      get{ return parringPlayer; }  
    }

    RaycastHit hit;


    private void Start()
    {
        speed = 7.5f;
        Debug.LogFormat("{0}", speed);
        downPlayer = true;
        jumpPlayer = false;
        parringPlayer = false;
        attackTime = 0.75f;
        hp = 5;
        abilityBar = 0;
    }

    private void Update()
    {
        PlayerParring();
        PlayerMove();
        PlayerDown();
        ShotBullet();
        GetabilityBar();
        UpdateUi();
    }

    private void UpdateUi()
    {
        hpText.text = "hp :" + hp;
        abilityBarText.text = "ability : " +Mathf.Round( abilityBar*10)*0.1;
    }

    private bool RaycastHit(bool foothold = true)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2( player.gameObject.transform.position.x, player.gameObject.transform.position.y-0.7f), Vector2.down, 0.1f);
        if(hit.collider != null) {
            if (foothold) { 
                if(hit.transform.CompareTag("floor")|| hit.transform.CompareTag("foothold"))
                {
                    Debug.Log("rayhit2");
                    return true;
                }
            }
            else
            {
                if (hit.transform.CompareTag("foothold"))
                {
                    Debug.Log("rayhit3");
                    return true;
                }
            }
        }
      
        return false ;
    }

    //바닥에 바닥 또는 발판이 없다면 플레이어를 아래로 내리는 장치(중력 같은 역할)
    private void PlayerDown()
    {
        
       //먼가 이상한것 같기도
        if(RaycastHit()) { 
                jumpPlayer = false;
                Debug.Log("stap");
              //바닥이나 발판에 착지한 후 다시 점프할 수 있도록
                downPlayer = false;
            
        }
        else if(!jumpPlayer)//점프상태가 아니라면
        {
            downPlayer = true;
        }

        if (downPlayer)
        {
            Debug.Log("2");
            StartCoroutine(PlayerDownFrame(1));
        }
    }

    //인자로 받은 숫자만큼 내려가는 프레임을 작동합니다.
    IEnumerator PlayerDownFrame(int frame)
    {
        for(int i = 0; i < frame; i++)
        {
            player.transform.position += new Vector3(0, -speed, 0)*Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    //입력값에 따라 플레이어의 음직임을 결정
    private void PlayerMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("left");
            player.transform.position += new Vector3(-speed, 0, 0)*Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("right");
            player.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //점프가 아닐때
            if(!jumpPlayer)
                StartCoroutine(PlayerJump());
        }

     
        if (Input.GetKeyDown(KeyCode.S)) {
            
                if (RaycastHit(false))
                {
                downPlayer = true;
                jumpPlayer = true;//아래로 내려가는 것도 점프이다.
                StartCoroutine(PlayerDownFrame(7));
                }
        }

    }

    //플레이어가 점프를 한다. 점프를 할때 아래로 떨어지는 것을 방지해야함
   IEnumerator PlayerJump()
    {
        downPlayer = false;
        jumpPlayer = true;
        for(int i = 0; i < 16; i++)
        {
            player.transform.position += new Vector3(0, speed*1.6f, 0) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        downPlayer = true;
        
        yield return 0;
    }

    //총알 발사 프로그램
    private void ShotBullet()
    {
        bool inputbullet = false;
        int count = 0;
        attackTime -= Time.deltaTime;
        Quaternion v = Quaternion.identity;
        if (Input.GetKey(KeyCode.LeftArrow))
        { 
                v = Quaternion.Euler(0,0,90);
            inputbullet = true;
            count = 1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
           
                v = Quaternion.Euler(0, 0, 270);
            inputbullet = true;
            count = 2;
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {

            Debug.LogFormat("{0}", v.eulerAngles.z);
            inputbullet = true;
            if (count == 1)
            {
                v = Quaternion.Euler(0, 0, 45);
            }
            else if(count == 2)
            {
                v = Quaternion.Euler(0, 0, 315);
            }
            else
            {
                v = Quaternion.Euler(0, 0, 0);
            }
           
        }
        if (inputbullet&&attackTime<0)
        {
            attackTime = 0.75f;
            GameObject bullettemp = Instantiate(bullet, player.transform.position, v);
          
        }

        


    }


    private void PlayerParring()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //패링이 아닐때
            if (!parringPlayer)
            {
                StartCoroutine(Parring());
            }
        }
    }

    IEnumerator Parring()
    {
        parringPlayer = true;
        player.GetComponent<SpriteRenderer>().sprite = playerSprite[1];
        yield return new WaitForSeconds(1f);
        parringPlayer = false;
        player.GetComponent<SpriteRenderer>().sprite = playerSprite[0];
        yield return 0;
    }

    public void ParringSuccess()
    {
        Debug.Log("패링 성공");
        if(abilityBar <4f)
            abilityBar += 1;
    }

    //시간이 지날때마다 능력 게이지가 차오른다.
    public void GetabilityBar()
    {
        abilityBar += 0.1f * Time.deltaTime;
    }
    //GameManager에서 호출하며 플레이어의 체력을 감소, 체력이 0이하가 된다면 GameManager 에서 게임 오버를 출력한다.
    public bool hpDown(int damage)
    {
        hp -= damage;

        if (hp < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    



}
