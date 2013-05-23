package aspects;

public aspect PerformanceMonitoring {
    pointcut all(): call(* aop4..*(..));
    Object around(): all()
    {
        long start = System.nanoTime();
        try
        {
            return proceed();
        }
        finally
        {
            long complete = System.nanoTime();
            System.out.println("Operation "
                            + thisJoinPointStaticPart.getSignature()
                            + " took " + (complete-start) + " nanoseconds.");
        }
    }
}
