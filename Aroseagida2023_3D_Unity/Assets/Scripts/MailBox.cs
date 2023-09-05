using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region MailBox.cs의 주요기능
/*
    1. [편지보기] & [종료하기] 버튼
    2. [종료하기] ->  게임 종료하기
*/
#endregion

public class MailBox : MonoBehaviour
{
    public Canvas Main_Mailbox_Ui;
    public Canvas Letter_List_Ui;

    void Start()
    {
        Main_Mailbox_Ui.gameObject.SetActive(false);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           Main_Mailbox_Ui.gameObject.SetActive(true);
        }
            
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Main_Mailbox_Ui.gameObject.SetActive(false);
            Letter_List_Ui.gameObject.SetActive(false);
        }
    }

    public void QuitGame() //[게임 종료하기] 버튼 클릭 시
    {
            Application.Quit();
            print("종료합니다");
    }
    
    
  
}


