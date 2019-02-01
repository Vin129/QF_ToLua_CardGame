local RedpointMgr = class("RedpointMgr")

function RedpointMgr:setData(t, tid, pid, bsc, v2p, opLv)
    local typeItem = {}
    typeItem.tId = tid
    typeItem.bShowCount = bsc
    typeItem.v2Pos = v2p
    typeItem.parentId = pid
    typeItem.opLv = opLv
    self.dicIdToType[tid] = typeItem
end

function RedpointMgr:ctor()
    self.dicRedpoints = {}
    self.dicIdToType  = {}
end

function RedpointMgr:InitConfig()
    self.dicRedpoints = {}
    self.dicIdToType  = {}
    
    --              name                    id      parentId    ifShowNumber    dotPos              opLv
  --self:setData(   "LOGIN_ACTIVITY",       0,      nil,        true,           Vector2(50, 50)                                 )
  --self:setData(   "LOGIN_ACTIVITY",       0,      nil,        false,          nil                                             )
    self:setData(   "EXCHANGE_ACTIVITY",    0,      nil,        false,          nil,                 0                          )                                                 
    self:setData(   "MATCH",                1,      nil,        false,          nil,                 JumpOpenLv["4"]            )
    self:setData(   "MAIL",                 2,      nil,        false,          nil,                 0                          )
    self:setData(   "FRIEND_APPLY",         3,      3000,       false,          nil,                 0                          ) 
    self:setData(   "FRIEND_GIFT",          4,      3001,       false,          nil,                 0                          )   --好友一键领取按钮
    self:setData(   "TASK",                 5,      2000,       false,          nil,                 0                          )
    self:setData(   "LOGIN_REWARD",         6,      1000,       false,          nil,                 0                          )
    self:setData(   "ENERGY_ACTIVITY",      7,      1000,       false,          nil,                 0                          )
    self:setData(   "RANK_RACE",            8,      1001,       false,          nil,                 JumpOpenLv["5-1"]          )
    self:setData(   "TOUR_RACE",            9,      1001,       false,          nil,                 JumpOpenLv["5-2"]          )
    self:setData(   "LEGEND_CHALLENGE",     10,     1001,       false,          nil,                 JumpOpenLv["5-3"]          )
    self:setData(   "RECRUIT",              11,     10032,      false,          nil,                 0                          )   --顶级招募
    self:setData(   "RANKING_LIST",         12,     nil,        false,          nil,                 0                          )
    self:setData(   "SIGN_IN",              13,     nil,        false,          nil,                 0                          )
    self:setData(   "LEVEL_PACKAGE",        14,     1000,       false,          nil,                 0                          )   --等级礼包
    self:setData(   "SEVEN_DAYS",           15,     1000,       false,          nil,                 0                          )   --七日活跃
    self:setData(   "PRACTICE_COMPETITION", 16,     1001,       false,          nil,                 JumpOpenLv["5-6"]          )   --训练赛
    self:setData(   "CHAMPION_ROAD",        17,     1001,       false,          nil,                 JumpOpenLv["5-5"]          )   --冠军之路
    self:setData(   "FOOTBALL_FIELD",       18,     nil,        false,          nil,                 JumpOpenLv["11"]           )   --球场
    self:setData(   "BETA_TEST_LOGIN",      19,     1000,       false,          nil,                 0                          )   --封测礼包
    self:setData(   "RECHARGE_RETURN_BEFORE",20,    1000,       false,          nil,                 0                          )   --充值返还活动（可领取本轮之前的所有的返还）
    self:setData(   "TELEPHONE",            21,     4001,       false,          nil,                 0                          )   --手机号绑定红点
    self:setData(   "BETA_TEST_LOGIN",      22,     1000,       false,          nil,                 0                          )   --充值返还活动(本轮，不可领取)

    self:setData(   "ACTIVITY_ALL",         1000,   nil,        false,          nil,                 0                          )   --活动入口
    self:setData(   "FIGHT",                1001,   nil,        false,          nil,                 JumpOpenLv["5"]            )   --竞技
    self:setData(   "STORE",                1002,   nil,        false,          nil,                 0                          )   --商城入口
    self:setData(   "DJ_STORE",             10021,  1002,       false,          nil,                 0                          )   --道具商城
    self:setData(   "PM_STORE",             10022,  1002,       false,          nil,                 JumpOpenLv["10"]           )   --排名商城
    self:setData(   "SJ_STORE",             10023,  1002,       false,          nil,                 JumpOpenLv["6-3"]          )   --赛季商城
    self:setData(   "YZ_STORE",             10024,  1002,       false,          nil,                 JumpOpenLv["6-4"]          )   --远征商城
    self:setData(   "CQ_STORE",             10025,  1002,       false,          nil,                 JumpOpenLv["6-5"]          )   --传奇商城
    self:setData(   "RECRUIT_HOME",         10031,  nil,        false,          nil,                 0                          )   --招募首页入口
    self:setData(   "RECRUIT_IN",           10032,  10031,      false,          nil,                 0                          )   --招募入口
    self:setData(   "RECRUIT_STAR",         10033,  10032,      false,          nil,                 0                          )   --球星招募
    self:setData(   "RECRUIT_TOP",          10034,  10032,      false,          nil,                 0                          )   --顶级招募
    self:setData(   "STORY_CATE",           1004,   nil,        false,          nil,                 0                          )   --故事生涯
    self:setData(   "CATE_AWARD",           1005,   nil,        false,          nil,                 0                          )   --章节奖励
    self:setData(   "ROOM",                 1006,   nil,        false,          nil,                 0                          )   --更衣室
    self:setData(   "PLAYER_STARUP",        1007,   1006,       false,          nil,                 0                          )   --球员升星
    self:setData(   "EQUIP_STARUP",         1008,   1006,       false,          nil,                 0                          )   --装备升星
    self:setData(   "TASK_HOME",            2000,   nil,        false,          nil,                 0                          )   --每日任务奖励
    self:setData(   "TASK_EVERYDAY",        2001,   2000,       false,          nil,                 0                          )   --每日任务
    self:setData(   "TASK_ACTIVITY",        2002,   2000,       false,          nil,                 0                          )   --活动任务
    self:setData(   "FRIEND_MAIN",          3000,   nil,        false,          nil,                 0                          )   --好友入口
    self:setData(   "FRIEND_GIFT1",         3001,   3000,       false,          nil,                 0                          )    
    self:setData(   "TELEPHONE1",           4001,   nil,        false,          nil,                 0                          )    
