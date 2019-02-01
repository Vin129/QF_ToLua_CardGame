UIView = class("UIView",ICommand)
-- 构造函数
-- @param type View的类型
-- @member mUIRoot Type(GameObject)
-- @tab mTimerList 记录此类所拥有的计时器,需要通过此类的成员函数创建才会记录
-- @tab mSubViewList 记录子view列表
-- @tab mItemWndList 记录子物件
-- @tab mWndDataTbl 用于与cs通信并创建uiroot对象的消息体
function UIView:ctor(type)
	self.mParam = nil;
	self.mSubViewList = {} 
	self.mItemWndList = {}	--存在的原因是用来记录item，而item对象在被删除时是不需要主动destroy gameobject的
	self.mTimerList = {}
	self.mUIRoot = nil
	self.mViewType = type;
	self.mIsVisible = false;
	self.mIsSuccessInStash = false
	self.mFirstLoad = true	--是否初次加载界面
	self.mInitRequestSuccess = false -- 如果请求失败，界面不会被显示，下次点击时候会直接执行Show方法，根据该状态判断是否需要再次请求
	self.mWndDataTbl = {Name = NotiConst.CREATE_WINDOW, Body = CreateWndData.New()}
	self.mWndDataTbl["Body"].WndName = self.__cname;
	self.mWndDataTbl["Body"].Type = type;
	self.mWndDataTbl["Body"].Async = false;
	self.mWndDataTbl["Body"].LuaCallback =
	function(gameObject)
		self:LoadFinished(gameObject)
	end;
end

-- 【不可被重载】 called after gameobject is create finished 
function UIView:LoadFinished(gameObject)
	self.mUIRoot = gameObject;
	self:Visible(false)
	self:BindUI();
	self:InitRequest(function ()
		self.mInitRequestSuccess = true
		self:PushInStash()
		self:CreateViewData();
		self:Show();
		self:RegisterEventHandler();
	end)
	-- self:CreateViewData(); 
	-- self:Show();
	-- self:RegisterEventHandler();
end

-- 针对界面初始化涉及请求服务器的情况
-- 请重载这个方法，初始化的数据写在这里面
--【注意】重载该方法时一定要带该func参数【注意】
--【注意】请一定要在请求成功的返回里面优先执行func（）方法【注意】
function UIView:InitRequest(func)
	func()
end

function UIView:GetViewType()
	return self.mViewType
end

function UIView:GetName()
	return self.mWndDataTbl["Body"].WndName;
end

function UIView:SetFileName(value)
	self.mFileName = value
end

function UIView:GetFileName()
	return self.mFileName;
end

function UIView:getStash()
	if self.mParam == nil then
		return nil
	end

	return self.mParam[UIConst.StashFlag]
end

function UIView:SetFirstLoad(value)
	self.mFirstLoad = value
end

--[[ =========【不可被重载】============【不可被重载】===========【不可被重载】======== ]]--
-- 不需要主动调用，在LoadFinished中被调用了
-- 此函数不需要被继承，如果需要在显示显示的时候做一些特殊事情，请重载OnShow函数
function UIView:Show()
	
	if not self.mInitRequestSuccess then
		self:InitRequest(function ()
			self:PushInStash()
			self:CreateViewData();
			self:RegisterEventHandler();
			self.mInitRequestSuccess = true
			self:BeforeOnShowFunc()
		end)
	else
		self:BeforeOnShowFunc()
	end
end
-- 原本Show中的方法
function UIView:BeforeOnShowFunc()
	if nil == self.mUIRoot or self.mUIRoot:Equals(nil) then
		self.mSubViewList = {};
		LuaHelper.SendMessageCommand(self.mWndDataTbl);
	else
		self:Visible(self.mWndDataTbl["Body"].Visible);
		if nil ~= self.mViewData then
			self.mViewData:ProcessData();
		end
		for k, v in pairs(self.mSubViewList) do	
			if nil ~= v and v.mIsVisible == true then
				v:Show()
			end
		end
		self:OnShow()
		self:SetFirstLoad(false)
	end
end

function UIView:CheckRePush()
	if nil ~= self.mParam and true == self.mParam[UIConst.StashFlag] and self.mIsSuccessInStash then
		local stashMgr = WindowMgr:GetStashMgr()
		if(nil ~= stashMgr) then
			local isRePush = stashMgr:RePush(self,self.mParam);
			if self.mUIRoot and isRePush then
				UIUtil:setViewAsLastSibling(self.mUIRoot)
			end
		end
	end
