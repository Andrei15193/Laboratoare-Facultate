package aspects;

public aspect Logger
{
    pointcut logAll(): call(* aop2..*(..));
    before(): logAll()
    {
        aop2.Application.logger.info(thisJoinPointStaticPart.getSignature().toLongString());
    }
}
