UIConst =
{
	Parent = "parent",
	RootUI = "rootui",
	Prefabs = "prefabs",
	PrefabName = "prefabname",
	DragCanvas = "dragcanvas",
	StashFlag = 'stash',
	HidePreWindow = "HidePreWindow",
	Visible = "visible",
	MultiInstNum = "MultiInstNum",
}

CtrlDef =
{
	Text = "text",
	Progressbar = "progressbar",
	Image = "image",
	RawImage = "rawimage",
}

InFormMgrState = {
    Dormant = "Dormant",
    Running = "Running",
    Stop = "Stop"
}

PlayerStarColor = {
    "8FE852FF",
    "8FE852FF",
    "5291E8FF",
    "9552E8FF",
    "E8C552FF",
    "F46A63FF",
    "C7B660FF",
}

PlayerHpColor = {
    "2EAB31FF",
    "7BEB04FF",
    "C2B13DFF",
    "E59102FF",
    "AB2E2EFF",
}

-- 经常使用的颜色
CommUseColor = {
    White = "FFFFFFFF",
    BtnGray = "878787FF",
}

-- 招募类型
RecruitType = {
    "StarPlayer",
    "TopPlayer",
    "TimerPlayer",
}

-- 招募卷类型
RecruitCardType = {
    StarPlayer = "1035", 
    TopPlayer = "1038", 
    TimerPlayer = "1039", 
}


-- 登录注册错误提示
LRErrorType = {
    LoginId = 1,
    LoginPsd = 2,
    PhoneId = 3,
    PhoneMsg = 4,
    PhonePsd1 = 5,
    PhonePsd2 = 6,
    UserId = 7,
    UserPsd1 = 8,
    UserPsd2 = 9,
}

-- 设置toggle种类
SettingTogType = {
    BGM = 1,
    MusicEffect = 2,
}

-- 比赛引擎开启类型
EngineMatchType = {
    PVP = 1,
    PVE = 2,
    VIEW = 3,
}

-- 比赛状态
EngineState = {
    Prepareing = 1,
    Matching = 2,
    PointSphere = 3,
    End = 4,
    None = 5,
}

--我的主客位
MyMatchFlag = {
    Home = 0,
    Away = 1,
    Viewer = 2, 
}

ObjType = {
    User = 1,
    Player = 2,
}

-- 比赛横竖屏
ViewState = {
     H = 1,
     V = 2,   
}

--阵容所需类型
FormationNeedType = {
    Normal = 1,
    Exchange = 2,
}

--比赛类型编号，buildMatch所需
BuildMatchType = {
    LeagueMatch = 100, --联赛
    CupMatch = 101, --杯赛
    RankMatch = 200, --排名赛
    TourMatch = 201, --巡回赛
    FriendMatch = 202, --友谊赛
    AdvMatch = 203,--广告赛
    CareerMatch = 204,--生涯赛
}
-- 引擎注册事件
EngineEventType = {
    MatchStar = 0,
    MatchEnd = 1,
    AnimationStart = 4,
    AnimationEnd = 5,
    PenaltyStart = 6,
    ExtraTime = 7,
    PlayerEvent = 8,
    Text = 9,
}
--引擎注册信息更新
EngineInfoEvent = {
    Formation = 1,
    Tactics = 2,
    PlayerStatus = 3,
    Statistics = 4,
    Time = 5,
    KeyPlayer = 6,
}

-- EngineEventType.Event 中的事件类型
EnginePlayerEvent = {
    Foul = 0, --犯规
    Yellow = 1, --黄牌
    Red = 2, --红牌
    MiddleEnd = 3, --中场结束
    FreeKick = 4, --任意球
    Corner = 5,  --角球
    Penalty = 6,  --点球
    Switch = 7,   --换人
    Goal = 8,   --进球
    UpdateScore = 9, -- 比分更新
    Formation = 10, --阵容通知
    Statistics = 11, --统计数据更新
    PlayerStatus = 12, --球员状态更新
    Tactics = 13, --战术更新
    Pass = 14,  --传球
    Break = 15, --突破
    Shoot = 16, --射门
    PenaltyRoundStart = 17, --点球回合开始
    PenaltyGoal = 18, -- 点球进球
    PenaltyShoot = 19, -- 点球没进
    PenaltyStateUpdate = 20, -- 点球结果更新
    OtherAction = 21, --其他动作
    Hurt = 30, -- 受伤
    Injury = 31, -- 重伤
    DoubleYellow = 32, --双黄牌
    MatchStart = 100,
    MatchEnd = 101,
}

