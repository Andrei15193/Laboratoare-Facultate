package aspects;

import java.util.ArrayList;
import java.util.List;

import aop5.controller.SecretaryController;
import aop5.controller.StudentController;
import aop5.domain.MarksApplication;

public aspect ObserverAspect
{
    declare parents: MarksApplication implements ISubject;
    declare parents: StudentController implements IObserver;
    declare parents: SecretaryController implements IObserver;

    ISubject IObserver.subject;
    private List<IObserver> ISubject.observers = new ArrayList<IObserver>();
    
    public void ISubject.addObserver(IObserver observer)
    {
        observers.add(observer);
    }
    
    public void ISubject.removeObserver(IObserver observer)
    {
        observers.remove(observer);
    }
    
    public void ISubject.notifyObservers()
    {
        for(IObserver observer: observers)
            observer.update();
    }

    public void StudentController.update()
    {
        try
        {
            setMarksInModel();
        }
        catch (Exception e)
        {
        }
    }
    
    public void SecretaryController.update()
    {
        try
        {
            setCoursesInModel();
        }
        catch (Exception e)
        {
        }
    }
    
    pointcut Observed(MarksApplication marksApplication):
        execution(@ChangesState * aop5.domain.MarksApplication.*(..)) && this(marksApplication);

    after(MarksApplication marksApplication, StudentController studentController):
        initialization(StudentController.new(*)) && this(studentController) && args(marksApplication)
    {
        marksApplication.addObserver(studentController);
    }
    
    after(MarksApplication marksApplication, SecretaryController secretaryController):
        initialization(SecretaryController.new(*)) && this(secretaryController) && args(marksApplication)
    {
        marksApplication.addObserver(secretaryController);
    }
    
    after(MarksApplication marksApplication) returning: Observed(marksApplication)
    {
        marksApplication.notifyObservers();
    }
    
    after(StudentController studentController):
        execution(* StudentController.close()) && this(studentController)
    {
        studentController.subject.removeObserver(studentController);
    }
    
    after(SecretaryController secretaryController):
        execution(* SecretaryController.close()) && this(secretaryController)
    {
        secretaryController.subject.removeObserver(secretaryController);
    }
}
