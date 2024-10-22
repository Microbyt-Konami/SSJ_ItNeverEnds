using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleBehaviour : EnemyBaseBehaviour
{
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _enemyController.StartMove(Vector2.up);
            yield return new WaitForSeconds(3f);
            _enemyController.StopMove();
            _enemyController.Rotate(Vector2.right);
            _enemyController.StartMove(Vector2.down);
            yield return new WaitForSeconds(3f);
            _enemyController.StopMove();
        }
    }
}
