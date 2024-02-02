## DotNetProjcet1는 어떤 프로젝트인가요? 🙋 

이번 프로젝트는 asp.net core를 사용해 만든 일기장 서비스입니다! </br>
랜덤으로 주제를 뽑아 주어 일기를 쓰는데 키워드를 참고할 수 있고,</br>
일기를 쓸 수 있는 게시판과 로그인 기능을 포함하고 있는 웹입니다.

#### [랜덤 주제 뽑기 API & 일기장 게시글 CRUD]
![myDiary_crud](https://github.com/ngeetl/DotNetProject1/assets/53422022/0fb8775f-9140-4d60-a5b5-cc27ffb26e04)

#### [회원가입 & 로그인]
![myDiary_login](https://github.com/ngeetl/DotNetProject1/assets/53422022/15447b5d-5a73-4f34-b9ba-33d9ea4f901e)

## DotNetProjcet1는 어떤 기능들이 구현되어 있나요? 
* MVC(Model-View-Controller) 패턴을 사용하여 각 구성 요소 간의 결합도를 낮추었습니다.
* 일기의 주제(Topics)를 랜덤으로 뽑아 주는 Api Controller를 버튼을 만들었습니다.
* ajax를 사용하여 일기의 주제(Topics)를 요청하는 로직을 구현하였습니다.
* ClaimsIdentity 객체를 사용하여, 쿠키 인증 기반 로그인 기능을 구현했습니다.
* 데이터베이스를 연결하여 일기장(게시글)의 CRUD(생성, 조회, 수정, 삭제) 기능을 전체적으로 구현하였습니다.
* 인증된 사용자에게만 자신의 게시글에 대한 전체적인 데이터 조작 권한을 부여하도록 설계하였습니다.
* Dapper를 이용하여 Model을 데이터베이스와 매핑하였습니다.

## 프로젝트를 진행하면서 느낀점은요?
.NET 웹 프로젝트를 통한 백엔드 개발 경험은 MVC 패턴의 강점을 명확히 이해하는 데 도움이 되었습니다.<br/> 
이 패턴의 적용으로 코드 구조화와 분리가 용이해져서 각 기능의 개발과 유지보수가 훨씬 명확하고 효율적으로 이루어졌습니다. <br/> 
태그 헬퍼의 사용으로 데이터 바인딩과 유효성 검사의 로직을 간결하게 만들어, 개발 과정을 간소화할 수 있었습니다.<br/> 
CRUD 기능의 구현 과정에서는 데이터베이스와의 효율적인 상호작용과 쿼리 최적화의 필요성을 배웠으며, 더 나아가 LINQ 학습의 필요성을 느끼게 되는 계기가 되었습니다. <br/> 
프로젝트 배포에 있어서 환경과 보안 구성에 어려움을 느꼈지만, 이는 향후 배포 프로세스 보완에 대한 학습 계획을 수립할 수 있는 기회가 되었습니다.

#### API 저장소 바로가기: https://github.com/ngeetl/DotNetProject1-Api

## 기술 스택
  <img src="https://img.shields.io/badge/html5-E34F26?style=for-the-badge&logo=html5&logoColor=white"> <img src="https://img.shields.io/badge/css-1572B6?style=for-the-badge&logo=css3&logoColor=white"> 
  <img src="https://img.shields.io/badge/javascript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black"> <img src="https://img.shields.io/badge/mariaDB-003545?style=for-the-badge&logo=mariaDB&logoColor=white">
  <img src="https://img.shields.io/badge/bootstrap-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white"> <img src="https://img.shields.io/badge/dotnet-4053D6?style=for-the-badge&logo=dotnet&logoColor=white">
  <img src="https://img.shields.io/badge/axios-A86454?style=for-the-badge&logo=axios&logoColor=white">


