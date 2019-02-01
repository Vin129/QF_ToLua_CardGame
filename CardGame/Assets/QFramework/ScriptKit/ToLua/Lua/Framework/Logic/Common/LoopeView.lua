module("Player",package.seeall)

local LoopDemo = class("LoopDemo",UIView)

--��ʼ��
function LoopDemo:Init()
    self.mWndDataTbl["Body"].PrefabName = "Common/LoopView";
    LuaHelper.SendMessageCommand(self.mWndDataTbl);

	self.this = 1--��ǰ��ʾ�Ľ�����
	self.count = 9--��������
	self.width = 750--������
	self.dragWidth = self.width*0.4--�ɴ�����ҳ����̾���
	self.leftPosition = Vector3(-self.width,0,0)
	self.middlePosition = Vector3(0,0,0)
	self.rightPosition = Vector3(self.width,0,0)
	self.outPosition = Vector3(0,10000,0)
	self:reset(self.this)
end

--��ʾ������Դ��
function LoopDemo:OnShow()
	WindowMgr:HideWindow(WindowMgr.mWndList[WindowNameConst.HeadView])
	WindowMgr:ShowWindow(WindowNameConst.HeadView)
	WindowMgr:HideWindow(WindowMgr.mWndList[WindowNameConst.ButtomView])
end

--��λUI���
function LoopDemo:BindUI()
	
	local close = LuaHelper.FindChild(self.mUIRoot,"BackBtn")
	UIUtil:RegisterClickEvent(close,function() self:Close() end)
		
	--Scroll View
	local scrollView = LuaHelper.FindChild(self.mUIRoot,"Scroll View")
	UIHelper.RegisterEndDragEvent(scrollView,function()self:dragEvent()end)
		
	self.content = LuaHelper.FindChild(scrollView,"Viewport/Content")
	self.view = {--��ҳ����
		LuaHelper.FindChild(self.content,"View1"),
		LuaHelper.FindChild(self.content,"View2"),
		LuaHelper.FindChild(self.content,"View3"),
		LuaHelper.FindChild(self.content,"View4"),
		LuaHelper.FindChild(self.content,"View5"),
		LuaHelper.FindChild(self.content,"View6"),
		LuaHelper.FindChild(self.content,"View7"),
		LuaHelper.FindChild(self.content,"View8"),
		LuaHelper.FindChild(self.content,"View9"),
	}
	
	self.button = {--��ҳ��ť
		LuaHelper.FindChild(self.mUIRoot,"Btn1"),
		LuaHelper.FindChild(self.mUIRoot,"Btn2"),
		LuaHelper.FindChild(self.mUIRoot,"Btn3"),
		LuaHelper.FindChild(self.mUIRoot,"Btn4"),
		LuaHelper.FindChild(self.mUIRoot,"Btn5"),
		LuaHelper.FindChild(self.mUIRoot,"Btn6"),
		LuaHelper.FindChild(self.mUIRoot,"Btn7"),
		LuaHelper.FindChild(self.mUIRoot,"Btn8"),
		LuaHelper.FindChild(self.mUIRoot,"Btn9"),
	}
	for i=1,9 do
		UIUtil:RegisterClickEvent(self.button[i],function()self:reset(i)end)
	end
end

--��ȡ��������
function LoopDemo:left(i)
	if i==1 then
		return self.count
	else
		return i-1
	end
end

--��ȡ�Ҳ������
function LoopDemo:right(i)
	if i==self.count then
		return 1
	else
		return i+1
	end
end

--�϶�������
function LoopDemo:dragEvent()
	x = self.content.transform.localPosition.x
	--�����϶�
	if x<-self.dragWidth then
		self.this = self:right(self.this)
		self.content.transform.localPosition = Vector3 (x+self.width,0,0)
	end
	--�����϶�
	if x>self.dragWidth then
		self.this = self:left(self.this)
		self.content.transform.localPosition = Vector3 (x-self.width,0,0)
	end
	self:reset(self.this)
end

--����λ��
function LoopDemo:reset(i)
	--�л���ť״̬
	LuaHelper.FindChild(self.button[i],"Image"):SetActive(true)
	for k,v in ipairs(self.button) do
		if k~=i then
			LuaHelper.FindChild(v,"Image"):SetActive(false)
		end
	end
	--����λ��
	for k,v in ipairs(self.view) do
		v.transform.localPosition = self.outPosition
	end
	self.view[self:left(i)].transform.localPosition = self.leftPosition
    self.view[i].transform.localPosition = self.middlePosition
	self.view[self:right(i)].transform.localPosition = self.rightPosition
end

return LoopDemo.new(WindowType.Window)