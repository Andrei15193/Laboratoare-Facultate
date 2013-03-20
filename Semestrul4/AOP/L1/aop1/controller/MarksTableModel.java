package aop1.controller;

import java.util.ArrayList;
import java.util.List;

import javax.swing.table.AbstractTableModel;

import org.apache.log4j.Level;

import aop1.Application;
import aop1.domain.Mark;

public class MarksTableModel extends AbstractTableModel
{
    public MarksTableModel()
    {
        Application.logger.log(Level.INFO, "MarksTableModel.MarksTableModel()");
        this.marks = new ArrayList<Mark>();
    }

    public void addMarks(final Mark[] marks)
    {
        Application.logger.log(Level.INFO, "MarksTableModel.addMarks(" + marks
                        + " :Mark[])");
        final int startIndex = this.marks.size();
        for (Mark mark: marks)
            this.marks.add(mark);
        this.fireTableRowsInserted(startIndex, this.marks.size() - 1);
    }

    public void addMark(final Mark mark)
    {
        Application.logger.log(Level.INFO, "MarksTableModel.addMark(" + mark
                        + " :Mark)");
        final int newIndex = this.marks.size();
        this.marks.add(mark);
        this.fireTableRowsInserted(newIndex, newIndex);
    }

    @Override
    public String getColumnName(int column)
    {
        Application.logger.log(Level.INFO, "MarksTableModel.getColumnName("
                        + column + " :int)");
        switch (column)
        {
        case 0:
            return "Course";
        case 1:
            return "Mark";
        default:
            return "No header";
        }
    };

    @Override
    public int getColumnCount()
    {
        Application.logger.log(Level.INFO, "MarksTableModel.getColumnCount()");
        return 2;
    }

    @Override
    public int getRowCount()
    {
        Application.logger.log(Level.INFO, "MarksTableModel.getRowCount()");
        return this.marks.size();
    }

    @Override
    public Object getValueAt(int row, int column)
    {
        Application.logger.log(Level.INFO, "MarksTableModel.getValueAt(" + row
                        + " :int, " + column + " :int)");
        final Mark mark = this.marks.get(row);
        switch (column)
        {
        case 0:
            return mark.getCourse().getName();
        case 1:
            return mark.getMark();
        default:
            return "Blank";
        }
    }

    private final List<Mark> marks;
    private static final long serialVersionUID = 1L;
}