end

function UIView:ShowSubView(view,bool)
	if(view~=nil) then
		if(type(view)=="table")then
			for i,v in ipairs(view) do
				if(v~=nil) then
					v:SetActive(bool[i])
				end
			end
		else
			view:SetActive(bool)
		end
	end
end

-- 与Show类似，不需要主动调用，也不需要重载，如果需要在界面被隐藏的时候做些特殊的事情，请重载OnHide函数
function UIView:Hide()
	if nil == self.mUIRoot then
		return;
	end
	self:Visible(false)
	self:OnHide()
end

function UIView:Visible(isVisible)
	if nil == self.mUIRoot then
		return;
	end
	
	self.mUIRoot:SetActive(isVisible)
	self.mIsVisible = isVisible;
end

function UIView:IsVisible()
	return self.mIsVisible;
end

function UIView:Close()
	if nil ~= self.mParam and true == self.mParam[UIConst.StashFlag] and self.mIsSuccessInStash then
		local stashMgr = WindowMgr:GetStashMgr()
		if nil ~= stashMgr then
			stashMgr:PopWnd();
			return
		end
	end

	if not self.mUIRoot or self.mUIRoot:Equals(nil) then
		return
	end

	local param = {}
	param['object'] = self
	param['destory'] = true
	param['release'] = true
	CommandManager:PostCommand(CommandConst.CloseWindow, param)
end


-- 【不可被重载】窗口被销毁的时候才调用，不需要显示调用，如果想在窗口销毁的时候做些别的事情，请重载OnDestory
-- bool destory 用来表示是否需要显示销毁gameobject
function UIView:Destroy(destory)
	self:RemoveEventHandler();
	if nil ~= self.mSubViewList then
		for k, v in pairs(self.mSubViewList) do	
			if nil ~= v then
				v:Destroy(false)
			end
		end
	end
	if nil ~= self.mItemWndList then
		for k, v in pairs(self.mItemWndList) do
			if nil ~= v then
				v:Destroy(false)
			end	
		end
	end
	
	if nil ~= self.mTimerList then
		for k,v in pairs(self.mTimerList) do
			TimerMgr:RemoveTimer(v);
		end
	end
	
	if not self.mUIRoot or self.mUIRoot:Equals(nil) then
		
	elseif nil ~= self.mUIRoot and true == destory then
		local cmdMsg = {Name = NotiConst.DESTROY_WINDOW, Body = DestroyWndData.New();}
		cmdMsg.Body.WndName = self.mWndDataTbl["Body"].WndName
		cmdMsg.Body.Object = self.mUIRoot;
		LuaHelper.SendMessageCommand(cmdMsg);
	end
	self.mUIRoot = nil;
	self.mSubViewList = nil;
	self.mItemWndList = nil;
	self.mTimerList = nil;
	self.mViewData = nil;

	self:OnDestroy();
end

function UIView:Create(param)
	self.mParam = param;	
	if nil ~= self.mUIRoot then
		LuaHelper.DestroyGameObject(self.mUIRoot)
	end
	self:Init();
end

function UIView:PushInStash()
	if nil ~= self.mParam and true == self.mParam[UIConst.StashFlag] then
		local stashMgr = WindowMgr:GetStashMgr()
		if(nil ~= stashMgr) then
			stashMgr:PushWnd(self,self.mParam);
			self.mIsSuccessInStash = true
		end
	elseif self.mViewType == WindowType.Window then
		logWarn(self.__cname.." not stashed!")
	end

	if self.mParam and self.mParam.offsetZ then
		local pos = Vector3(self.mUIRoot.transform.localPosition.x, self.mUIRoot.transform.localPosition.y, self.mParam.offsetZ)
		UIHelper.SetLocalPosition(self.mUIRoot, pos)
	end
end


--处理缓存数据或者是本view关心的数据
function UIView:CreateViewData()
	self.mViewData = require("logic/Data/viewdata").new()
end

function UIView:RemoveViewData()
end

