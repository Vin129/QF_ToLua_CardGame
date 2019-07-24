--=============================================================================
-- 2019.7 XAVIER
--=============================================================================
local TestView = class("TestView",LuaBehaviour)

--===== 初始化流程:注意Awake方法不要重写 =====
function TestView:BindUI()
	self.Button1 = self:Find(self.gameObject,"Button1");
	self.Button2 = self:Find(self.gameObject,"Button2");
end

function TestView:RegisterUIEvent()

	QUIHelper.SetButtonClickEvent(self.Button1,function()

	end)

	QUIHelper.SetButtonClickEvent(self.Button2,function()

	end)
end

--===== Behaviour生命周期函数 =====
function TestView:OnEnable()

end

function TestView:Start()

end

function TestView:Update()

end

function TestView:OnDisable()

end

function TestView:OnDestroy()

end
--================================
return TestView.new();