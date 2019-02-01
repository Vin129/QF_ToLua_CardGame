module("Common",package.seeall)

local LongClickButton = class("LongClickButton")
function LongClickButton:ctor(uiRoot)
    self.mUIRoot = uiRoot;
    self.isLongClick = false;
    self.timer = nil;
	UIHelper.SetButtonClickExtensionEvent(uiRoot,function(sender,event)
		self:handleEvents(sender, event);
	end)
end

function LongClickButton:Enabled(enabled)
    UIHelper.SetButtonInteractable(self.mUIRoot,enabled);
end

function LongClickButton:handleEvents(sender, event)
    if event == "OnPress" then
        self.isLongClick = false;
        self.timer = TimerMgr:AddDelayTimer(0.2,1,function()
            TimerMgr:RemoveTimer(self.timer);
            self.timer = nil;
            self.isLongClick = true;
        end)
    elseif event == "OnRelease" then
        if self.timer then
            TimerMgr:RemoveTimer(self.timer);
            self.timer = nil;
        end
        self:OnLongPress();
        self.isLongClick = false;
    elseif event == "OnHeld" then
        if self.isLongClick then
            self:OnLongPress();
        end
    end
end

function LongClickButton:OnLongPress()
    log("LongClickButton:OnLongPress")
end

return LongClickButton