--惩罚种类
EnginePunishType = {
    Yellow = 1,
    Red = 2,
    DoubleYellow = 3,
    Hurt = 4,
    Injury = 5,
}


--抽卡种类
CardEffectType = {
    OneGeneral = 1,
    TemGeneral = 2,
    SpecialPlayer = 3,
    SpecialEquip = 4,
}

--====================---===============---=======
-- 跳转类型
ToJumpType = {
    Recruit = 1, -- 招募
    Store = 2, -- 商城
    Fight = 3 -- 竞技
}

PmdType = {
    Horn = 1,
    System = 2,
    User = 3,
    Loop = 4,
}

ChatType = {
    World = 1,
    Union = 2,
    User = 3,
}

UserLevelLimit = {
    EquipStarUp = 0
}

DebrisStatus = 
{
    CanCombine = 1,--合成类型且数量足够
    CanStarUp = 2,--升星类型且数量足够

    NoCombine = 100,--合成类型且数量不足
    NoStarUp = 101,--合成类型且数量不足

    Other = 200,--其他类型
}

PlayerStatus = 
{
    CanLevelUp = 1,--可升级
    CanStarUp = 2,--可升星

    PlayerLevelLimit = 100,--球员等级限制
    UserLevelLimit = 101,--用户等级限制
    DebrisNumLimit = 102,--碎片数量限制
    PropertyNumLimit = 103,--道具数量不足
    CoinNumLimit = 104,--欧元数量不足

    Other = 200,
}

EquipDetailMode = {
    Player = 0,--球员详情，查看装备详情
    Room = 1,--更衣室，查看装备详情，不显示卸下和更换
    Material = 2 --升级升星，作为材料时，功能按钮都不显示
}

EquipStatus = 
{
    CanLevelUp = 1,--可升级
    CanStarUp = 2,--可升星
}

EquipAutoType = {
    ATK = 1, -- 进攻
    DEF = 2, -- 防御
    SYN = 3, -- 综合
    GK = 4, -- 守门员
}


--RoomView
RoomPageType = 
{
    Player = 1,
    PlayerDebris = 2,
    Equip = 3,
    EquipDebris = 4,
    HandbookPlayer = 5,
    GroundEmployee = 6,
}

RoomPageSortType = 
{
    Default = 1,
    Power = 2,
    Quality = 3,
    Score = 4,
    Time = 5,
    Numer = 2,
    Level = 2,
    Site = 4,
}

RoomPageSortCondition = 
{
    Big2Small = 1,
    Smaill2Big = 0,
}

RoomPageMode = 
{
    Normal = 0,
    LevelUp = 1,
    StarUp = 2,
    Change = 3,
    Material = 4,
    Recycle = 5,    
    Reborn = 6, 
    Adver = 7,
    Enhance = 8, --强化选卡
    EnhanceMat = 9, --强化选材料
    Tacit = 10, -- 默契

    MidHurt = 11,     --护理室（受伤天数>1）
    SmallHurt = 12,   --康复室（受伤天数=1）
    BadHurt = 13,     --重症室（重伤球员）
    TiliNotFull = 14,        --体力不满
    NotTop = 15,             --非顶级
}

RoomPageBtnType = 
{
    PlayerDetail = 0,
    PlayerRoad = 1,

    PlayerDebrisRoad = 10,
    PlayerDebrisCompound = 11,
    PlayerDebrisStar = 12,
    PlayerDebrisSell = 13,

    EquipDetail = 20,
    EquipRoad = 21,
    
    EquipDebrisRoad = 30,
    EquipDebrisCompound = 31,
    EquipDebrisStar = 32,
    EquipDebrisSell = 33,

    Detail = 40
}

BagPageType = 
{
    All = 1,
    Material = 2,
    Gift = 3
}

BagPageMode = 
{
    Normal = 0,
    Select = 1,
}

BagPageBtnType = 
{
    Sale = 0,
    Use = 1,
    Level = 2,
    Star = 3,
    OpenSelect = 4,--打开可选礼包
}
-- 弹窗类型
TipPopType = {
    Normal = 0,
    HaveBtn = 1,
    Tips = 2,
    Debris = 3,
    Prop = 4,
    Gain = 5,
}
-- VipType
VipPopType = {
    Match = 1,
    Tili = 2,
    Rank = 3,
}

-- 不足弹窗
LackPopType = {
    Tili = 1,
    Diamond = 2,
    PlayerCareerWipe = 3,
    Vip = 4,
    Coin = 5,
    NormalProp = 6,
}
-- 道具种类
PropertyType = {
    Diamond = 1,
    Coin = 2,
    LevelUp = 3,
    StarUp = 4,
    CanUserInBag = 5,
    Other = 99,
}