function UIView:CreateSubView(viewName,scriptPath,param)
	if nil == self.mSubViewList then
        self.mSubViewList = {}
    end
    local viewObj = self.mSubViewList[viewName];
    if nil ~= viewObj then
        viewObj:Show();
    else
        --log(viewName)
        viewObj = LuaHelper.DoFile(scriptPath);
		if(nil ~= viewObj) then
            viewObj:Create(param);
            viewObj:SetFileName(viewName);
        end
        if WindowType.Window == viewObj:GetViewType() then
            self.mSubViewList[viewName] = viewObj;
        end
    end
    return viewObj
end


function UIView:RemoveSubView(subView,flag)
	if nil == subView then
		return;
	end
	subView:Destroy(flag);
	if(nil ~= self.mSubViewList) then
		for k,v in pairs(self.mSubViewList) do
			if v == subView then
				self.mSubViewList[k] = nil;
				return;
			end
		end
	end
end

function UIView:CreateItemView(itemName,param)
	local kWndObj = require(itemName).new(WindowType.Item)	
    kWndObj:Create(param);
	if nil == self.mItemWndList then
		self.mItemWndList = {} 
	end
	table.insert(self.mItemWndList,kWndObj);
    return kWndObj
end

function UIView:RemoveItemView(itemObj,flag)
	if nil == itemObj then
        return;
    end

    itemObj:Destroy(flag);
	if nil ~= self.mItemWndList then
		for k,v in pairs(self.mItemWndList) do
			if v == itemObj then
				table.remove( self.mItemWndList,k)
				return;
			end
		end
	end
end

-- 【不可被重载】创建定时器 每帧都执行的定时器
function UIView:CreateFrameTimer(callback)
	if nil == self.mTimerList then
		self.mTimerList = {};
	end
	local timer = TimerMgr:AddFrameTimer(callback);
	table.insert(self.mTimerList,timer);
	return timer
end

--【不可被重载】创建定时器 每过delayTime,执行一次，一共执行loopcount(firstNotDelay：首次是否延时调用)
function UIView:CreateDelayTimer(delayTime,loopCount,callback,firstNotDelay)
	if nil == self.mTimerList then
		self.mTimerList = {};
	end
	local timer = TimerMgr:AddDelayTimer(delayTime,loopCount,callback,firstNotDelay);
	table.insert(self.mTimerList,timer);
	return timer
end

function UIView:RemoveTimer(timer)
	if timer == nil then
		return
	end
	TimerMgr:RemoveTimer(timer);
	if self.mTimerList then
		for k,v in pairs(self.mTimerList) do
			if v == timer then
				table.remove(self.mTimerList,k)
			end
		end
	end
end

function UIView:StopTimer(timer)
	TimerMgr:StopTimer(timer);
end

function UIView:PlayTimer(timer)
	TimerMgr:PlayTimer(timer);
end

---- 懒人福利 ------
--修改文字
function UIView:write(obj,text)
    if obj == nil then
        return
    end
    UIHelper.SetLabelText(obj,text)
end
--关闭
function UIView:closeObj(obj)
	if obj == nil then
		return
	end
	obj:SetActive(false)
end
function UIView:showObj(obj)
	if obj == nil then
		return
	end
	obj:SetActive(true)
end

--=======

function UIView:FrameInstantiate(name, totalCount, insFunc, doneCallback, countPerframe)
	if not self.frameInsTable then
		self.frameInsTable = {}
	end

	if self.frameInsTable[name] then
		logError("FrameInstantiate name "..name.." already exist")
		return
	end

	countPerframe = countPerframe or 1

	self.frameInsTable[name] = {}
	self.frameInsTable[name].curretIndex = 0
	self.frameInsTable[name].frameTimer = self:CreateFrameTimer(
		function()
			for i = 1, countPerframe do
				self.frameInsTable[name].curretIndex = self.frameInsTable[name].curretIndex + 1
				if self.frameInsTable[name].curretIndex > totalCount then
					self:RemoveTimer(self.frameInsTable[name].frameTimer)
					self.frameInsTable[name].frameTimer = nil
					self.frameInsTable[name] = nil
					if doneCallback then
						doneCallback()
					end
					return
				end
				insFunc(self.frameInsTable[name].curretIndex)
			end
		end
	)
end

--[[========【可被重载的函数】=============【可被重载的函数】===========【可被重载的函数】=========]]--
function UIView:Init()
end

function UIView:BindUI()
end

function UIView:OnShow()
end

function UIView:OnHide()
end

function UIView:OnDestroy()
end
