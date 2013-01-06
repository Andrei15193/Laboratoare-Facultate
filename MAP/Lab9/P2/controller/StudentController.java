package controller;

import java.util.Observable;
import java.util.Observer;

import javax.swing.table.AbstractTableModel;

import persistence.AllMarks;
import persistence.AllStudents;
import persistence.PersistenceException;
import controller.model.MarksTableModel;
import domain.Mark;
import domain.Student;

public class StudentController extends Observable implements Observer
{
    public StudentController(final AllMarks allMarks,
                    final AllStudents allStudents)
    {
        this.allMarks = allMarks;
        this.allStudents = allStudents;
        this.marksTableModel = new MarksTableModel();
    }

    @Override
    public void update(Observable sender, Object object)
    {
        Mark mark;
        if (this.loggedStudent != null && object instanceof Mark)
        {
            mark = (Mark)object;
            if (this.loggedStudent.getName().equals(mark.getStudentName()))
            {
                this.marksTableModel.addMark(mark);
            }
        }
    }

    public boolean logIn(final String studentName, final String studentPassword)
                    throws PersistenceException
    {
        this.loggedStudent = this.allStudents
                        .with(studentName, studentPassword);
        if (this.loggedStudent != null)
        {
            this.marksTableModel.addMarks(this.allMarks
                            .from(this.loggedStudent));
            return true;
        }
        else
            return false;
    }

    public AbstractTableModel getMarksTableModel()
    {
        return this.marksTableModel;
    }

    private Student loggedStudent;
    private final AllMarks allMarks;
    private final AllStudents allStudents;
    private final MarksTableModel marksTableModel;
}
