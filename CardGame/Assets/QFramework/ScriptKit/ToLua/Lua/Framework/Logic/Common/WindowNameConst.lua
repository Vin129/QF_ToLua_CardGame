local WindowNameConst = class("WindowNameConst")

function WindowNameConst:ctor()
	self:SetWindowName('GuideView', 'Logic/Common/GuideView')
	self:SetWindowName('WaitingView', 'Logic/Common/WaitingView')
	self:SetWindowName('SplashView', 'Logic/Init/SplashView')
	self:SetWindowName('StartView', 'Logic/Login/StartView')
	self:SetWindowName('MainView', 'Logic/MainTown/MainView')
	-- 结算界面
	self:SetWindowName('SettleView', 'Logic/MainTown/Common/SettleView')
	-- 签到
	self:SetWindowName('SignInView', 'Logic/MainTown/SignIn/SignInView')
	self:SetWindowName('SignItem', 'Logic/MainTown/SignIn/SignItem')
	-- 活动
	self:SetWindowName('ActivityView', 'Logic/MainTown/Activity/ActivityView')
	self:SetWindowName('GetTiliView', 'Logic/MainTown/Activity/GetTiliView')
	self:SetWindowName('LoginAwardView', 'Logic/MainTown/Activity/LoginAwardView')
	self:SetWindowName('LvRewardLimitedView', 'Logic/MainTown/Activity/LvRewardLimitedView')
	self:SetWindowName('ClosedBetaRewardView', 'Logic/MainTown/Activity/ClosedBetaRewardView')
	self:SetWindowName('SevenDaysTaskView', 'Logic/MainTown/Activity/SevenDaysTaskView')
	self:SetWindowName('RechargeReturnView', 'Logic/MainTown/Activity/RechargeReturnView')
	self:SetWindowName('RechargeReturnPop', 'Logic/MainTown/Activity/RechargeReturnPop')
	-- 扫荡界面
	self:SetWindowName('SweepPop', 'Logic/MainTown/Common/PopView/SweepPop')	
	self:SetWindowName('SwipeItem', 'Logic/MainTown/Common/PopView/SwipeItem')	
	-- 弹窗工具
	self:SetWindowName("TipsPop", "Logic/MainTown/Common/PopView/TipsPop")
	self:SetWindowName("PropDescPop", "Logic/MainTown/Common/PopView/PropDescPop")
	self:SetWindowName("NormalPop", "Logic/MainTown/Common/PopView/NormalPop")
	self:SetWindowName("NoBtnPop", "Logic/MainTown/Common/PopView/NoBtnPop")
	self:SetWindowName("OutPutPop", "Logic/MainTown/Common/PopView/OutPutPop")
	self:SetWindowName("ToJumpPanel", "Logic/MainTown/Common/PopView/ToJumpPanel")
	self:SetWindowName("JumpRoadItem", "Logic/MainTown/Common/PopView/JumpRoadItem")
	self:SetWindowName("GainPop", "Logic/MainTown/Common/PopView/GainPop")
	self:SetWindowName("EmailPop", "Logic/MainTown/Common/PopView/EmailPop")
	self:SetWindowName("BuyTiliPop", "Logic/MainTown/Common/PopView/BuyTiliPop")
	self:SetWindowName("BuyPlayerCareerWipePop", "Logic/MainTown/Common/PopView/BuyPlayerCareerWipePop")
	self:SetWindowName("ChoosePop", "Logic/MainTown/Common/PopView/ChoosePop")
	self:SetWindowName("SkillUpgradePop", "Logic/MainTown/Common/PopView/SkillUpgradePop")
	self:SetWindowName("SkillRefreshPop", "Logic/MainTown/Common/PopView/SkillRefreshPop")
	self:SetWindowName("SellPop", "Logic/MainTown/Common/PopView/SellPop")
	self:SetWindowName("BuyGoodsPop", "Logic/MainTown/Common/PopView/BuyGoodsPop")
	self:SetWindowName("SponsorsPop", "Logic/MainTown/Common/PopView/SponsorsPop")
	self:SetWindowName("LackPropPop", "Logic/MainTown/Common/PopView/LackPropPop")
	self:SetWindowName("SelectPackagePop", "Logic/MainTown/Common/PopView/SelectPackagePop")
	self:SetWindowName("RandPackagePop", "Logic/MainTown/Common/PopView/RandPackagePop")
	self:SetWindowName("PlayerDesPop", "Logic/MainTown/Common/PopView/PlayerDesPop")
	self:SetWindowName("ChangeNamePop", "Logic/MainTown/Common/PopView/ChangeNamePop")
	self:SetWindowName("RulePop", "Logic/MainTown/Common/PopView/RulePop")
	self:SetWindowName("ManagerLevelUpPop", "Logic/MainTown/Common/PopView/ManagerLevelUpPop")
	self:SetWindowName("FunctionOpenPop", "Logic/MainTown/Common/PopView/FunctionOpenPop")
	self:SetWindowName("OpenCardPop", "Logic/MainTown/Common/PopView/OpenCardPop")
	self:SetWindowName("GetPlayerNumPop", "Logic/MainTown/Common/PopView/GetPlayerNumPop")
	self:SetWindowName("PhoneBindPop", "Logic/MainTown/Common/PopView/PhoneBindPop")
	-- 方形Item 碎片、道具...
	self:SetWindowName("DebrisItem", "Logic/MainTown/Common/CubeItem/DebrisItem")	
	self:SetWindowName("SpecialItem", "Logic/MainTown/Common/CubeItem/SpecialItem")	
	self:SetWindowName("PropItem", "Logic/MainTown/Common/CubeItem/PropItem")
	-- 资源条
	self:SetWindowName("ResourceBar", "Logic/MainTown/Common/ResourceBar")
	-- 跑马灯
	self:SetWindowName("RollNotice", "Logic/MainTown/Common/RollNotice")
	-- 聊天
	self:SetWindowName("ChatView", "Logic/MainTown/Chat/ChatView")
	self:SetWindowName("ChatItem", "Logic/MainTown/Chat/ChatItem")
	self:SetWindowName("ChatPlayerItem", "Logic/MainTown/Chat/ChatPlayerItem")
	self:SetWindowName("ChatHistoryItem", "Logic/MainTown/Chat/ChatHistoryItem")
	-- 登录
	self:SetWindowName("LoginView", "Logic/Login/LoginView")
	-- 首页
	self:SetWindowName("HomeView", "Logic/MainTown/HomeView")
	self:SetWindowName("PlayerPart", "Logic/MainTown/Home/PlayerPart")
	-- 首页Banner
	self:SetWindowName("MatchBanner", "Logic/MainTown/Home/MatchBanner")
	self:SetWindowName("CareerBanner", "Logic/MainTown/Home/CareerBanner")
	-- 详情
	self:SetWindowName("DetailView", "Logic/MainTown/Detail/DetailView")
	self:SetWindowName("RelationItem", "Logic/MainTown/Detail/RelationItem")
	self:SetWindowName("TacitView", "Logic/MainTown/Detail/TacitView")
	self:SetWindowName("PlayerBigCard", "Logic/MainTown/Detail/PlayerBigCard")
	self:SetWindowName("CardEffectNode", "Logic/MainTown/Detail/CardEffectNode")
	-- 阵容
	self:SetWindowName("FormationView", "Logic/MainTown/Formation/FormationView")
	self:SetWindowName("FormationPage", "Logic/MainTown/Formation/FormationPage")
	self:SetWindowName("TacticsView", "Logic/MainTown/Formation/TacticsView")
	self:SetWindowName("TacticsTipPop", "Logic/MainTown/Formation/TacticsTipPop")
	self:SetWindowName("TacticsToggle", "Logic/MainTown/Formation/TacticsToggle")
	self:SetWindowName("CommonFormationView", "Logic/MainTown/Formation/CommonFormationView")
	self:SetWindowName("DutyPop", "Logic/MainTown/Formation/DutyPop")
	self:SetWindowName("PlayerSmallCard", "Logic/MainTown/Formation/PlayerSmallCard")
	self:SetWindowName("FormationPlayerObj", "Logic/MainTown/Formation/FormationPlayerObj")
	--更衣室
	self:SetWindowName("RoomView", "Logic/MainTown/Room/RoomView")
	self:SetWindowName("RoomFilterPop", "Logic/MainTown/Room/RoomFilterPop")
	self:SetWindowName("BagView", "Logic/MainTown/Room/BagView")
	self:SetWindowName("PlayerItem", "Logic/MainTown/Room/PlayerItem")
	self:SetWindowName("PlayerDebrisItem", "Logic/MainTown/Room/PlayerDebrisItem")	
	self:SetWindowName("PropertyItem", "Logic/MainTown/Room/PropertyItem")
	self:SetWindowName("EquipItem", "Logic/MainTown/Room/EquipItem")
	self:SetWindowName("EquipDebrisItem", "Logic/MainTown/Room/EquipDebrisItem")
	--招募
	self:SetWindowName("RecruitView", "Logic/MainTown/Recruit/RecruitView")
	self:SetWindowName("RecruitStore", "Logic/MainTown/Recruit/RecruitStore")
	self:SetWindowName("RecruitTimeStore", "Logic/MainTown/Recruit/RecruitTimeStore")
	self:SetWindowName("RecruitBanner", "Logic/MainTown/Recruit/RecruitBanner")
	self:SetWindowName("ExhibitionView", "Logic/MainTown/Recruit/ExhibitionView")
	--培养
	self:SetWindowName("TrainView", "Logic/MainTown/Train/TrainView")
	--球员升级
	self:SetWindowName("LeveUpView", "Logic/MainTown/LeveUp/LeveUpView")
	--球员升星
	self:SetWindowName("StarUpView", "Logic/MainTown/StarUp/StarUpView")
	--建队主界面
	self:SetWindowName("CreateClubView", "Logic/Login/CreateClub/CreateClubView")
	--俱乐部信息
	self:SetWindowName("ClubInFo", "Logic/Login/CreateClub/ClubInFo")
	--新手获得
	self:SetWindowName("GainInFo", "Logic/Login/CreateClub/GainInFo")
	-- 缘分激活弹窗
	self:SetWindowName("RelationPop", "Logic/MainTown/Detail/RelationPop")
	--装备
	self:SetWindowName("EquipDetailView", "Logic/MainTown/Equip/EquipDetailView")
	self:SetWindowName("EquipLevelUpView", "Logic/MainTown/Equip/EquipLevelUpView")
	self:SetWindowName("EquipStarUpView", "Logic/MainTown/Equip/EquipStarUpView")
	self:SetWindowName("EquipBigCard", "Logic/MainTown/Equip/EquipBigCard")
	-- 世界赛
	self:SetWindowName("WorldRankView", "Logic/MainTown/WorldRank/WorldRankView")
	self:SetWindowName("WorldRankItem", "Logic/MainTown/WorldRank/WorldRankItem")
	self:SetWindowName("Top10Item", "Logic/MainTown/WorldRank/Top10Item")
	self:SetWindowName("AwardItem", "Logic/MainTown/WorldRank/AwardItem")
	--图鉴
	self:SetWindowName("HandBookView", "Logic/MainTown/HandBook/HandBookView")
	self:SetWindowName("HandBookRankView", "Logic/MainTown/HandBook/HandBookRankView")
	--等级礼包
	self:SetWindowName("LevelRewardsPop", "Logic/MainTown/LevelRewards/LevelRewardsPop")
	--好友
	self:SetWindowName("FriendView", "Logic/MainTown/Friend/FriendView")
	self:SetWindowName("FriendItem", "Logic/MainTown/Friend/FriendItem")
	self:SetWindowName("FriendDetailView", "Logic/MainTown/Friend/FriendDetailView")
	self:SetWindowName("FriendMatchItem", "Logic/MainTown/Friend/FriendMatchItem")
	--巡回赛
	self:SetWindowName("MatchItem", "Logic/MainTown/TourMatch/MatchItem")
	self:SetWindowName("TourMatchView", "Logic/MainTown/TourMatch/TourMatchView")
	self:SetWindowName("WipeItem", "Logic/MainTown/TourMatch/WipeItem")
	self:SetWindowName("ChessView", "Logic/MainTown/TourMatch/ChessView")
	self:SetWindowName("ChessGridItem", "Logic/MainTown/TourMatch/ChessGridItem")	
	--邮件
	self:SetWindowName("EmailView", "Logic/MainTown/Email/EmailView")
	--故事生涯
	self:SetWindowName("StoryCareerView", "Logic/MainTown/StoryCareer/StoryCareerView")
	self:SetWindowName("StoryCateView", "Logic/MainTown/StoryCareer/StoryCateView")
	self:SetWindowName("PlotView", "Logic/MainTown/StoryCareer/PlotView")
	self:SetWindowName("FtItem", "Logic/MainTown/StoryCareer/FtItem")
	--球员生涯
	self:SetWindowName("PlayerCareerView", "Logic/MainTown/PlayerCareer/PlayerCareerView")
	self:SetWindowName("PlayerCareerMapView", "Logic/MainTown/PlayerCareer/PlayerCareerMapView")
	self:SetWindowName("ChapterItem", "Logic/MainTown/PlayerCareer/ChapterItem")
	self:SetWindowName("SectionItem", "Logic/MainTown/PlayerCareer/SectionItem")
	self:SetWindowName("PlayerCareerMatchPop", "Logic/MainTown/PlayerCareer/PlayerCareerMatchPop")
	--联赛
	self:SetWindowName("MatchView", "Logic/MainTown/Match/MatchView")
	self:SetWindowName("LeagueDetailView", "Logic/MainTown/Match/LeagueDetailView")
	self:SetWindowName("LeagueScorePanel", "Logic/MainTown/Match/LeagueScorePanel")
	self:SetWindowName("LeagueScheduPanel", "Logic/MainTown/Match/LeagueScheduPanel")
	self:SetWindowName("MatchPlayerPanel", "Logic/MainTown/Match/MatchPlayerPanel")
	self:SetWindowName("LeagueAwardView", "Logic/MainTown/Match/LeagueAwardView")
	--联赛杯
	self:SetWindowName("CupDetailView", "Logic/MainTown/Match/CupDetailView")
	self:SetWindowName("CupZigePanel", "Logic/MainTown/Match/CupZigePanel")
	self:SetWindowName("CupYuxuanPanel", "Logic/MainTown/Match/CupYuxuanPanel")
	self:SetWindowName("CupTaotaiPanel", "Logic/MainTown/Match/CupTaotaiPanel")
	self:SetWindowName("CupJuesaiPanel", "Logic/MainTown/Match/CupJuesaiPanel")

	self:SetWindowName("MatchHonorView", "Logic/MainTown/Match/MatchHonorView")
	self:SetWindowName("CupAwardView", "Logic/MainTown/Match/CupAwardView")
	self:SetWindowName("AgainstView", "Logic/MainTown/Match/AgainstView")

	--商城
	self:SetWindowName("StoreMainView", "Logic/MainTown/Store/StoreMainView")
	self:SetWindowName("StoreView", "Logic/MainTown/Store/StoreView")
	--回收
	self:SetWindowName("RecycleView", "Logic/MainTown/Recycle/RecycleView")	
	self:SetWindowName("RecySubView", "Logic/MainTown/Recycle/RecySubView")	
	self:SetWindowName("RebornSubView", "Logic/MainTown/Recycle/RebornSubView")	
	--任务
	self:SetWindowName("MissionView", "Logic/MainTown/Mission/MissionView")	
	self:SetWindowName("MissionItem", "Logic/MainTown/Mission/MissionItem")	
	self:SetWindowName("ActRewardPop", "Logic/MainTown/Mission/ActRewardPop")
	--传奇挑战赛
	self:SetWindowName("LegRankPop", "Logic/MainTown/LegendChalleng/LegRankPop")
	self:SetWindowName("LegPlayerItem", "Logic/MainTown/LegendChalleng/LegPlayerItem")
	self:SetWindowName("LegClgMatchView", "Logic/MainTown/LegendChalleng/LegClgMatchView")		
	self:SetWindowName("LegendChallengView", "Logic/MainTown/LegendChalleng/LegendChallengView")		
	--训练赛
	self:SetWindowName("TrainMatchView", "Logic/MainTown/TrainMatch/TrainMatchView")
	
	--竞技
	self:SetWindowName("FightMainView", "Logic/MainTown/Fight/FightMainView")			
	--广告媒体
	self:SetWindowName("AdvItem", "Logic/MainTown/AdvMedia/AdvItem")		
	self:SetWindowName("AwardPanel", "Logic/MainTown/AdvMedia/AwardPanel")		
	self:SetWindowName("AdvMediaView", "Logic/MainTown/AdvMedia/AdvMediaView")		
	self:SetWindowName("AdvMediaShootView", "Logic/MainTown/AdvMedia/AdvMediaShootView")
	-- 充值
	self:SetWindowName("VipPanel", "Logic/MainTown/Recharge/VipPanel")
	self:SetWindowName("RechargeItem", "Logic/MainTown/Recharge/RechargeItem")			
	self:SetWindowName("RechargeView", "Logic/MainTown/Recharge/RechargeView")		
	self:SetWindowName("FirstRechargeView", "Logic/MainTown/Recharge/FirstRechargeView")			
	self:SetWindowName("RechargePop", "Logic/MainTown/Recharge/RechargePop")			
	-- 通用他人信息展示Item
	self:SetWindowName("AbsPlayerInfoMod", "Logic/MainTown/Chat/AbsPlayerInfoMod")
	--VIP特权
	self:SetWindowName("VipView", "Logic/MainTown/Vip/VipView")
	self:SetWindowName("VipInfoItem", "Logic/MainTown/Vip/VipInfoItem")

	--排行榜界面
	self:SetWindowName("RankView", "Logic/MainTown/Rank/RankView")
	self:SetWindowName("RankCell", "Logic/MainTown/Rank/RankCell")

	--经理人详情
	self:SetWindowName("ManagerView", "Logic/MainTown/Manager/ManagerView")
	self:SetWindowName("PackagePop", "Logic/MainTown/Manager/PackagePop")

	--公告
	self:SetWindowName("AnnounceView", "Logic/MainTown/Manager/AnnounceView")
	--球员强化
	self:SetWindowName("PlayerIntensifyView","Logic/MainTown/PlayerIntensify/PlayerIntensifyView")
	self:SetWindowName("PlayerIntensifyConfirmPop","Logic/MainTown/PlayerIntensify/PlayerIntensifyConfirmPop")
	self:SetWindowName("PlayerIntensifyResultPop","Logic/MainTown/PlayerIntensify/PlayerIntensifyResultPop")
	--比赛引擎
	self:SetWindowName("CommObj", "Logic/MainTown/MatchEngine/CommObj")
	self:SetWindowName("MatchPrepareView", "Logic/MainTown/MatchEngine/MatchPrepareView")
	self:SetWindowName("PrepareFormPanel", "Logic/MainTown/MatchEngine/PrepareFormPanel")
	self:SetWindowName("EngineAboutView", "Logic/MainTown/MatchEngine/EngineAboutView")
	self:SetWindowName("EngineBothFormPlate", "Logic/MainTown/MatchEngine/EngineBothFormPlate")
	self:SetWindowName("EngineMainView", "Logic/MainTown/MatchEngine/EngineMainView")
	self:SetWindowName("EngineNeedPlate", "Logic/MainTown/MatchEngine/EngineNeedPlate")
	self:SetWindowName("EngineReportPlate", "Logic/MainTown/MatchEngine/EngineReportPlate")
	self:SetWindowName("EngineSkillPlate", "Logic/MainTown/MatchEngine/EngineSkillPlate")
	self:SetWindowName("EngineTimelinePlate", "Logic/MainTown/MatchEngine/EngineTimelinePlate")
	self:SetWindowName("MatchEngineView", "Logic/MainTown/MatchEngine/MatchEngineView")
	self:SetWindowName("EngineFormationView", "Logic/MainTown/MatchEngine/EngineFormationView")
	self:SetWindowName("EngineTacticsView", "Logic/MainTown/MatchEngine/EngineTacticsView")
	self:SetWindowName("EngineTiliPlate", "Logic/MainTown/MatchEngine/EngineTiliPlate")
	self:SetWindowName("ShowFormationPlate", "Logic/MainTown/MatchEngine/ShowFormationPlate")
	--冠军之路
	self:SetWindowName("ChampionsRoadView", "Logic/MainTown/ChampionsRoad/ChampionsRoadView")
	self:SetWindowName("ChampionsRoadItem", "Logic/MainTown/ChampionsRoad/ChampionsRoadItem")
	--默契升级
	self:SetWindowName("TacitLevelUpView", "Logic/MainTown/Tacit/TacitLevelUpView")	
	-- 球场
	self:SetWindowName("GroundView", "Logic/MainTown/Ground/GroundView")
	self:SetWindowName("StadiumBuilding", "Logic/MainTown/Stadium/StadiumBuilding")
	self:SetWindowName("EmployeeListView", "Logic/MainTown/Stadium/EmployeeListView")
	self:SetWindowName("FieldView", "Logic/MainTown/Ground/FieldView")	
	self:SetWindowName("ManagerBuildingView", "Logic/MainTown/Ground/ManagerBuildingView")	
	self:SetWindowName("EmployeeItem", "Logic/MainTown/Ground/Common/EmployeeItem")	
	self:SetWindowName("EmployeeDetailPop", "Logic/MainTown/Ground/Common/EmployeeDetailPop")	
	self:SetWindowName("BuildingLevelUpPop", "Logic/MainTown/Ground/Common/BuildingLevelUpPop")	
end

function WindowNameConst:SetWindowName(k,v)
    if WindowNameConst[k] ~= nil then  
        logError("Repeat key " .. k)
    end
    WindowNameConst[k] = v;
end

return WindowNameConst.new()
