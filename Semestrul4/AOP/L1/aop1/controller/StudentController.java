package aop1.controller;

import java.util.Observable;
import java.util.Observer;

import javax.swing.table.AbstractTableModel;

import org.apache.log4j.Level;

import aop1.Application;
import aop1.domain.Mark;
import aop1.domain.MarksApplication;
import aop1.domain.Student;
import aop1.persistence.RepositoryException;

public class StudentController implements Observer
{
    public StudentController(final MarksApplication marksApplication)
    {
        Application.logger.log(Level.INFO,
                        "StudentController.StudentController("
                                        + marksApplication
                                        + " :MarksApplication)");
        this.marksApplication = marksApplication;
        this.marksTableModel = new MarksTableModel();
        this.marksApplication.addObserver(this);
    }

    @Override
    public void update(Observable sender, Object added)
    {
        Application.logger.log(Level.INFO, "StudentController.update(" + sender
                        + " :Observable, " + added + " :Object)");
        final Mark mark;
        if (added instanceof Mark)
        {
            mark = (Mark)added;
            if (this.loggedInStudent.getName().equals(
                            mark.getStudent().getName()))
                this.marksTableModel.addMark(mark);
        }
    }

    public boolean logIn(final String studentName, final String studentPassword)
                    throws RepositoryException
    {
        Application.logger.log(Level.INFO, "StudentController.logIn("
                        + studentName + " :String, " + studentPassword
                        + " :String)");
        this.loggedInStudent = this.marksApplication.getStudent(studentName,
                        studentPassword);
        if (this.loggedInStudent != null)
        {
            this.marksTableModel.addMarks(this.marksApplication
                            .getMarksForStudent(studentName));
            return true;
        }
        else
            return false;
    }

    public void unsubscribe()
    {
        Application.logger.log(Level.INFO, "StudentController.unsubscribe()");
        this.marksApplication.deleteObserver(this);
    }

    public AbstractTableModel getMarksTableModel()
    {
        Application.logger.log(Level.INFO,
                        "StudentController.getMarksTableModel()");
        return this.marksTableModel;
    }

    private Student loggedInStudent;
    private final MarksApplication marksApplication;
    private final MarksTableModel marksTableModel;
}