PlayerWoundType = {
    "<color=#30D70D>健康</color>",
    "<color=#F7CE41>轻伤</color>",
    "<color=#E23A3C>重伤</color>"
}

PlayerCoverType = {
    Tili = 1,
    State = 2,
    Wound = 3
}

WorldChallengeType = {
    Left = "Left",
    Mid = "Mid",
    Right = "Right",
}

ChatItemType = {
    Player = 1,
    System = 2,
    User = 3,
}

ChatPlayerType = {
    Friend = 1,
    History = 2,
}

WorldRankType = {
    Lv1 = 4,
    Lv2 = 3,
    Lv3 = 2,
    Lv4 = 1
}

FriendTiemType = {
    Friend = 1,
    Require = 2,
    Add = 3,
    MatchReport = 4,
}

FriendBtnsEvent = {
    Receive = 1,
    Send = 2,
    Delete = 3,
    Add = 4,
    Agree = 5,
    Oppose = 6,
    Chat = 7,
}

GainType = {
    Prop = 1,
    Debris = 2,
}

TourMatchType = {
    FA = 1,
    LL = 2,
    SA = 3,
    BL = 4,
}

ChessGridType = {
    Road = 0, -- 路
    Building = 1, -- 无用建筑
    Vend = 2, -- 贩卖机
    Bar = 3, -- 风俗店
    Gift = 4, -- 礼包
    Enemy = 5, -- 对手
}

ItemType = {
    Player = 1,             --球员
    PlayerDebris = 2,       --球员碎片
    Equip = 3,              --装备
    EquipDebris = 4,        --装备碎片
    Property = 5,           --道具
    Package = 6,            --礼包（可选/随机）
}

PopType = {
    Email = 1,              --邮件详情pop
    Gain = 2,               --获取道具pop
    Normal = 3,             --通用确定 取消pop
    Choose = 4,             --经理人选择队徽/队服
    Skill = 5,              --球员/装备技能升级
    SkillRefresh = 6,       --技能刷新
    OutPut = 7,             --产出
    BuyGoods = 8,           --商城购买道具
    Sponsors = 9,           --赞助商
    SelectPackage = 10,     --可选礼包
    RandPackage = 11,       --随机礼包
    PlayerDetail = 12,      --球员简介
    ChangeName = 13,        --改名pop
    Rule = 14,              --规则pop
    ManagerLevelUp = 15,    --经理人升级
    GetPlayerNum = 16       --球员上限扩容
}

--技能升级pop框类型
SkillPopType = {
    Player = 1,     --球员
    Equip = 2,      --装备
}

-- 关卡类型
ChapterType = {
    Small = 1,
    Big = 2,
}

--结算类型
SettleType = {
    TourMatch = 1,
    PlayerCareer = 2,
    StoryCareer = 3,
    LegChalleng = 4,
    ChampionRoad = 5,
    FriendMatch = 6,
    WorldRank = 7, --排名赛
}

--阵容职责类型
DutyType = {
    Caption = 1,
    Free = 2,
    Pointer = 3,
    LeftC = 4,
    RightC = 5,
}

-- 回收类型
RecycleType = {
    PlayerRecy = 1,
    PlayerReborn = 2,
    EquipRecy = 3,
    EquipReborn = 4,
}

-- 出售类型
SellType = {
    PlayerDebris = 1,
    EquipDebris = 2,
}

-- 任务状态
MissionState = {
    Completed = 1, -- 完成未领取
    CanJump = 2, -- 可以跳转 
    NoComplete = 3, -- 未完成且无跳转
    HasGot = 4, -- 已经领取
}

--特殊item类型
SpecialItemType = {
    Exp = 1, -- 经验
    Activity = 2,  -- 活跃度
    Score = 3  -- 七日活跃积分
}

--传奇挑战赛状态
LegChallengType = {
    ToStar = 1, -- 开始挑战（没打到过第一个记录点）
    ReStar = 2, -- 重新开始
    Continue = 3 -- 正在挑战
}

--广告状态
AdvItemType = {
    NoOpen = 1, -- 前置未过
    NoPlayer = 2, -- 未安排球员
    OnShoot = 3, -- 正在拍摄
    CmplShoot = 4, -- 拍摄结束
    Special = 5, -- 存在特殊事件
}

