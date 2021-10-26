using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public EnemyManager enemyManager;

    public enum type { boss, normalEnemy,barrage,paringBarrage};//순서대로 보스, 일반몬스터, 탄막, 패링탄막

    public type enemyType;

    public int hp = 999;


    public void Hpdonw(int damage,GameObject hit)//2번째 인자는 만약 맞은 탄환도 삭제시키기 위한 예비용
    {
        if(hp == 999)//체력이 999일경우 죽지 않는 적 오브젝트이다.
        {
            return;
        }
        hp -= damage;//체력 감소
        if (hp < 1)
        {
            if (this.gameObject.CompareTag("boss"))
            {
                GameManager.gameManager.GameOver.Invoke();
            }
            DestroyObject(this.gameObject);
        }

    }

    //적 오브젝트 삭제
    public void DestroyObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    /*
     * 사용법
     * 이동할 gameObject.GetComponent<Enemy>().사용할 함수 이름(인자들);
     * 
     * 
     */

    //직선으로 이동
    public void MoveEnemy(Vector3 endPoint, float time)
    {
        Vector3 startPoint = this.gameObject.transform.position;


        StartCoroutine(MoveEnemyCor(startPoint, endPoint, time));

    }
    //직선 이동 코루틴
    IEnumerator MoveEnemyCor(Vector3 startPoint, Vector3 endPoint,float time)
    {
        for(int i = 0; i <= time; i++)
        {
            this.gameObject.transform.position = Vector3.Lerp(startPoint, endPoint, i / time);
            yield return new WaitForFixedUpdate();
        }


        yield return 0;
    }
    //크기를 조절하는 함수
    public void ScaleEnemy(Vector3 endScale , float time)
    {
        Vector3 startScale = this.transform.localScale;
        StartCoroutine(ScaleEnemyCor( startScale, endScale, time));
    }
    //크기를 조절하는 코루틴
   IEnumerator ScaleEnemyCor(Vector3 startScale, Vector3 endScale, float time)
    {
        for(int i = 0; i <= time; i++)
        {
            this.gameObject.transform.localScale = Vector3.Lerp(startScale, endScale, i / time);//직선 보간
            yield return new WaitForFixedUpdate();
        }

        yield return 0;
    }
    //휘어지는 탄환을 발사하는 함수
    public void reflex(Vector3 endPoint ,float time)
    {
        Vector3 startPoint = this.transform.position;
        StartCoroutine(TrackingPlayerCor(startPoint,endPoint, time));
    }
    //휘어지는 탄환을 발사하는 코루틴
    IEnumerator TrackingPlayerCor(Vector3 startPoint,Vector3 endPoint, float time)
    {
       


        for (int i = 1; i <= time; i++)
        {
           
            this.gameObject.transform.position = Vector3.Slerp(startPoint, endPoint, i/time);
                //startPoint = this.gameObject.transform.localScale;
           
           
            yield return new WaitForFixedUpdate();
        }
        

        yield return 0;
    }

    //s모형으로 위아래 이동하는 방식
    public void Uturn(Vector3 endPoint, float time, float moveRound)
    {
        Vector3 startPoint = this.transform.position;
        StartCoroutine(UturnCor(startPoint, endPoint, time, moveRound));
    }

    IEnumerator UturnCor(Vector3 startPoint, Vector3 endPoint, float time,float moveRound)
    {
      
        for(int i = 0; i <= time; i++)
        {


            
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(startPoint.x, endPoint.x, i / time)
                , startPoint.y + (endPoint.y-startPoint.y)* Mathf.Sin(i*Time.deltaTime)//y시작위치에서 moveRound 까지 왕복한다.
                , 0);

            yield return new WaitForFixedUpdate();
        }
        this.gameObject.transform.position = endPoint;
        yield return 0;
    }



}
