package aspects;

import java.util.HashMap;
import java.util.Map;

import aop4.domain.Student;

public aspect Caching
{
    private Map<String, Student> studentCache;
    
    public Caching()
    {
        studentCache=new HashMap<String,Student>();
    }
    
    pointcut studentCachedOperations(String name)
        : execution(public Student aop4..*Students+.*(*)) && args(name);
    
    Student around(String name)
        : studentCachedOperations(name)
    {
        Student student = studentCache.get(name);
        if (student == null)
        {
            student = proceed(name);
            studentCache.put(name, student);
            System.out.println("  >>> New student in cache: " + name);
        }
        else
            System.out.println("  >>>  Student from cache: " + name);
        return student;
    }
}
