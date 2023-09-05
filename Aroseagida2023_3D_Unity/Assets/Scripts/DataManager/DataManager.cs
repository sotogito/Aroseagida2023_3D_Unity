using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

#region DataManager.cs 주요 기능
/*
    1. 저장할 한묶음의 데이터들을 모아둠
    2. 저장하기
    3. 불러오기
    4. 데이터클리어
*/
#endregion

namespace Letter
{
    public class PlayerData
    {
        public bool IsDrawn;
        public string LetterText;
        public int LetterNum;
        public List<DrawnInfo> DrawnLines;
    }

    public class DataManager : MonoBehaviour
    {
        #region 인스턴스 및 변수

        public static DataManager instance; 
        public PlayerData nowPlayer = new PlayerData(); 


        //데이터가 저장된 경로 = path+nowSlot
        public string path;
        public int nowSlot;

        public bool GameStart;
        #endregion


        private void Awake()
        {
            #region 싱글톤
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(instance.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
            #endregion

            path = Application.persistentDataPath + "/AROSEAGIDA SAVE";
            print(path);
        }

        
        #region SaveData(), LoadData(), DataClear() - 저장하기,불러오기,데이터클리어
        public void SaveData() //저장하기 함수
        {
            string data = JsonUtility.ToJson(nowPlayer);
            File.WriteAllText(path+ nowSlot.ToString(), data); 
        }
        public void LoadData() //불러오기 함수
        {
            string data = File.ReadAllText(path + nowSlot.ToString());
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        }

        public void DataClear() //데이터클리어
        {
            nowSlot = -1;
            nowPlayer = new PlayerData();
        }
        #endregion


        #region test() [그림그리기]가 선택될 때 출력됨
        public void test()
        {
            print("");
            print("");
            print("DataManager로 넘어간 데이터를 출력합니다");
            print("id : "+nowPlayer.LetterNum);
            print("Letter : "+nowPlayer.LetterText);
            print("nowSlot : "+nowSlot);
            print("");
            print("");
        }
        #endregion

        
    
    }

}
