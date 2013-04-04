package aop2.controller;

import java.util.Observable;
import java.util.Observer;

import javax.swing.table.AbstractTableModel;

import aop2.domain.Mark;
import aop2.domain.MarksApplication;
import aop2.domain.Student;
import aop2.persistence.RepositoryException;


public class StudentController implements Observer
{
    public StudentController(final MarksApplication marksApplication)
    {
        this.marksApplication = marksApplication;
        this.marksTableModel = new MarksTableModel();
        this.marksApplication.addObserver(this);
    }

    @Override
    public void update(Observable sender, Object added)
    {
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
        this.marksApplication.deleteObserver(this);
    }

    public AbstractTableModel getMarksTableModel()
    {
        return this.marksTableModel;
    }

    private Student loggedInStudent;
    private final MarksApplication marksApplication;
    private final MarksTableModel marksTableModel;
}
