// --------------------------------------------------------- 
// test.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
using DG.Tweening;
public class test : MonoBehaviour
{

    [SerializeField]
    Animator anim;
 #region variable 
 #endregion
 #region property
 #endregion
 #region method
 
 private void Awake()
 {

 }
 
 private void Start ()
 {
    //transform.DOMoveX(2.4f, 1);

        anim.SetTrigger("move");
 }

 private void Update ()
 {

 }
 #endregion
}