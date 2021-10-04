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
            DestroyObject();
        }

    }

    //적 오브젝트 삭제
    public void DestroyObject()
    {
        DestroyImmediate(this.gameObject);
    }

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

   
    


}
