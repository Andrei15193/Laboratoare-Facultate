package aspects;

import java.util.logging.Logger;

import proiect.controller.Application;

public aspect LoggingAspect
{   
    pointcut logAll(): call(* proiect..*(..));
    
    before(): logAll()
    {
        Logger.getLogger("logger").info(String.format(thisJoinPointStaticPart.getSignature().toLongString()));
    }
}