end

function RedpointMgr:RegisterRedpoint(go, tyId)
    if AppConst.IsIOSReview then
        return
    end

    if not go then return end

    local ty = self.dicIdToType[tyId]
    if not ty then return end

    local item = self.dicRedpoints[tyId]
    if not item then
        item = {}
        item.count = 0
        item.opLv = ty.opLv
        self.dicRedpoints[tyId] = item
    end

    --如果已经挂载红点，不再创建
    local rp = LuaHelper.LoadPrefab("Prefabs/UI/Common/Redpoint/Redpoint", go);
    rp:SetActive(false)
    if ty.v2Pos then
        LuaHelper.SetLocalPositionVector3(rp,ty.v2Pos.x, ty.v2Pos.y, 0)
    else
        local width = UIHelper.GetObjWidth(go);
        local height = UIHelper.GetObjHeight(go);
        LuaHelper.SetLocalPositionVector3(rp,width / 2 - 20, height / 2 - 20, 0)
    end
    LuaHelper.SetLoaclScale(rp, 1, 1, 1)

    item.obj = rp
    
    -- default mark is shown
    if ty.bShowCount then
        item.txtCount = LuaHelper.FindChild(rp, "txt_count")
        item.txtCount:SetActive(true)
        LuaHelper.FindChild(rp, "img_mark"):SetActive(false)
    end

    self:refresh(item)
end

function RedpointMgr:Increase(tyId, tarCount)
    self:changeTo(tyId, {target = tarCount, delta = 1})     
end

function RedpointMgr:Decrease(tyId, tarCount)
    self:changeTo(tyId, {target = tarCount, delta = -1})  
end

function RedpointMgr:changeTo(tyId, change)
    local ty = self.dicIdToType[tyId]
    if not ty then return end
    local item = self.dicRedpoints[tyId]
    if not item then
        item = {}
        item.count = 0
        item.opLv = ty.opLv
        self.dicRedpoints[tyId] = item
    end
    
    if User:TeamMgr():isReachLv(ty.opLv) then
        local oldCount = item.count
        -- judge target first
        item.count = change.target or item.count + change.delta
        if item.count < 0 then
            item.count = 0
        end

        if ty.parentId and item.count ~= oldCount then
            self:changeTo(ty.parentId, {delta = item.count - oldCount})
        end
    end

    self:refresh(item)
end

function RedpointMgr:refresh(item)    
    if not item.obj or item.obj:Equals(nil) then
        return
    end

    item.obj:SetActive(item.count > 0)

    if item.txtCount then
        UIHelper.SetLabelText(item.txtCount, tostring(item.count))
    end 
end

function RedpointMgr:Reset()
    self.dicRedpoints = {}
end

function RedpointMgr:isReachRedPoint(type)
    local ty = self.dicIdToType[type]
    return ty.opLv == User:TeamMgr():getData("level")
end

return RedpointMgr.new()