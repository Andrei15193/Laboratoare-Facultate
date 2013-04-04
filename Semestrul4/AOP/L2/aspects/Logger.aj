package aspects;

public aspect Logger
{
    pointcut logAll(): call(* aop2..*(..));
    before(): logAll()
    {
        aop2.Application.logger.info(thisJoinPointStaticPart.getSignature().toLongString());
        aop2.Application.log4j.log(org.apache.log4j.Level.INFO,thisJoinPointStaticPart.getSignature().toLongString());
    }
}