-- 广告条件
AdvCoditionType = {
    Position = 1,
    Height = 2,
    Age = 3,
    Country = 4,
    PlayerType = 5,
    Star = 6,
    Level = 7,
}
-- 充值Item类型
RechargeItemType = {
    Diamond = 0,
    Privilege = 1,
}

--充值活动类型
RechargeLabelType = {
    Normal = 0,
    First = 1, -- 首充
    Double = 2, -- 双倍
    Discount = 3, -- 打折
}

VipPanelType = {
    Recharge = 1,
    Vip = 2,
}

-- UIHelper.SetButtonSpriteSwap
BtnSptSwapType = {
    Target = 1,
    Highlighted = 2,
    Pressed = 3,
    Disabled = 4,
}
-- 对应common_property_add_up_cost该表类型
OrderUpCostType = {
    MallRefresh = 1, --商城刷新
    PlayerRebron = 2,--球员重生
    EquipRebron = 3,--装备重生
    LegChallengTime = 4,--传奇挑战赛次数购买
    SponsorCost = 5,--赞助商消耗
    BuyTili = 6,--购买体力
    StroyCareerRest = 7,--故事生涯重置
    PlayerCareerCost = 8,--球员生涯
    UserChangeName = 9,--经理人更名
    WorldRankTime = 10, -- 世界排名
    Retrospect = 11, -- 补签
    TrainMatch = 12, -- 训练赛次数
    TourMatch = 13 -- 巡回赛次数
}

--杯赛赛事类型
CupMatchType = {
    ZigeSai = 1,
    YuxuanSai = 2,
    TaotaiSai = 3,
    JueSai = 4,
}

--排行榜类型
RankType = {
    AtkRank = 1,
    LevelRank = 2,
    CareerRank = 3,
    PlayerRank = 4,
    SociatyRank = 5,
}


-- 球员小卡状态
SPlayerCard = {
    Normal = 1,
    Formation = 2,
}

--道具类型
PropType = {
    Property = 1,       --道具
    SelectPackage = 2,  --可选礼包
    RandPackage = 3,    --随机礼包
    CardPackage = 4,    --卡包
}

--背包类型
BagType = {
    Material = 1,       --材料
    Package = 2,        --礼包
}

--冠军之路
ChampionsRoadImageTitle = {
    "yinchao_",
    "xijia_",
    "yijia_",
    "dejia_",
}

--默契类型
TacitLevelUpType = {
    ST = 1,
    CM = 2,
    DM = 3,
    GK = 4,
}

-- 充值返还状态
RechargeReturnType = {
    Check = 1, -- 返还详情
    Get = 2, -- 返还领取
}

-- 手机绑定状态
PhoneBindStatus = {
    NoBind = 1,
    Bind = 2,
    RemoveBind = 3,
}


--球场时间
GroundTimeType = {
    Light = 1,
    Night = 2,
}



--球场定位位置
GroundBuildingPos = {
    Vector3.New(-12, 26, 7),	
    Vector3.New(11.3, 12.7, -8.8),	
    Vector3.New(-8, 22, -10),	
    Vector3.New(-10.6, 20, 11.6),	
    Vector3.New(-21, 13, -9),	
    Vector3.New(-21, 20, 12.5),
}
GroundBuildingRota = {
    Vector3.New(60, 120, 0),
    Vector3.New(45, -210, 0),	
    Vector3.New(60, -220, 0),	
    Vector3.New(60, 61, 0),	
    Vector3.New(45, -198, 0),	
    Vector3.New(60, 162, 0),	
}
GroundBuildingName = {
    "主球场",
    "医疗中心",
    "教练馆",
    "休闲中心",
    "经理大楼",
    "训练场",
}

-- 球场层
GroundLayer = {
    Normal = "targetBuilding",
    Choose = "chooseBuilding",
}


--常量定义 number和string  CONST_(T)(N) T:n-number s-string
CONST_nTacitLevelMaxCount = 90        --默契等级最大值
CONST_nRoomMaxCount = 100        --更衣室最大球员数量
CONST_nEnhanceMaxLevel = 9       --强化最大等级
CONST_nPlayerMaxLevel = 100       --球员最大等级
CONST_nEquipMaxLevel = 100        --装备最大等级
CONST_nPlayerMaxStar = 7         --球员最大星
CONST_nSweepTenOpenLv = 15       --扫荡10次开启等级
CONST_nChallengeAgainOpenLv = 15       --再次挑战开启等级
CONST_v3GroundCameraPos = Vector3.New(-16, 42, 21)  -- 球场初始摄像机位置
CONST_v3GroundCameraRota = Vector3.New(60, 152, 0) -- 球场初始摄像机角度