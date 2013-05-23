package aop4.controller;

import javax.swing.table.AbstractTableModel;

import aop4.domain.MarksApplication;
import aop4.domain.Student;
import aop4.persistence.RepositoryException;

public class StudentController/* implements Observer */
{
    public StudentController(final MarksApplication marksApplication)
    {
        this.marksApplication = marksApplication;
        this.marksTableModel = new MarksTableModel();
        // this.marksApplication.addObserver(this);
    }

    // @Override
    // public void update(Observable sender, Object added)
    // {
    // final Mark mark;
    // if (added instanceof Mark)
    // {
    // mark = (Mark)added;
    // if (this.loggedInStudent.getName().equals(
    // mark.getStudent().getName()))
    // this.marksTableModel.addMark(mark);
    // }
    // }
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

    // public void unsubscribe()
    // {
    // this.marksApplication.deleteObserver(this);
    // }
    public AbstractTableModel getMarksTableModel()
    {
        return this.marksTableModel;
    }

    public void setMarksInModel() throws RepositoryException
    {
        this.marksTableModel.setMarks(this.marksApplication
                        .getMarksForStudent(this.loggedInStudent.getName()));
    }

    private Student loggedInStudent;
    private final MarksApplication marksApplication;
    private final MarksTableModel marksTableModel;
}
