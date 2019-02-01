local FrameTimer = class("FrameTimer",Timer)

function FrameTimer:ctor(callback)
    self:Init(callback);
end

function FrameTimer:Update(deltaTime)
    if(nil ~= self.mCallback) then  
        self.mCallback(deltaTime)
    end
end

return FrameTimer