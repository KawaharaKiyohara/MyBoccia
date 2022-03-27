using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class PlayerBallScript : IBallScript
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ・ｽ{・ｽ[・ｽ・ｽ・ｽ・ｽ・ｽG・ｽ・ｽ・ｽA・ｽ・ｽﾉ難ｿｽ・ｽ・ｽ・ｽ・ｽ・ｽ・ｽ・ｽﾌ擾ｿｽ・ｽ・ｽ
    /// </summary>
    public override void InsideVenue()
    {
        InArea = true;
    }

    /// <summary>
    /// ・ｽ{・ｽ[・ｽ・ｽ・ｽ・ｽ・ｽG・ｽ・ｽ・ｽA・ｽO・ｽﾉ出・ｽ・ｽ・ｽ・ｽ・ｽﾌ擾ｿｽ・ｽ・ｽ
    /// </summary>
    public override void OutsideVenue()
    {
        InArea = false;
    }

    /// <summary>
    /// ・ｽ{・ｽ[・ｽ・ｽ・ｽ・ｽ・ｽ・ｽ~・ｽ・ｽ・ｽ・ｽ・ｽﾆゑｿｽ・ｽﾌ擾ｿｽ・ｽ・ｽ
    /// </summary>
    public override void EndThrow()
    {
        IsThrowing = false;
        if (InArea == false)
        {

            this.gameObject.GetComponent<BallStateScript>().ResetState();
            this.gameObject.SetActive(false);
        }
    }
}
