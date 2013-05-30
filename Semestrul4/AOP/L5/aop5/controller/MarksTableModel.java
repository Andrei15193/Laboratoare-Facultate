package aop5.controller;

import java.util.ArrayList;
import java.util.List;

import javax.swing.table.AbstractTableModel;

import aop5.domain.Mark;

public class MarksTableModel extends AbstractTableModel
{
    public MarksTableModel()
    {
        this.marks = new ArrayList<Mark>();
    }

    public void addMarks(final Mark[] marks)
    {
        final int startIndex = this.marks.size();
        for (Mark mark: marks)
            this.marks.add(mark);
        this.fireTableRowsInserted(startIndex, this.marks.size() - 1);
    }

    public void addMark(final Mark mark)
    {
        final int newIndex = this.marks.size();
        this.marks.add(mark);
        this.fireTableRowsInserted(newIndex, newIndex);
    }

    @Override
    public String getColumnName(int column)
    {
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
        return 2;
    }

    @Override
    public int getRowCount()
    {
        return this.marks.size();
    }

    @Override
    public Object getValueAt(int row, int column)
    {
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

    public void setMarks(Mark[] marksForStudent)
    {
        this.marks.clear();
        for (Mark mark: marksForStudent)
            this.marks.add(mark);
        this.fireTableDataChanged();
    }

    private final List<Mark> marks;
    private static final long serialVersionUID = 1L;
}
