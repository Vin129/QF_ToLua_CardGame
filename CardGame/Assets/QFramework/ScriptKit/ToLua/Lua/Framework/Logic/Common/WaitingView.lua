local WaitingView = class("WaitingView",UIView)

function WaitingView:Init()
    self.mWndDataTbl["Body"].PrefabName = "Common/Waiting/Waiting";
    self.cb = self.mParam.cb
    LuaHelper.SendMessageCommand(self.mWndDataTbl);
end

-- 创建窗口后的回调
function WaitingView:BindUI()
    UIUtil:RegisterClickEvent(self.mUIRoot,function(sender) log("WatingView Click") end);
end

function WaitingView:OnShow()
    UIUtil:setViewAsLastSibling(self.mUIRoot)
    self:CreateDelayTimer(
        15, 0,
        function()
            if self.mUIRoot and not self.mUIRoot:Equals(nil) then
                WindowMgr:HideWaiting()
                UIUtil:showTip("请求超时")
                if self.cb then
                    self.cb()
                end
                SocketProxy:ClearAllHandleProto()
                SocketProxy:DistributeError()
                LuaHelper.GetGSNetManager():RequestHeartBeat()
            end
        end
    ) 
end

return WaitingView.new(WindowType.Window)