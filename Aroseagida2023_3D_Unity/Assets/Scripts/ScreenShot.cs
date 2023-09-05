using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ScreenShot.cs의 주요기능
/*
    1. 캡쳐하고 사진을 경로로 저장
*/
#endregion

namespace Letter
{

    public class ScreenShot : MonoBehaviour
    {
        //스크린 샷을 찍을동안은 잠시 펜이 사라짐
        public GameObject playerLeftHand;
        public GameObject playerRightHand;

        public GameObject playerPenPoint;


        
        void Start()
        {  
            //이미지 초기화(첫 시작화면)
            CaptureScreen();
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                playerLeftHand.gameObject.SetActive(false);
                playerRightHand.gameObject.SetActive(false);
                CaptureScreen();
            }
            if (Input.GetKeyUp(KeyCode.T))
            {
                playerLeftHand.gameObject.SetActive(true);
                playerRightHand.gameObject.SetActive(true);
            }
        }

        void CaptureScreen() //캡쳐함수
        {
            int NowSlot = DataManager.instance.nowSlot;
            //string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName ="_Screenshot_" + NowSlot + ".png";
            string filePath = Application.persistentDataPath +  "/AROSEAGIDA SAVE" + fileName;
    
            ScreenCapture.CaptureScreenshot(filePath);
        }

    }

}
