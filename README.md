# SP_Slot 
🎮 프로젝트 소개

이 프로젝트는 Unity로 개발된 인벤토리 예제입니다.

🕹️ 요소 및 기능

메인 메뉴 : 기본 능력치를 보여주며 선택지를 제공하며 스테이터스창 , 인벤토리 창, 아이템 획득 기능과 연결 됩니다
 - 기본 능력치 : 직업, 이름, 레벨, 경험치, 소지금이 표기됩니다

스테이터스 : 캐릭터의 스텟을 표시하고 장비한 장비만큼의 능력치가 수치로 표기된다

인벤토리 : 각 슬롯별로 아이템과 그 갯수가 저장된다. 슬롯을 누르면 사용 팝업이 생성된다.
 - 소비품 : 사용 버튼을 누르면 플레이어의 스텟이 증가 되고 아이템이 1개 줄어든다.
 - 장비 : 장착, 해제 버튼을 눌러 아이템을 장착하며 인벤토리에 표시된다. 같은 부위(검,방패)를 누르면 장비를 교환한다.
 - 자원 : 사용 버튼이 없으며 정보만 표기 된다
   
아이템 획득 : 인벤토리에서 아이템을 넣을 수 있는 슬롯을 찾아 각 칸에 최대한 아이템을 자동으로 추가한다.
- 획득 : 해당 예시에서는 사과를 5개 획득하여 넣을 수 있는 슬롯을 찾아 추가된다.


정보 불러오기 : JSON파일을 이용하여  아이템 정보, 캐릭터 스테이터스, 인벤토리 정보를 불러온다.

📸 스크린샷


![image](https://github.com/user-attachments/assets/48842776-d9f1-4a0d-9274-816fdbefd11f)

![image](https://github.com/user-attachments/assets/efec65b6-aece-4d40-ae25-f5866581f943)
![image](https://github.com/user-attachments/assets/f2b4f3af-02aa-4841-b25b-fb3684948808)
![image](https://github.com/user-attachments/assets/175e8a16-aba2-472e-bf34-e922c44fc886)
![image](https://github.com/user-attachments/assets/2350b325-5c54-4a62-ad1f-4a1388aa3c98)
![image](https://github.com/user-attachments/assets/f79ba72e-c5b5-4e6b-9c29-6d353209f5d1)
![image](https://github.com/user-attachments/assets/80cffd02-8a30-4962-8783-bf1c67dc6755)

![image](https://github.com/user-attachments/assets/12d177cb-5a9a-4d2e-916d-0bb19f1e9801)



🔧 개발 정보

엔진: Unity

개발 언어: C#
