using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Letter
{
    public class RandomLetterList : MonoBehaviour
    {
        #region Option
        public List<string> ageOptions = new List<string>
		{
			"지금까지","최근에" //사용자 나이를 받아 추가
		};
        private string[] whenOptions1 = new string[]
		{
			"봄","여름","가을","겨울","크리스마스", "추석","설날", "새해","생일",
			"어버이날","어린이날"
			//"삼일절","현충일","광복절","한글날",
		};
        private string[] whenOptions2 = new string[]
		{
			"결혼식", "휴가", "생일파티","가족모임","여행","산책",
			"동호회","영화","뮤지컬","월드컵", "친구들과의 여행","가족들과의 여행"
		};
        private string[] whoOptions = new string[]
		{
		    "가족","부모님","어머니","아버지","친구","배우자","자녀","아들","딸","손자","손녀"
		};
        private string[] whereOptions = new string[]
		{
			"집","회사","공원","학교","바다","산","정원","꽃밭","숲","동굴","산책로",
			"벚꽃이 만개한 곳","파도가 일렁이던 곳","별이 보이는 곳"
		};
        #endregion


        #region level에 따른 RandomLevel, OptionRange, RandomOption 지정
        public int randomOption;
        public int randomLevel;
        public void QuestionLevel (int qtype, int level)
        {
            randomOption = 0;
            randomLevel = 0;

            if(qtype == 0)
            {
                if(level <=2)
                {
                    randomLevel = 0;
                }
                else
                {
                    randomLevel = UnityEngine.Random.Range(0,2);
                }
            }

            if(qtype == 1)
            {
                randomLevel = UnityEngine.Random.Range(0,level);

                int ageOptionsRange = ageOptions.Count;
                int whenOptions1Range = whenOptions1.Length;
                int whenOptions2Range = whenOptions2.Length;
                int whoOptionsRange = whoOptions.Length;
                int whereOptionsRange = whereOptions.Length;

                //level에 따라서 option Random 범위 조정 - 난이도 조정
                if(level == 1)
                {
                    ageOptionsRange = (int)(ageOptions.Count * 0.2);
                }
                else if(level == 2)
                {
                
                    ageOptionsRange = (int)(ageOptions.Count * 0.4);
                    whenOptions1Range = (int)(whenOptions1.Length * 0.2);
                }
                else if(level == 3)
                {
                    ageOptionsRange = (int)(ageOptions.Count * 0.8);
                    whenOptions1Range = (int)(whenOptions1.Length * 0.5);
                    whoOptionsRange = (int)(whoOptions.Length * 0.5);
                }
                else if(level == 4)
                {
                    whenOptions1Range = (int)(whenOptions1.Length * 0.8);
                    whoOptionsRange = (int)(whoOptions.Length * 0.8);
                    whenOptions2Range = (int)(whenOptions2.Length * 0.5);
                }
                else if(level == 5)
                {

                }

                //Level의 랜덤값에 따라서 RandomQuestion의 option index값 지정 
                if(randomLevel == 0)
                {
                    randomOption = UnityEngine.Random.Range(0,ageOptionsRange);
                }
                else if(randomLevel == 1)
                {
                    randomOption = UnityEngine.Random.Range(0,whenOptions1Range);
                }
                else if(randomLevel == 2)
                {
                    randomOption = UnityEngine.Random.Range(0,whoOptionsRange);
                }
                else if(randomLevel == 3)
                {
                    randomOption = UnityEngine.Random.Range(0,whenOptions2Range);
                }
                else if(randomLevel == 4)
                {
                    randomOption = UnityEngine.Random.Range(0,whereOptionsRange);
                }
            }
        }
        #endregion


        #region 지정된 함수 4개에 따른 Question 생성
        public string[] question;
        public int randomQuestion;
        public string resultQuestion;
        public void Question (int qtype, int rLevel, int rQuestion, int rOption)
        {
            #region QuestionType 0 : option이 없는 질문 
            if(qtype == 0)
            {
                if(rLevel == 0) 
                {
                    question = new string[]
                    {
                        "가장 최근에 꾼 꿈이 무엇인가요?",
                        "가장 최근 웃었던 적은 언제인가요?",
                        "가장 최근 울었던 적은 언제인가요?",
                        "최근에 가장 맛있었던 식사는 언제인가요?",
                        "가장 인상적인 꾼 꿈이 무엇인가요?",
                        "숨겨진 재능이나 열정을 발견했던 때 기억나나요?",
                        "당신의 나이로 돌아간 부모님에게 드리고싶은 선물 있나요?",
                        "당신의 나이로 돌아간 부모님과 같이 가고 싶은 곳 있나요?",
                        "가장 사랑하는 사람에게 준 선물 있나요?",
                        "부모가 되었다는 것을 처음 알았을 때 기억나나요?",
                        "사랑하는 자식을 처음 마주했을 때 기억 나나요?",
                        "나의 마지막 사랑은 누구인가요?",
                        "낮선 사람으로부터 예상치 못한 친절을 받았던 때가 있나요?",
                        "당신의 나이로 돌아간 부모님에게 드리고싶은 선물 있나요?",
                        "당신의 나이로 돌아간 부모님과 같이 가고 싶은 곳 있나요?",
                        "가장 사랑하는 사람에게 준 선물 있나요?",
                        "부모가 되었다는 것을 처음 알았을 때 기억나나요?",
                        "사랑하는 자식을 처음 마주했을 때 기억 나나요?",
                        "나의 마지막 사랑은 누구인가요?",
                    };
                }
                else if(rLevel == 1) 
                {
                    question = new string[]
                    {
                        "첫사랑에게 주고싶은 선물은 무엇인가요?",
                        "어렸을 때 가장 좋아하는 장난감은 무엇이였나요?",
                        "어렸을 때 별명이 있었나요?",
                        "어렸을 때 가장 좋아했던 과목은 무엇인가요?",
                        "어렸을 때의 일기 중 기억나는 이야기가 있나요?",
                        "어린시절 가장 좋아했던 칭찬은 무엇인가요?",
                        "어렸을 때 학교가 끝나고 어떻게 시간을 보냈나요?",
                        "어릴 적 가장 좋아했던 동화나 이야기는 무엇이었나요?",
                        "어릴 적 자주 가던 친구의 집에서 특별한 경험이 있었나요?",
                        "어릴 적 가장 좋아했던 동물이나 반려동물은 무엇이었나요?",
                        "어린시절 꿈이 있었나요?",
                        "어린시절 가장 좋아하는 사람은 누구였나요?",
                        "처음 살던 집 기억나나요?"
                    };
                }
                else{
                    question = new string[]
                    {
                        "오류"
                    };
                }
            }
            #endregion
            
            #region QuestionType 1 : optioin이 있는 질문
            if(qtype == 1)
            {
                if(rLevel == 0) //age
                {
                    question = new string[]
                    {
                        ageOptions[rOption]+" 가장 기억에 남는 순간이 있나요?",
                        ageOptions[rOption]+" 가장 감사했던 순간이 있나요?",
                        ageOptions[rOption]+" 여전히 미소를 짓게하는 재미있었던 순간이 있나요?",
                        ageOptions[rOption]+" 배운 가장 중요한 교훈이 있나요?",
                        ageOptions[rOption]+" 가장 기억에 남는 여행 있나요?",
                        ageOptions[rOption]+" 가장 기억에 남는 장소 있나요?",
                        ageOptions[rOption]+" 존경했던 사람은 누군가요?",
                        ageOptions[rOption]+" 가장 친했던 사람 기억나나요?"
                    };
                }
                else if(rLevel == 1) //when1
                {
                    question = new string[]
                    {
                        "가장 최근의"+whenOptions1[rOption]+"기억나나요?",
                        whenOptions1[rOption]+"에 누구와 보냈나요?",
                        whenOptions1[rOption]+"에 어디서 보냈나요?",
                        whenOptions1[rOption]+"때에 집 풍경은 어떤가요?"
                    };
                }
                else if(rLevel == 2) //who
                {
                    question = new string[]
                    {
                        whoOptions[rOption]+"은(는)) 당신에게 어떤 존재인가요?",
                        whoOptions[rOption]+"와(과) 가장 행복했던 순간은 언제인가요?",
                        whoOptions[rOption]+"에게 감동받았던 순간 있나요?",
                        whoOptions[rOption]+"에게 주고싶은 선물 있나요?"
                    };
                }
                else if(rLevel == 3) //when2
                {
                    question = new string[]
                    {
                        "첫"+whenOptions2[rOption]+" 기억나나요?",
                        "가장 최근의"+whenOptions2[rOption]+"기억나나요?",
                        "가장 기억에 남는 "+whenOptions2[rOption]+" 있나요?"
                    };
                }
                else if(rLevel == 4) //where
                {
                    question = new string[]
                    {
                        "가장 최근에 방문했던"+whereOptions[rOption]+"은 어디인가요?",
                        "가장 기억에 남는"+whereOptions[rOption]+"있나요?",
                        "가장 기억나는 "+whereOptions[rOption]+"주위에 무엇이 있었나요?",
                        "어렸을 때로 돌아간다면"+whereOptions[rOption]+"에 누구를 데려가고 싶나요?"
                    };
                }
                else{
                    question = new string[]
                    {
                        "오류"
                    };
                }
            }
            #endregion

            randomQuestion = UnityEngine.Random.Range(0,question.Length);
            resultQuestion = question[rQuestion];
        }
        #endregion
    }
}